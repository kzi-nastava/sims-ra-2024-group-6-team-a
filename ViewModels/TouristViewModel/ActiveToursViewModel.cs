using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class ActiveToursViewModel
    {
        public static ObservableCollection<TourScheduleDTO> Tours { get; set; }
        public TourScheduleDTO SelectedTourSchedule { get; set; }
        public TourTouristDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }
        public RelayCommand TrackKeypointCommand { get; set; }

        private LocationRepository _locationRepository;
        private TourService _tourService;
        private TourScheduleService _schdeuleService;
        private TourReservationService _reservationService;
        public ActiveTours Window { get; set; }
        public ActiveToursViewModel(ActiveTours window, TourScheduleDTO tourSchedule, User user)
        {
            Window = window;

            _tourService = new TourService();
            _schdeuleService = new TourScheduleService();
            _locationRepository = new LocationRepository();
            _reservationService = new TourReservationService();
            LoggedUser = user;

            Tours = new ObservableCollection<TourScheduleDTO>();
            SelectedTourSchedule = tourSchedule;
            TrackKeypointCommand = new RelayCommand(Execute_TrackKeypointCommand);
            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach (TourSchedule tour in _schdeuleService.GetOngoingSchedulesByUser(LoggedUser))
            {
                Tours.Add(new TourScheduleDTO(tour));
            }

        }

        private void Execute_TrackKeypointCommand(object sender)
        {
            if (SelectedTourSchedule != null)
            {
                KeypointsTracking keypointsTracking = new KeypointsTracking(SelectedTourSchedule.Id);
                keypointsTracking.ShowDialog();

            }

        }
    }
}
