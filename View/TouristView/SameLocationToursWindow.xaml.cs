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
        
        public SameLocationToursWindow(TourScheduleDTO tourSchedule, User user)
        {
            InitializeComponent();
            DataContext = this;
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
                Model.Image image = GetFirstTourImage(tour.Id);
                Tours.Add(new TourTouristDTO(tour, LanguageService.GetInstance().GetById(tour.LanguageId), LocationService.GetInstance().GetById(tour.LocationId), TourScheduleService.GetInstance().GetByTour(tour), image.Path));
            }
        }
        public Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedTour = (TourTouristDTO)button.DataContext;

            if (selectedTour != null)
            {
                TourReservationForm form = new TourReservationForm(LoggedUser, selectedTour);
                form.ShowDialog();

            }
        }

        private void DetailedView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedTour = (TourTouristDTO)button.DataContext;

            DetailedView details = new DetailedView(selectedTour);
            details.Owner = this;
            details.ShowDialog();
        }
    }
}
