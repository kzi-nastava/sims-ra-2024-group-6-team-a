using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.GuestViews;
using BookingApp.View;
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
using System.Windows.Navigation;
using BookingApp.ApplicationServices;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public partial class GuestAccommodationsViewModel : Window, IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<AccommodationOwnerDTO> Accommodations { get; set; }
        public Guest Guest { get; set; }
        public List<String> Cities { get; set; }
        public List<String> States { get; set; }

        public NavigationService NavService { get; set; }
        public GuestAccommodationsViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;
            NavService = navigation;
            States = new List<String>();
            Cities = new List<String>();
            SearchCommand = new RelayCommand(Execute_SearchCommand);
            SearchResetCommand = new RelayCommand(Execute_SearchResetCommand);
            ReserveCommand = new RelayCommand(Execute_ReserveCommand);
            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            Update();
        }
        public void Update()
        {
            AddCities();
            AddStates();
            Accommodations.Clear();
            foreach (Accommodation accommodation in AccommodationService.GetInstance().GetAll())
            {
                bool hasRenovation= false;
                Model.Image image = new Model.Image();
                foreach (Model.Image i in ImageService.GetInstance().GetByEntity(accommodation.Id, Enums.ImageType.Accommodation))
                {
                    image = i;
                    break;
                }
                foreach (AccommodationRenovation renovation in RenovationService.GetInstance().GetAll())
                {
                    if(renovation.AccommodationId== accommodation.Id && DateOnly.FromDateTime(DateTime.Today) <= renovation.EndDate && DateOnly.FromDateTime(DateTime.Today) >= renovation.StartDate)
                        hasRenovation= true;
                }
                    Accommodations.Add(new AccommodationOwnerDTO(accommodation, LocationService.GetInstance().GetByAccommodation(accommodation), image.Path, hasRenovation));
            }
        }
        private void AddCities()
        {
            Cities.Clear();
            if (SelectedState != null) {
                foreach (Location location in LocationService.GetInstance().GetAll())
                {
                    if(SelectedState == location.State)
                    Cities.Add(location.City);
                }
                return;
            }

            foreach (Location location in LocationService.GetInstance().GetAll())
            {
                Cities.Add(location.City);
            }
        }  
        private void AddStates()
        {
            foreach (Location location in LocationService.GetInstance().GetAll())
            {
                if (!States.Contains(location.State))
                {
                    States.Add(location.State);
                }
            }
        }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand SearchResetCommand { get; set; }
        public RelayCommand ReserveCommand { get; set; }
        private string selectedCity;
        private string searchType;
        private string searchName;
        private string selectedState;
        private string searchGuestNumber;
        private string searchDaysNumber;
        public string SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }
        public string SelectedState
        {
            get { return selectedState; }
            set
            {
                selectedState = value;
                OnPropertyChanged(nameof(filteredData));
                AddCities();
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
                AddCities();
                ObservableCollection<AccommodationOwnerDTO> result = Accommodations;
                if (!string.IsNullOrEmpty(SelectedCity))
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.city.ToLower().Contains(SelectedCity.ToLower())));
                if (!string.IsNullOrEmpty(SelectedState))
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.State.ToLower().Contains(SelectedState.ToLower())));
                if (!string.IsNullOrEmpty(searchType))
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.Type.ToString().ToLower().Contains(searchType.ToLower())));
                if (!string.IsNullOrEmpty(searchName))
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.Name.ToLower().Contains(searchName.ToLower())));
                if (!string.IsNullOrEmpty(searchGuestNumber))
                        result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.MaxGuests >= Convert.ToInt32(searchGuestNumber.ToLower())));
                if (!string.IsNullOrEmpty(searchDaysNumber))
                        result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.MinReservationDays <= Convert.ToInt32(searchDaysNumber.ToLower())));
                return result;
            }
        }
        public void Execute_SearchCommand(object obj)
        {
            if (obj is Button button)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }
        public void Execute_ReserveCommand(object obj)
        {
            AccommodationOwnerDTO accommodationDTO = obj as AccommodationOwnerDTO;
            NavService.Navigate(new AccommodationReservationView(accommodationDTO, Guest, NavService));
        }
        public void Execute_SearchResetCommand(object obj)
        {
            SearchName = null;
            SelectedState = null;
            SelectedCity = null;
            SearchType = null;
            SearchGuestNumber = null;
            SearchDaysNumber = null;
            OnPropertyChanged(nameof(SearchName));
            OnPropertyChanged(nameof(SelectedState));
            OnPropertyChanged(nameof(SelectedCity));
            OnPropertyChanged(nameof(SearchType));
            OnPropertyChanged(nameof(SearchGuestNumber));
            OnPropertyChanged(nameof(SearchDaysNumber));
            AddCities();
        }
        private Visibility reserveVisibility;
        public Visibility ReserveVisibility
        {
            get { return reserveVisibility; }
            set
            {
                if (reserveVisibility != value)
                {
                    reserveVisibility = value;
                    OnPropertyChanged(nameof(ReserveVisibility));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
