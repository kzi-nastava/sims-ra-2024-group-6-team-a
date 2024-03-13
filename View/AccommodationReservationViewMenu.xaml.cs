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
        public Accommodation SelectedAccommodation { get; set; }
        private string searchLocation;
        private string searchType;
        private string searchName;
        private string searchState;
        private string searchGuestNumber;
        private string searchDaysNumber;
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

        public ObservableCollection<AccommodationOwnerDTO> filteredData
        {
            get
            {
                ObservableCollection<AccommodationOwnerDTO>  result = Accommodations;

                if (!string.IsNullOrEmpty(searchLocation))
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.city.ToLower().Contains(searchLocation)));
                }
                if (!string.IsNullOrEmpty(searchState))
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.State.ToLower().Contains(searchState)));
                }

                if (!string.IsNullOrEmpty(searchType))
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.Type.ToString().ToLower().Contains(searchType)));
                }

                if (!string.IsNullOrEmpty(searchName))
                {
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.Name.ToLower().Contains(searchName)));
                }

                if (!string.IsNullOrEmpty(searchGuestNumber))
                {
                    int a;
                    bool number = int.TryParse(searchGuestNumber,out a);
                    if(number)
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.MaxGuests >= Convert.ToInt32(searchGuestNumber)));
                }
                if(!string.IsNullOrEmpty(searchDaysNumber))
                {
                    int a;
                    bool number = int.TryParse(searchDaysNumber, out a);
                    if (number)
                        result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.MinReservationDays <= Convert.ToInt32(searchDaysNumber)));
                }

                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
