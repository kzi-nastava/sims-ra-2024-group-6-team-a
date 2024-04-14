using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Observer;
using BookingApp.Resources;
using BookingApp.Model;
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
using System.ComponentModel;
using BookingApp.View.TouristView;
using BookingApp.ApplicationServices;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TouristViewMenu.xaml
    /// </summary>
    public partial class TouristViewMenu : Window, IObserver
    {

        public static ObservableCollection<TourTouristDTO> Tours {  get; set; }
        public TourTouristDTO SelectedTour { get; set; }
        public TourReservationDTO SelectedReservation { get; set; }
        public TourScheduleDTO TourSchedule { get; set; }
        public User LoggedUser { get; set; }

        public string NameSearch { get; set; }
        public string CitySearch { get; set; }
        public string StateSearch { get; set; }

        public string LanguageSearch { get; set; }
        public string CapacitySearch { get; set; }
        public string DurationSearch { get; set; }


        private LocationRepository _locationRepository;
        private TourService _tourService;
        private ImageRepository _imageRepository;
        private TourScheduleService _scheduleService;
        private TourReservationService _reservationService;
        private UserRepository _userRepository;

        public TouristViewMenu(User user,  LocationRepository locationRepository, ImageRepository imageRepository, TourScheduleService scheduleService, TourReservationService reservationService, UserRepository userRepository)
        {
            InitializeComponent();
            DataContext = this;

            this._locationRepository = locationRepository;
            this._imageRepository = imageRepository;
            this._scheduleService = scheduleService;
            this._reservationService = reservationService;
            this._userRepository = userRepository;
            
            LoggedUser  = user;
            _tourService = new TourService();
            //_tourService.Subscribe(this);
            
            Tours = new ObservableCollection<TourTouristDTO>();
            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach(Tour tour in _tourService.GetAll()) 
            {
               
                Tours.Add(new TourTouristDTO(tour, _locationRepository.GetById(tour.LocationId), _scheduleService.GetByTour(tour) ));
            }

            
        }

        private void TextboxCity_TextChanged(object sender, TextChangedEventArgs e)
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
            DurationSearch= TextboxDuration.Text;
        }

        private void TextboxLanguage_TextChanged(object sender, TextChangedEventArgs e)
        {
            LanguageSearch = TextboxLanguage.Text;
        }

        private void TextboxCapacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            CapacitySearch = TextboxCapacity.Text;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Tours.Clear();

            foreach (Tour tour in _tourService.GetAll())
            {
                if (CheckSearchConditions(tour))
                {
                    Tours.Add(new TourTouristDTO(tour, _locationRepository.GetById(tour.LocationId), _scheduleService.GetByTour(tour)));
                }
            }
        }

        public bool CheckSearchConditions(Tour tour)
        {
            bool containsName = IsStringMatch(tour.Name, NameSearch);
            bool containsCity = IsStringMatch(_locationRepository.GetById(tour.LocationId).City, CitySearch);
            bool containsState = IsStringMatch(_locationRepository.GetById(tour.LocationId).State, StateSearch);
            bool containsLanguage = IsStringMatch(tour.Language.ToString(), LanguageSearch);
            bool capacityIsLower = IsCapacityLower(tour.Capacity, CapacitySearch);
            bool containsDuration = IsDurationMatch(tour.Duration, DurationSearch);

            return containsName && containsState && containsCity && containsDuration && capacityIsLower && containsLanguage;
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
            TourTouristDTO selectedTour = (TourTouristDTO)tourDataGrid.SelectedItem;
            
            if (selectedTour != null)
            {
                TourReservationForm form = new TourReservationForm(LoggedUser,selectedTour, _reservationService, _scheduleService);
                form.ShowDialog();
                
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TextboxName.Text = "";
            TextboxState.Text = "";
            TextboxCity.Text = "";
            TextboxDuration.Text = "";
            TextboxCapacity.Text = "";
            TextboxLanguage.Text = "";
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
    }
}
