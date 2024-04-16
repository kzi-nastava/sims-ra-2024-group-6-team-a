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

        private TourScheduleService _schdeuleService;
        private TourReservationService _reservationService;
        
        public SameLocationToursWindow(TourScheduleDTO tourSchedule, User user)
        {
            InitializeComponent();
            DataContext = this;

            _schdeuleService = new TourScheduleService();
            _reservationService = new TourReservationService();
            LoggedUser = user;

            Tours = new ObservableCollection<TourTouristDTO>();
            TourScheduleDTO = tourSchedule;

            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach(Tour tour in TourService.GetInstance().GetSameLocationTours(TourScheduleDTO))
            {
                Tours.Add(new TourTouristDTO(tour, LocationService.GetInstance().GetById(tour.LocationId), TourScheduleService.GetInstance().GetByTour(tour)));
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
