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
        public TourScheduleDTO TourSchedule { get; set; }
        public User LoggedUser { get; set; }
        public TourTouristDTO SelectedTour { get; set; }

        public string NameSearch { get; set; }
        public string CitySearch { get; set; }
        public string StateSearch { get; set; }

        public string LanguageSearch { get; set; }
        public string CapacitySearch { get; set; }
        public string DurationSearch { get; set; }

        public TouristViewMenu(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            Tours = new ObservableCollection<TourTouristDTO>();
            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach (Tour tour in TourService.GetInstance().GetAll())
            {
                Model.Image image = GetFirstTourImage(tour.Id);
                Tours.Add(new TourTouristDTO(tour, LocationService.GetInstance().GetById(tour.LocationId), TourScheduleService.GetInstance().GetByTour(tour), image.Path));
            }
        }
        public Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }

       /* private void TextboxCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            CitySearch = TextboxCity.Text;

        }

        private void TextboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameSearch = TextboxName.Text;
        }

        private void TextboxState_TextChanged(object sender, TextChangedEventArgs e)
        {
            StateSearch = TextboxState.Text;
        }

        private void TextboxDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            DurationSearch = TextboxDuration.Text;
        }

        private void TextboxLanguage_TextChanged(object sender, TextChangedEventArgs e)
        {
            LanguageSearch = TextboxLanguage.Text;
        }

        private void TextboxCapacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            CapacitySearch = TextboxCapacity.Text;
        }*/

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Tours.Clear();

            foreach (Tour tour in TourService.GetInstance().GetAll())
            {
                if (CheckSearchConditions(tour))
                {
                    Model.Image image = GetFirstTourImage(tour.Id);
                    Tours.Add(new TourTouristDTO(tour, LocationService.GetInstance().GetById(tour.LocationId), TourScheduleService.GetInstance().GetByTour(tour), image.Path));
                }
            }
        }

        public bool CheckSearchConditions(Tour tour)
        {
            bool containsName = IsStringMatch(tour.Name, NameSearch);
            bool containsCity = IsStringMatch(LocationService.GetInstance().GetById(tour.LocationId).City, CitySearch);
            bool containsState = IsStringMatch(LocationService.GetInstance().GetById(tour.LocationId).State, StateSearch);
            //bool containsLanguage = IsStringMatch(tour.Language.ToString(), LanguageSearch);
            bool capacityIsLower = IsCapacityLower(tour.Capacity, CapacitySearch);
            bool containsDuration = IsDurationMatch(tour.Duration, DurationSearch);

            return containsName && containsState && containsCity && containsDuration && capacityIsLower  /* && containsLanguage*/;
        }

        private bool IsStringMatch(string target, string search)
        {
            return string.IsNullOrEmpty(search) || target.ToLower().Contains(search.ToLower());
        }

        private bool IsCapacityLower(int capacity, string search)
        {
            return string.IsNullOrEmpty(search) || Convert.ToInt32(search) <= capacity;
        }

        private bool IsDurationMatch(double duration, string search)
        {
            return string.IsNullOrEmpty(search) || Convert.ToDouble(search) == duration;
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
        private void MyActiveTours_Click(object sender, RoutedEventArgs e)
        {
            ActiveTours activeTours = new ActiveTours(TourSchedule, LoggedUser);
            activeTours.ShowDialog();
        }

        private void Inbox_Click(object sender, RoutedEventArgs e)
        {
            Inbox inbox = new Inbox(LoggedUser);
            inbox.ShowDialog();
        }

        private void TourRating_Click(object sender, RoutedEventArgs e)
        {
            FinishedTours finishedTours = new FinishedTours(LoggedUser);
            finishedTours.ShowDialog();
        }
        private void Vouchers_Click(object sender, RoutedEventArgs e)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
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
    }
}
