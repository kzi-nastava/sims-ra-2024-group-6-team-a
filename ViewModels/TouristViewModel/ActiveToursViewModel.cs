using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.TouristView;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class ActiveToursViewModel
    {
        public static ObservableCollection<TourScheduleDTO> Tours { get; set; }
        public static ObservableCollection<TourScheduleDTO> FutureTours { get; set; }
        public TourScheduleDTO SelectedTourSchedule { get; set; }
        public User LoggedUser { get; set; }
        public RelayCommand TrackKeypointCommand { get; set; }
        public RelayCommand GenereateReportCommand { get; set; }
        public ActiveToursViewModel( TourScheduleDTO tourSchedule, User user)
        {
            
            LoggedUser = user;

            Tours = new ObservableCollection<TourScheduleDTO>();
            FutureTours = new ObservableCollection<TourScheduleDTO>();
            SelectedTourSchedule = tourSchedule;
            TrackKeypointCommand = new RelayCommand(Execute_TrackKeypointCommand);
            GenereateReportCommand = new RelayCommand(Execute_GenerateReportCommand);
            Update();
        }

        public void Update()
        {
            Tours.Clear(); 
            foreach (TourSchedule tour in TourScheduleService.GetInstance().GetOngoingSchedulesByUser(LoggedUser))
            {
                Model.Image image = GetFirstTourImage(tour.TourId);
                Tours.Add(new TourScheduleDTO(tour, LocationService.GetInstance().GetById(TourService.GetInstance().GetById(tour.TourId).LocationId), LanguageService.GetInstance().GetById(TourService.GetInstance().GetById(tour.TourId).LanguageId), image.Path));
            }

            FutureTours.Clear(); 
            foreach (TourSchedule tour in TourScheduleService.GetInstance().GetFutureSchedulesByUser(LoggedUser))
            {
                Model.Image image = GetFirstTourImage(tour.TourId);
                FutureTours.Add(new TourScheduleDTO(tour, LocationService.GetInstance().GetById(TourService.GetInstance().GetById(tour.TourId).LocationId), LanguageService.GetInstance().GetById(TourService.GetInstance().GetById(tour.TourId).LanguageId), image.Path));
            }

        }
        public Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).FirstOrDefault();
        }

        private void Execute_TrackKeypointCommand(object obj)
        {
            TourScheduleDTO tourSchedule = (TourScheduleDTO)obj;
            KeypointsTracking keypointsTracking = new KeypointsTracking(tourSchedule);
            keypointsTracking.Owner = Application.Current.MainWindow;
            keypointsTracking.ShowDialog();
        }



        private void Execute_GenerateReportCommand(object parameter)
        {
            Tourist tourist = TouristService.GetInstance().GetByTouristId(LoggedUser.Id);
            TourScheduleDTO tourSchedule = (TourScheduleDTO)parameter;
            List<Checkpoint> checkpoints = CheckpointService.GetInstance().GetAllByTourScheduleId(tourSchedule.Id).ToList();
            List<TourGuests> guests = TourGuestService.GetInstance().GetAllByTourId(tourSchedule.Id).ToList();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
            string relativePath = @$"Resources\tourist-report{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.pdf";
            string fullPath = Path.Combine(projectRoot, relativePath);
            TouristReportGenerator reportGenerator = new TouristReportGenerator(DateTime.Now, tourSchedule, tourist,checkpoints, guests);
            QuestPDF.Settings.License = LicenseType.Community;
            reportGenerator.GeneratePdf(fullPath);

            if (System.Windows.MessageBox.Show("Do you want to view your report right away?", "PDF Report", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {        
                MessageBox.Show("PDF report generated successfully!");
            }
        }
    }
}
