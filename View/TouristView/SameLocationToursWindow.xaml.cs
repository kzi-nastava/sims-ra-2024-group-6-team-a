using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SameLocationToursWindow.xaml
    /// </summary>
    public partial class SameLocationToursWindow : Window, BookingApp.Observer.IObserver
    {
        public static ObservableCollection<TourTouristDTO> Tours { get; set; }

        public TourScheduleDTO TourScheduleDTO { get; set; }
        public User LoggedUser { get; set; }

        private LocationRepository _locationRepository;
        private TourService _tourService;
        private TourScheduleService _schdeuleService;
        private TourReservationService _reservationService;
        
        public SameLocationToursWindow(TourScheduleDTO tourSchedule, User user)
        {
            InitializeComponent();
            DataContext = this;

            _tourService = new TourService();
            _schdeuleService = new TourScheduleService();
            _locationRepository = new LocationRepository();
            _reservationService = new TourReservationService();
            //_tourService.Subscribe(this);
            LoggedUser = user;

            Tours = new ObservableCollection<TourTouristDTO>();
            TourScheduleDTO = tourSchedule;

            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach(Tour tour in _tourService.GetSameLocationTours(TourScheduleDTO))
            {
                Tours.Add(new TourTouristDTO(tour, _locationRepository.GetById(tour.LocationId), _schdeuleService.GetByTour(tour)));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            TourTouristDTO selectedTour = (TourTouristDTO)tourDataGrid.SelectedItem;

            if (selectedTour != null)
            {
                TourReservationForm form = new TourReservationForm(LoggedUser, selectedTour, _reservationService, _schdeuleService);
                form.ShowDialog();

            }
        }
    }
}
