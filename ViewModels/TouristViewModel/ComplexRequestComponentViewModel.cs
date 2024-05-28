using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class ComplexRequestComponentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<LanguageDTO> Languages { get; set; } = new ObservableCollection<LanguageDTO>();
        public ObservableCollection<LocationDTO> Locations { get; set; } = new ObservableCollection<LocationDTO>();
        public Action CloseAction { get; set; }
        public SimpleRequestDTO SelectedRequest { get; set; } = new SimpleRequestDTO();

        private DateTime _start;
        public DateTime Start
        {
            get
            {
                return _start;
            }

            set
            {
                if (value != _start)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                    SelectedRequest.Start = DateOnly.FromDateTime(value);
                }
            }
        }
        private DateTime _end;
        public DateTime End
        {
            get
            {
                return _end;
            }

            set
            {
                if (value != _end)
                {
                    _end = value;
                    OnPropertyChanged("End");
                    SelectedRequest.End = DateOnly.FromDateTime(value);
                }
            }
        }
        public User LoggedUser { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged("Surname"); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        private int _selectedLanguageIndex = -1;
        public int SelectedLanguageIndex
        {
            get { return _selectedLanguageIndex; }
            set
            {
                _selectedLanguageIndex = value;
                OnPropertyChanged(nameof(SelectedLanguageIndex));
            }
        }

        private int _selectedLocationIndex = -1;
        public int SelectedLocationIndex
        {
            get { return _selectedLocationIndex; }
            set
            {
                _selectedLocationIndex = value;
                OnPropertyChanged(nameof(SelectedLocationIndex));
            }
        }
        
        public RelayCommand AddTouristInfoCommand { get; set; }
        public RelayCommand RemoveTouristCommand { get; set; }
        public RelayCommand SaveReservationCommand { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }

        public ComplexRequestViewModel ParentWindow { get; set; }
        public ComplexRequestComponentViewModel(User user, ComplexRequestViewModel parentWindow)
        {
            LoggedUser = user;
            this.ParentWindow = parentWindow;

            AddTouristInfoCommand = new RelayCommand(Execute_AddTouristInfoCommand);
            RemoveTouristCommand = new RelayCommand(RemoveTourist);
            SaveReservationCommand = new RelayCommand(Execute_SaveReservationCommand);
            CancelReservationCommand = new RelayCommand(Execute_CancelReservationCommand);

            SetLanguages();
            SetLocations();
            AddUserInfo();
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
        private void AddUserInfo()
        {
            Tourist tourist = TouristService.GetInstance().GetByTouristId(LoggedUser.Id);
            SelectedRequest.Guests.Add(new TourGuestDTO(tourist.Name, tourist.Age, tourist.Surname, tourist.UserId));
        }

        private void Execute_SaveReservationCommand(object obj)
        {
            SelectedRequest.LocationId = SelectedLocationIndex + 1;
            SelectedRequest.LanguageId = SelectedLanguageIndex + 1;
            Location location = LocationService.GetInstance().GetById(SelectedRequest.LocationId);
            Language language = LanguageService.GetInstance().GetById(SelectedRequest.LanguageId);
            SelectedRequest.Location = location.City + ", " + location.State;
            SelectedRequest.Language = language.Name;
            SelectedRequest.TouristId = LoggedUser.Id;
            SelectedRequest.Status = Resources.Enums.RequestStatus.Onhold;
            ParentWindow.SimpleRequests.Add(SelectedRequest);
            CloseAction();

        }
        private void Execute_CancelReservationCommand(object sender)
        {
            CloseAction();
        }

        private void Execute_AddTouristInfoCommand(object sender)
        {
            SelectedRequest.Guests.Add(new TourGuestDTO(Name, Age, Surname, -1));
            ClearInputFields();
        }
        private void ClearInputFields()
        {
            Name = "";
            Surname = "";
            Age = 0;
        }
        private void RemoveTourist(object parameter)
        {
            var touristToRemove = parameter as TourGuestDTO;
            SelectedRequest.Guests.Remove(touristToRemove);
        }


    }
}
