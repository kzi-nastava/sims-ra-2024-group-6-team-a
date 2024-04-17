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
        public NavigationService NavService { get; set; }
        public GuestAccommodationsViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;
            NavService = navigation;
            SearchCommand = new RelayCommand(Execute_SearchCommand);
            SearchResetCommand = new RelayCommand(Execute_SearchResetCommand);
            ReserveCommand = new RelayCommand(Execute_ReserveCommand);
            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            Update();
        }
        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in AccommodationService.GetInstance().GetAll())
            {
                Model.Image image = new Model.Image();
                foreach (Model.Image i in ImageService.GetInstance().GetByEntity(accommodation.Id, Enums.ImageType.Accommodation))
                {
                    image = i;
                    break;
                }
                Accommodations.Add(new AccommodationOwnerDTO(accommodation, LocationService.GetInstance().GetByAccommodation(accommodation), image.Path));
            }
        }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand SearchResetCommand { get; set; }
        public RelayCommand ReserveCommand { get; set; }
        private string searchCity;
        private string searchType;
        private string searchName;
        private string searchState;
        private string searchGuestNumber;
        private string searchDaysNumber;
        public string SearchCity
        {
            get { return searchCity; }
            set
            {
                searchCity = value;
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
                ObservableCollection<AccommodationOwnerDTO> result = Accommodations;
                if (!string.IsNullOrEmpty(searchCity))
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.city.ToLower().Contains(searchCity.ToLower())));
                if (!string.IsNullOrEmpty(searchState))
                    result = new ObservableCollection<AccommodationOwnerDTO>(result.Where(a => a.State.ToLower().Contains(searchState.ToLower())));
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
            SearchState = null;
            SearchCity = null;
            SearchType = null;
            SearchGuestNumber = null;
            SearchDaysNumber = null;
            OnPropertyChanged(nameof(SearchName));
            OnPropertyChanged(nameof(SearchState));
            OnPropertyChanged(nameof(SearchCity));
            OnPropertyChanged(nameof(SearchType));
            OnPropertyChanged(nameof(SearchGuestNumber));
            OnPropertyChanged(nameof(SearchDaysNumber));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
