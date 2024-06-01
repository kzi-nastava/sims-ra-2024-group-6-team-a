using BookingApp.ApplicationServices;
using BookingApp.Model;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.ViewModels.OwnerViewModel
{
    class GeneratePdfVM : INotifyPropertyChanged
    {
        private DateTime? _startDate;
        private DateTime? _endDate;
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        public ICommand GeneratePdfCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand SelectStartDateCommand { get; }
        public ICommand SelectEndDateCommand { get; }

        public int accId { get; set; }
        public GeneratePdfVM(int id) 
        {
            accId = id;
            GeneratePdfCommand = new RelayCommand(GeneratePdf, CanGeneratePdf);
            CloseCommand = new RelayCommand(CloseWindow);
            SelectStartDateCommand = new RelayCommand(SelectStartDate);
            SelectEndDateCommand = new RelayCommand(SelectEndDate);

        }

        private bool CanGeneratePdf(object parameter)
        {
            return StartDate.HasValue && EndDate.HasValue && StartDate < EndDate;
        }

        private void GeneratePdf(object parameter)
        {
 
                using (PdfDocument document = new PdfDocument())
                {

                    PdfPage page = document.Pages.Add();

                    PdfGraphics graphics = page.Graphics;

                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                    PdfGrid pdfGrid = new PdfGrid();

                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("Name");
                    dataTable.Columns.Add("Check in date");
                    dataTable.Columns.Add("Check out date");

                    foreach (AccommodationReservation res in AccommodationReservationService.GetInstance().GetByAccommodationId(accId))
                    {
                        DateOnly selectedStart = DateOnly.FromDateTime((DateTime)StartDate);
                        DateOnly selectedEnd = DateOnly.FromDateTime((DateTime)EndDate);

                        if (res.CheckInDate > selectedStart && res.CheckOutDate < selectedEnd)
                        {
                            string name = GuestService.GetInstance().GetFullname(res.GuestId);

                            dataTable.Rows.Add(new object[] { name, res.CheckInDate, res.CheckOutDate });

                        }

                    }

                    pdfGrid.DataSource = dataTable;

                    pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                    pdfGrid.Draw(page, new PointF(10, 10));

                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    // Navigate up to the project root directory
                    string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
                    // Define the relative path from the project root
                    string relativePath = @"Resources\owner-report.pdf";
                    // Combine the project root with the relative path
                    string fullPath = Path.Combine(projectRoot, relativePath);

                    document.Save(fullPath);

                }
                
                MessageBox.Show("Generated the PDF report.");
                CloseWindow(Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));
            
        }
        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }

        private void SelectStartDate(object parameter)
        {
            if (parameter is DatePicker datePicker)
            {
                datePicker.IsDropDownOpen = true;
            }
        }

        private void SelectEndDate(object parameter)
        {
            if (parameter is DatePicker datePicker)
            {
                datePicker.IsDropDownOpen = true;
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
