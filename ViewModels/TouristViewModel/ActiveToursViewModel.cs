using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ActiveToursViewModel( TourScheduleDTO tourSchedule, User user)
        {
            
            LoggedUser = user;

            Tours = new ObservableCollection<TourScheduleDTO>();
            FutureTours = new ObservableCollection<TourScheduleDTO>();
            SelectedTourSchedule = tourSchedule;
            TrackKeypointCommand = new RelayCommand(Execute_TrackKeypointCommand);
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
    }
}
