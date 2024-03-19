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
        private TourRepository _tourRepository;
        private TourScheduleRepository _scheduleRepository;
        private TourReservationRepository _tourReservationRepository;
        
        public SameLocationToursWindow(TourScheduleDTO tourSchedule, User user)
        {
            InitializeComponent();
            DataContext = this;

            _tourRepository = new TourRepository();
            _scheduleRepository = new TourScheduleRepository();
            _locationRepository = new LocationRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository.Subscribe(this);
            LoggedUser = user;

            Tours = new ObservableCollection<TourTouristDTO>();
            TourScheduleDTO = tourSchedule;

            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach(Tour tour in _tourRepository.GetSameLocationTours(TourScheduleDTO))
            {
                Tours.Add(new TourTouristDTO(tour, _locationRepository.GetById(tour.LocationId), _scheduleRepository.GetByTour(tour)));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            TourTouristDTO selectedTour = (TourTouristDTO)tourDataGrid.SelectedItem;

            if (selectedTour != null)
            {
                TourReservationForm form = new TourReservationForm(LoggedUser, selectedTour, _tourReservationRepository, _scheduleRepository);
                form.ShowDialog();

            }
        }
    }
}
