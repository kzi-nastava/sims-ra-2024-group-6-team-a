using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
namespace BookingApp.ViewModels.GuideViewModel
{
    public class TourStatisticsViewModel
    {
        public User LoggedUser { get; set; }
       

        public Guide LoggedGuide { get; set; }
        public static ObservableCollection<TourStatisticsDTO> FinishedTours { get; set; }
        public static ObservableCollection<TourStatisticsDTO> MostVisitedTour { get; set; }

        private string _selectedYear;
        public string SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    if (_selectedYear.Equals("Default"))
                        FindMostVisitedTour(0);
                    else
                    {
                        FindMostVisitedTour(int.Parse(_selectedYear));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        public TourStatisticsPage Window { get; set; }

        public RelayCommand GeneratePDFCommand { get; set; }


        public TourStatisticsViewModel(TourStatisticsPage window,User user)
        {
           
            LoggedUser = user;

            Window = window;
            LoggedGuide = GuideService.GetInstance().GetByUserId(LoggedUser.Id);
            FinishedTours = new ObservableCollection<TourStatisticsDTO>();
            MostVisitedTour = new ObservableCollection<TourStatisticsDTO>();
            Update();
            FindMostVisitedTour(0);
            GeneratePDFCommand = new RelayCommand(Execute_GeneratePDFCommand);

        }
        private void Execute_GeneratePDFCommand(object obj)
        {
            GeneratePdf(FinishedTours.ToList(), LoggedGuide);
        }

        public void FindMostVisitedTour(int selectedYear)
        {
            if (FinishedTours.Count == 0) return;

            TourStatisticsDTO mostVisitedTour = GetMostVisitedTour(selectedYear);

            MostVisitedTour.Clear();
            MostVisitedTour.Add(mostVisitedTour);
        }

        public TourStatisticsDTO GetMostVisitedTour(int selectedYear)
        {
            TourStatisticsDTO mostVisitedTour = FinishedTours.First();

            if (Window.datesBox.SelectedIndex != 0 && Window.datesBox.SelectedIndex != -1 && selectedYear != 0)
            {
                mostVisitedTour = FindMostVisitedTourByYear(selectedYear);
            }
            else
            {
                mostVisitedTour = FindMostVisitedTourOverall();
            }

            return mostVisitedTour;
        }

        public TourStatisticsDTO FindMostVisitedTourByYear(int selectedYear)
        {

            TourStatisticsDTO mostVisitedTour = new TourStatisticsDTO();
         
            foreach (Tour tour in TourService.GetInstance().GetAllByUser(LoggedUser))
            {
                Location location = LocationService.GetInstance().GetById(tour.LocationId);
                Model.Image image = GetFirstTourImage(tour.Id);
                Language language = LanguageService.GetInstance().GetById(tour.LanguageId);
                int tourists = 0;
                int children = 0;
                int adult = 0;
                int elderly = 0;
                foreach (TourSchedule schedule in TourScheduleService.GetInstance().GetAllByTourId(tour.Id))
                {
                    if (schedule.TourActivity != Enums.TourActivity.Finished || schedule.Start.Year != selectedYear) continue;

                    tourists += TourGuestService.GetInstance().CountGuests(schedule.Id);
                    children += TourGuestService.GetInstance().CountChildren(schedule.Id);
                    adult += TourGuestService.GetInstance().CountAdult(schedule.Id);
                    elderly += TourGuestService.GetInstance().CountElderly(schedule.Id);

                    if (mostVisitedTour.TouristNumber <= tourists)
                    mostVisitedTour = new TourStatisticsDTO(tour.Name, language, image.Path, location, tourists, children, adult, elderly);

                }
            }

            return mostVisitedTour;
        }
        
        
        
        public TourStatisticsDTO FindMostVisitedTourOverall()
        {
            TourStatisticsDTO? mostVisitedTour = FinishedTours
                .OrderByDescending(tour => tour.TouristNumber)
                .FirstOrDefault();

            return mostVisitedTour ?? FinishedTours.First();
        }




        //It is assumed that the statistics are done for the sum of all TourSchedules, if so, we go through the list of Tour
        //and take the tour schedule, for each tour we count the number of children/adults/elderly, we also need to make a function for dates Combobox because of distinct value
        public void Update()
        {
            Window.datesBox.Items.Clear();
            FinishedTours.Clear();
            List<int> dates = new List<int>();

            foreach (Tour tour in TourService.GetInstance().GetAllByUser(LoggedUser))
            {
                Location location = LocationService.GetInstance().GetById(tour.LocationId);
                Model.Image image = GetFirstTourImage(tour.Id);
                Language language = LanguageService.GetInstance().GetById(tour.LanguageId);

                int tourists = 0;
                int children = 0;
                int adult = 0;
                int elderly = 0;
                foreach (TourSchedule schedule in TourScheduleService.GetInstance().GetAllByTourId(tour.Id))
                {
                    if (schedule.TourActivity != Enums.TourActivity.Finished) continue;
                   
                    tourists += TourGuestService.GetInstance().CountGuests(schedule.Id);
                    children += TourGuestService.GetInstance().CountChildren(schedule.Id);
                    adult += TourGuestService.GetInstance().CountAdult(schedule.Id);
                    elderly += TourGuestService.GetInstance().CountElderly(schedule.Id);
                    dates.Add(schedule.Start.Year);
                }
                FinishedTours.Add(new TourStatisticsDTO(tour.Name, language, image.Path, location, tourists, children, adult, elderly));
            }
            AddDatesToComboBox(dates);
        }

        public void AddDatesToComboBox(List<int> dates)
        {
            var distinctDates = dates.Distinct().ToList();
            foreach (var date in distinctDates)
            {
                Window.datesBox.Items.Add(date);
            }

            Window.datesBox.Items.Insert(0, "Default");

        }

        public Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }


