using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Validation;
using BookingApp.View;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class SimpleRequestViewModel : ValidationBase, INotifyPropertyChanged
    {
        public static ObservableCollection<TourGuestDTO> TourGuests { get; set; } = new ObservableCollection<TourGuestDTO>();
        public ObservableCollection<LanguageDTO> Languages { get; set; } = new ObservableCollection<LanguageDTO>();
        public ObservableCollection<LocationDTO> Locations { get; set; } = new ObservableCollection<LocationDTO>();
        public Action CloseAction { get; set; }
        public SimpleRequestDTO SelectedRequest { get; set; } = new SimpleRequestDTO();
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
                if (_selectedLanguageIndex >= 0 && _selectedLanguageIndex < Languages.Count)
                {
                    SelectedRequest.LanguageId = Languages[_selectedLanguageIndex].Id;
                }
                else
                {
                    SelectedRequest.LanguageId = -1; 
                }
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
                if (_selectedLocationIndex >= 0 && _selectedLocationIndex < Locations.Count)
                {
                    SelectedRequest.LocationId = Locations[_selectedLocationIndex].Id;
                }
                else
                {
                    SelectedRequest.LocationId = -1;
                }
            }
        }
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private bool _isInputDataValid;

        private bool _isTouristInfoValid;

        private string beginDateInput { get; set; }
        private string endDateInput { get; set; }
        public RelayCommand AddTouristInfoCommand { get; set; }
        public RelayCommand RemoveTouristCommand { get; set; }
        public RelayCommand SaveReservationCommand { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }

        public SimpleRequestViewModel(User user)
        {
            LoggedUser = user;

            AddTouristInfoCommand = new RelayCommand(Execute_AddTouristInfoCommand);
            RemoveTouristCommand = new RelayCommand(RemoveTourist);
            SaveReservationCommand = new RelayCommand(Execute_SaveReservationCommand);
            CancelReservationCommand = new RelayCommand(Execute_CancelReservationCommand);
            _isInputDataValid = false;
            _isTouristInfoValid = false;
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
            TourGuests.Clear();
            Tourist tourist = TouristService.GetInstance().GetByTouristId(LoggedUser.Id);
            TourGuests.Add(new TourGuestDTO(tourist.Name, tourist.Age, tourist.Surname, tourist.UserId));
        }
        private void Execute_SaveReservationCommand(object sender)
        {

            SelectedRequest.LocationId = SelectedLocationIndex + 1;
            SelectedRequest.LanguageId = SelectedLanguageIndex + 1;
            SelectedRequest.TouristId = LoggedUser.Id;
            SelectedRequest.Status = Resources.Enums.RequestStatus.Onhold;
            SelectedRequest.ComplexRequestId = -1;
            _isInputDataValid = true;
            _isTouristInfoValid = false;
            Validate();
            if (IsValid)
            {
                TourRequestService.GetInstance().MakeTourRequest(SelectedRequest, TourGuests.ToList(), LoggedUser);
                MessageBox.Show("Request successfully created!");
                CloseAction();
            }
            
        }
        private void Execute_CancelReservationCommand(object sender)
        {
            CloseAction();
        }

        private void Execute_AddTouristInfoCommand(object sender)
        {
            _isInputDataValid = false;
            _isTouristInfoValid = true;
            Validate();
            if(IsValid)
            {

                TourGuests.Add(new TourGuestDTO(Name, Age, Surname, -1));
                ClearInputFields();
            }
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
            TourGuests.Remove(touristToRemove);
        }

        protected override void ValidateSelf()
        {
           
            ValidationErrors.Clear();

            if (_isTouristInfoValid)
            {
                if (string.IsNullOrWhiteSpace(Name))
                    ValidationErrors[nameof(Name)] = "Name is required.";

                if (string.IsNullOrWhiteSpace(Surname))
                    ValidationErrors[nameof(Surname)] = "Surname is required.";

                if (Age <= 0)
                    ValidationErrors[nameof(Age)] = "Enter a valid age number.";
            }


            if (_isInputDataValid)
            {
               
                if (SelectedRequest.LocationId == -1 || !Locations.Any(l => l.Id == SelectedRequest.LocationId))
                {
                    ValidationErrors["Location"] = "Select valid location from the list.";
                }

                if (SelectedRequest.LanguageId == -1 || !Locations.Any(l => l.Id == SelectedRequest.LanguageId))
                {
                    ValidationErrors["Language"] = "Select valid language from the list.";
                }
                if (string.IsNullOrWhiteSpace(SelectedRequest.Description))
                {
                    ValidationErrors["Description"] = "Description is required.";
                }

                if (SelectedRequest.Start > SelectedRequest.End)
                {
                    ValidationErrors["EndDate"] = "Invalid interval: end date must come after start.";
                }

                if (string.IsNullOrWhiteSpace(SelectedRequest.Start.ToString()) || SelectedRequest.Start == default)
                {
                    ValidationErrors["StartDate"] = "Start date is required.";
                }

                if (string.IsNullOrWhiteSpace(SelectedRequest.End.ToString()) || SelectedRequest.End == default)
                {
                    ValidationErrors["EndDate"] = "End date is required.";
                }

            }

            OnPropertyChanged(nameof(ValidationErrors));
        }
    }
}
