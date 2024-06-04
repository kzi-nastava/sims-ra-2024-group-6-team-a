using BookingApp.DTOs;
using BookingApp.Observer;
using BookingApp.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BookingApp.View.TouristView;
using BookingApp.ApplicationServices;
using BookingApp.Resources;
using System.Linq;
using BookingApp.Validation;
using System;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TouristViewMenu.xaml
    /// </summary>
    public partial class TouristViewMenu : Window,  IObserver
    {
        public static ObservableCollection<TourTouristDTO> Tours { get; set; } = new ObservableCollection<TourTouristDTO>();
        public ObservableCollection<LanguageDTO> Languages { get; set; } = new ObservableCollection<LanguageDTO>();
        public ObservableCollection<LocationDTO> Locations { get; set; } = new ObservableCollection<LocationDTO>();
        public TourScheduleDTO TourSchedule { get; set; }
        public User LoggedUser { get; set; }
        public TourFilterDTO Filter { get; set; }
        public TouristViewMenu(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;
            VideoControl.Volume = 100;
            VideoControl.Height = 500;
            Filter = new TourFilterDTO();
            Update();
            SetLanguages();
            SetLocations();
            TourRequestService.GetInstance().CheckRequestStatus();
            ComplexTourRequestService.GetInstance().CheckRequestStatus();
        }
        private void VideoControl_MediaOpened(object sender, RoutedEventArgs e)
        {
            VideoControl.Play();
            VideoControl.Pause();
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

        public void Update()
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
            bool isValid = true;
            if (locationComboBox.Text != string.Empty && !Locations.Any(l => l.LocationDisplayFormat == locationComboBox.Text))
            {
                locationError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                locationError.Visibility = Visibility.Collapsed;
            }

            if (languageComboBox.Text != string.Empty && !Languages.Any(l => l.LanguageDisplayFormat == languageComboBox.Text))
            {
                languageError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                languageError.Visibility = Visibility.Collapsed;
            }

            if (durationBox.Value == null || durationBox.Value > 200)
            {
                durationError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                durationError.Visibility = Visibility.Collapsed;
            }

            if (capacityBox.Value == null || capacityBox.Value > 100)
            {
                capacityError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                capacityError.Visibility = Visibility.Collapsed;
            }

            if (isValid)
            {
                Update();
            }
        }

        private void ClearFilters()
        {
            locationComboBox.SelectedIndex = -1;
            languageComboBox.SelectedIndex = -1;
            durationBox.Value = 0;
            capacityBox.Value = 0;
            languageComboBox.Text = string.Empty;
            locationComboBox.Text = string.Empty;
            Filter = new TourFilterDTO();
            locationError.Visibility = Visibility.Collapsed;
            languageError.Visibility = Visibility.Collapsed;
            durationError.Visibility = Visibility.Collapsed;
            capacityError.Visibility = Visibility.Collapsed;
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

        private void User_Click(object sender, RoutedEventArgs e)
        {
            Profile profileWindow = new Profile(LoggedUser);
            profileWindow.Owner = this;
            profileWindow.ShowDialog();
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
        void PlayClick(object sender, EventArgs e)
        {
            VideoControl.Play();
        }
        void PauseClick(object sender, EventArgs e)
        {
            VideoControl.Pause();
        }

        void StopClick(object sender, EventArgs e)
        {
            VideoControl.Stop();
        }
        private void VideoControl_Loaded(object sender, RoutedEventArgs e)
        {
            VideoControl.Stop();
        }

    }
}