        public void LoadStatistics()
        {
            Update();
        }



        public void GeneratePdf(List<TourStatisticsDTO> statistics, User guide)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document();
                try
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Add logo
                    string relativeLogoPath = "Resources/Images/logo.png";
                    string absoluteLogoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeLogoPath);

                    if (File.Exists(absoluteLogoPath))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(absoluteLogoPath);
                        logo.Alignment = Element.ALIGN_CENTER;
                        logo.ScaleToFit(100f, 100f);
                        document.Add(logo);
                    }

                    // Add title
                    Font titleFont = FontFactory.GetFont("Arial", 36, Font.BOLD, BaseColor.DARK_GRAY);
                    Paragraph title = new Paragraph("Tour Statistics Report", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 10f;
                    document.Add(title);

                    // Add current date
                    Font dateFont = FontFactory.GetFont("Arial", 14, BaseColor.GRAY);
                    Paragraph date = new Paragraph(DateTime.Now.ToString("MMMM dd, yyyy"), dateFont);
                    date.Alignment = Element.ALIGN_CENTER;
                    date.SpacingAfter = 30f;
                    document.Add(date);

                    // Create table for tour statistics
                    PdfPTable table = new PdfPTable(7);
                    table.WidthPercentage = 100;
                    table.SpacingAfter = 20f;
                    table.SetWidths(new float[] { 2f, 2f, 3f, 2f, 2f, 2f, 2f });

                    // Add table header
                    Font headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
                    AddTableHeader(table, "Tour Name", headerFont);
                    AddTableHeader(table, "Language", headerFont);
                    AddTableHeader(table, "Location", headerFont);
                    AddTableHeader(table, "Tourists", headerFont);
                    AddTableHeader(table, "Children", headerFont);
                    AddTableHeader(table, "Adults", headerFont);
                    AddTableHeader(table, "Elderly", headerFont);

                    // Add tour statistics to table
                    Font cellFont = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                    foreach (var stat in statistics)
                    {
                        AddTableCell(table, stat.Name, cellFont);
                        AddTableCell(table, stat.Language, cellFont);
                        AddTableCell(table, stat.Location, cellFont);
                        AddTableCell(table, stat.TouristNumber.ToString(), cellFont);
                        AddTableCell(table, stat.Children.ToString(), cellFont);
                        AddTableCell(table, stat.Adult.ToString(), cellFont);
                        AddTableCell(table, stat.Elderly.ToString(), cellFont);
                    }

                    document.Add(table);

                    // Add approved line with guide's signature
                    Font signatureFont = FontFactory.GetFont("Arial", 14, Font.ITALIC, BaseColor.BLACK);
                    Paragraph approved = new Paragraph($"Approved by: {LoggedGuide.Name}   {LoggedGuide.Surname}", signatureFont);
                    approved.Alignment = Element.ALIGN_RIGHT;
                    approved.SpacingBefore = 30f;
                    document.Add(approved);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error generating PDF: {ex.Message}");
                    return;
                }
                finally
                {
                    document.Close();
                }

                byte[] pdfBytes = stream.ToArray();

                OfferPdfAsDownload(pdfBytes, "TourStatisticsReport.pdf");
            }
        }

        private void OfferPdfAsDownload(byte[] pdfBytes, string fileName)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = fileName; 
            dlg.DefaultExt = ".pdf"; 
            dlg.Filter = "PDF documents (.pdf)|*.pdf";

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                File.WriteAllBytes(dlg.FileName, pdfBytes);
                MessageBox.Show("PDF file downloaded successfully.");
            }
        }



        private void AddTableHeader(PdfPTable table, string headerText, Font headerFont)
        {
            PdfPCell headerCell = new PdfPCell(new Phrase(headerText, headerFont));
            headerCell.BackgroundColor = new BaseColor(79, 129, 189);
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(headerCell);
        }

        private void AddTableCell(PdfPTable table, string text, Font cellFont)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, cellFont));
            cell.Padding = 5f;
            cell.BackgroundColor = new BaseColor(210, 230, 255);
            table.AddCell(cell);
        }

    }
}
