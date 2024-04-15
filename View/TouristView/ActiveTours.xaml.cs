using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModels.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for ActiveTours.xaml
    /// </summary>
    public partial class ActiveTours : Window
    {
        public static ObservableCollection<TourScheduleDTO> Tours { get; set; }
        public TourScheduleDTO SelectedTourSchedule { get; set; }
        public TourTouristDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }

        private LocationRepository _locationRepository;
        private TourService _tourService;
        private TourScheduleService _schdeuleService;
        private TourReservationService _reservationService;

        public ActiveToursViewModel ActiveToursWindow { get; set; }
        public ActiveTours(TourScheduleDTO tourSchedule, User user)
        {
            InitializeComponent();
            ActiveToursWindow = new ActiveToursViewModel(this, tourSchedule, user);
            DataContext = ActiveToursWindow;
            Update();
        }

        private void Update() 
        {
            ActiveToursWindow.Update();

        }

    }
}
