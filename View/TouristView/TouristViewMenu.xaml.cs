using BookingApp.DTOs;
using BookingApp.Observer;
using BookingApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BookingApp.View.TouristView;
using BookingApp.ApplicationServices;
using BookingApp.Resources;
using System.Linq;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TouristViewMenu.xaml
    /// </summary>
    public partial class TouristViewMenu : Window, IObserver
    {
        public static ObservableCollection<TourTouristDTO> Tours { get; set; }
        public ObservableCollection<LanguageDTO> Languages { get; set; } = new ObservableCollection<LanguageDTO>();
        public ObservableCollection<LocationDTO> Locations { get; set; } = new ObservableCollection<LocationDTO>();
        public TourScheduleDTO TourSchedule { get; set; }
        public User LoggedUser { get; set; }
        public TourTouristDTO SelectedTour { get; set; }
        public TourFilterDTO Filter { get; set; }

        public TouristViewMenu(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            Tours = new ObservableCollection<TourTouristDTO>();
            Filter = new TourFilterDTO();
            Update();
            SetLanguages();
            SetLocations();
            SimpleRequestService.GetInstance().CheckRequestStatus();
        }

        private void SetLanguages()
        {
            Languages.Clear();
            foreach (var language in LanguageService.GetInstance().GetAll())
                Languages.Add(new LanguageDTO(language));
        }

        private void SetLocations()
        {
            Locations.Clear();
            foreach (var location in LocationService.GetInstance().GetAll())
                Locations.Add(new LocationDTO(location));
        }

        public void Update() //OVDJE PRIKAZUJEM SAMO OBICNE TURE, NE REQUESTED TOURS
        {
            Tours.Clear();
            foreach (Tour tour in TourService.GetInstance().GetFiltered(Filter))
            {
                Model.Image image = GetFirstTourImage(tour.Id);
                Tours.Add(new TourTouristDTO(tour, LanguageService.GetInstance().GetById(tour.LanguageId), LocationService.GetInstance().GetById(tour.LocationId), TourScheduleService.GetInstance().GetByTour(tour), image.Path));
            }
        }
        public Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void ClearFilters()
        {
            locationComboBox.SelectedIndex = -1;
            languageComboBox.SelectedIndex = -1;
            durationBox.Value = 0;
            capacityBox.Value = 0;
        }
        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            ClearFilters();
            Update();
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedTour = (TourTouristDTO)button.DataContext;

            if (selectedTour != null)
            {
                TourReservationForm form = new TourReservationForm(LoggedUser, selectedTour);
                form.Owner = this;
                form.ShowDialog();

            }
        }
        private void MyActiveTours_Click(object sender, RoutedEventArgs e)
        {
            ActiveTours activeTours = new ActiveTours(TourSchedule, LoggedUser);
            activeTours.Owner = this;
            activeTours.ShowDialog();
        }

        private void Inbox_Click(object sender, RoutedEventArgs e)
        {
            Inbox inbox = new Inbox(LoggedUser);
            inbox.Owner = this;
            inbox.ShowDialog();
        }

        private void TourRating_Click(object sender, RoutedEventArgs e)
        {
            FinishedTours finishedTours = new FinishedTours(LoggedUser);
            finishedTours.Owner = this;
            finishedTours.ShowDialog();
        }
        private void Vouchers_Click(object sender, RoutedEventArgs e)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Owner = this;
            vouchersView.ShowDialog();
        }

        private void DetailedView_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedTour = (TourTouristDTO)button.DataContext;

            DetailedView details = new DetailedView(selectedTour);
            details.Owner = this;
            details.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow help = new HelpWindow();
            help.ShowDialog();
        }
        private void MakeRequest_Click(object sender, RoutedEventArgs e)
        {
            RequestForm request = new RequestForm(LoggedUser);
            request.Owner = this;
            request.ShowDialog();
        }
        private void MyRequests_Click(object sender, RoutedEventArgs e)
        {
            MyRequests request = new MyRequests(LoggedUser);
            request.Owner = this;
            request.ShowDialog();
        }
        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            RequestStatistics statistics = new RequestStatistics(LoggedUser.Id);
            statistics.Owner = this;
            statistics.ShowDialog();
        }

        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (languageComboBox.SelectedItem != null)
            {
                Filter.Language = (LanguageDTO)languageComboBox.SelectedItem;
                return;
            }
            Filter.Language = new LanguageDTO();
        }
        private void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (locationComboBox.SelectedItem != null)
            {
                Filter.Location = (LocationDTO)locationComboBox.SelectedItem;
                return;
            }
            Filter.Location = new LocationDTO();
        }
    }
}
