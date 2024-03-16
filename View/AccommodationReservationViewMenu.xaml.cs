using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationViewMenu.xaml
    /// </summary>
    public partial class AccommodationReservationViewMenu : Window, IObserver, INotifyPropertyChanged
    {
        private AccommodationRepository _repository;
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;

        public static ObservableCollection<AccommodationOwnerDTO> Accommodations { get; set; }
        public AccommodationOwnerDTO SelectedAccommodation { get; set; }

        private string searchLocation = "Search city...";
        private string searchType = "Search type...";
        private string searchName= "Search name...";
        private string searchState = "Search state...";
        private string searchGuestNumber = "Search number of guest...";
        private string searchDaysNumber = "Search number of days...";
        public AccommodationReservationViewMenu(LocationRepository locationRepository, ImageRepository imageRepository)
        {
            InitializeComponent();
            DataContext = this;
            this._locationRepository = locationRepository;
            this._imageRepository = imageRepository;

         
            Title = "Accommodation registration";
            _repository = new AccommodationRepository();
            _repository.Subscribe(this);       
            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            Update();
        }
       
        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in _repository.GetAll())
            {
                Model.Image image = new Model.Image();
                foreach (Model.Image i in _imageRepository.GetByEntity(accommodation.Id, Enums.ImageType.Accommodation))
                {
                    image = i;
                    break;
                }
                Accommodations.Add(new AccommodationOwnerDTO(accommodation, _locationRepository.GetByAccommodation(accommodation), image.Path));
            }
        }

        public string SearchLocation
        {
            get { return searchLocation; }
            set
            {
                searchLocation = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        public string SearchState
        {
            get { return searchState; }
            set
            {
                searchState = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        

        public string SearchName
        {
            get { return searchName; }
            set
            {
                searchName = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        public string SearchType
        {
            get { return searchType; }
            set
            {
                searchType = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        public string SearchGuestNumber
        {
            get { return searchGuestNumber; }
            set
            {
                searchGuestNumber = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        public string SearchDaysNumber
        {
            get { return searchDaysNumber; }
            set
            {
                searchDaysNumber = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        private void TextBox_GotFocusName(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Search name...")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocusName(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Search name...";
                textBox.Foreground = Brushes.Gray; 
            }
        }
        private void TextBox_GotFocusType(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Search type...")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black; 
            }
        }

        private void TextBox_LostFocusType(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Search type...";
                textBox.Foreground = Brushes.Gray; 
            }
        }
        private void TextBox_GotFocusCity(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Search city...")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black; 
            }
        }

        private void TextBox_LostFocusCity(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Search city...";
                textBox.Foreground = Brushes.Gray; 
            }
        }

        private void TextBox_GotFocusState(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Search state...")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black; 
            }
        }

        private void TextBox_LostFocusState(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Search state...";
                textBox.Foreground = Brushes.Gray; 
            }
        }
        private void TextBox_GotFocusGuest(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Search number of guest...")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocusGuest(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Search number of guest...";
                textBox.Foreground = Brushes.Gray; 
            }
        }
        private void TextBox_GotFocusDays(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Search number of days...")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black; 
            }
        }

        private void TextBox_LostFocusDays(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Search number of days...";
                textBox.Foreground = Brushes.Gray;
            }
        }
        public ObservableCollection<AccommodationOwnerDTO> filteredData
        {
            get
            {
                ObservableCollection<AccommodationOwnerDTO> result = Accommodations;

                if (!string.IsNullOrEmpty(searchLocation) && searchLocation != "Search city...")
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.city.ToLower().Contains(searchLocation.ToLower())));
                }
                if (!string.IsNullOrEmpty(searchState) && searchState  != "Search state...")
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.State.ToLower().Contains(searchState.ToLower())));
                }

                if (!string.IsNullOrEmpty(searchType) && searchType  != "Search type...")
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.Type.ToString().ToLower().Contains(searchType.ToLower())));
                }

                if (!string.IsNullOrEmpty(searchName) && searchName != "Search name...")
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.Name.ToLower().Contains(searchName.ToLower())));
                }

                if (!string.IsNullOrEmpty(searchGuestNumber) && searchGuestNumber  != "Search number of guest...")
                {
                    int a;
                    bool number = int.TryParse(searchGuestNumber,out a);
                    if(number)
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.MaxGuests >= Convert.ToInt32(searchGuestNumber.ToLower())));
                }
                if(!string.IsNullOrEmpty(searchDaysNumber) && searchDaysNumber  != "Search number of days...")
                {
                    int a;
                    bool number = int.TryParse(searchDaysNumber, out a);
                    if (number)
                        result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.MinReservationDays <= Convert.ToInt32(searchDaysNumber.ToLower())));
                }

                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_ClickMyReservation(object sender, RoutedEventArgs e)
        {
            MainView.Content = new MyReservation();
        }

        private void Button_ClickMakeReservation(object sender, RoutedEventArgs e)
        {
            AccommodationOwnerDTO accommodation = ((Button)sender).DataContext as AccommodationOwnerDTO;
            MainView.Content = new MakeReservation(accommodation);
        }

        private void Button_ClickHomePage(object sender, RoutedEventArgs e)
        {
            //MainView.Content = new AccommodationReservationViewMenu(_locationRepository, _imageRepository);

            AccommodationReservationViewMenu accommodationReservationViewMenu = new AccommodationReservationViewMenu(_locationRepository, _imageRepository);
            accommodationReservationViewMenu.Show();
        }
    }
}
