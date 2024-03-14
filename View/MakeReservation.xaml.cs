using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for MakeReservation.xaml
    /// </summary>
    public partial class MakeReservation : Page, INotifyPropertyChanged, IDataErrorInfo
    {
        private ImageRepository _imageRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;

        private int _accommodationId;

        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _state;
        public string State
        {
            get => _state;
            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minDays;
        public string MinDays
        {
            get => _minDays;
            set
            {
                if (value != _minDays)
                {
                    _minDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _cancelationDays;
        public string CancelationDays
        {
            get => _cancelationDays;
            set
            {
                if (value != _cancelationDays)
                {
                    _cancelationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _firstDate;
        public string FirstDate
        {
            get => _firstDate;
            set
            {
                if (value != _firstDate)
                {
                    _firstDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastDate;
        public string LastDate
        {
            get => _lastDate;
            set
            {
                if (value != _lastDate)
                {
                    _lastDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _guestNumber;
        public string GuestNumber
        {
            get => _guestNumber;
            set
            {
                if (value != _guestNumber)
                {
                    _guestNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _daysNumber;
        public string DaysNumber
        {
            get => _daysNumber;
            set
            {
                if (value != _daysNumber)
                {
                    _daysNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<DateRanges> AvailableDates { get; set; }
        public DateRanges SelectedDates;

        private AccommodationReservationRepository _reservationRepository;

        public MakeReservation(AccommodationOwnerDTO SelectedAccommodation)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationId = SelectedAccommodation.Id;
            AccommodationName = SelectedAccommodation.Name;
            State = SelectedAccommodation.State;
            City = SelectedAccommodation.City;
            Type = SelectedAccommodation.Type.ToString();
            CancelationDays = SelectedAccommodation.CancelationDays.ToString();
            MaxGuests = SelectedAccommodation.MaxGuests.ToString();
            MinDays = SelectedAccommodation.MinReservationDays.ToString();

            _accommodationReservationRepository= new AccommodationReservationRepository();
           // _accommodationReservationRepository.Subscribe(this);

            AvailableDates = new ObservableCollection<DateRanges>();

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FindDates_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid && (!string.IsNullOrEmpty(DatePickerStartDate.Text) && !string.IsNullOrEmpty(DatePickerEndDate.Text)))
            {
                List<DateRanges> availableDates = new List<DateRanges>();
                availableDates = _accommodationReservationRepository.GetAvailableDates(DateOnly.Parse(DatePickerStartDate.Text), DateOnly.Parse(DatePickerEndDate.Text), Convert.ToInt32(TextboxDaysNumber.Text), _accommodationId);
                AvailableDates.Clear();
                foreach (DateRanges dateRange in availableDates) 
                {
                     AvailableDates.Add(dateRange);
                }
            }
            else {
                MessageBox.Show("The fields are not filled in correctly!");
            }
        }
        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            SelectedDates = (DateRanges)DatesTable.SelectedItem;
            if (IsValid && SelectedDates != null)
            {
                AccommodationReservation accommodationReservation= new AccommodationReservation(_accommodationId, 3, SelectedDates.CheckIn, SelectedDates.CheckOut, Convert.ToInt32(GuestNumber), Enums.ReservationStatus.Active);
                _accommodationReservationRepository.Save(accommodationReservation);
                MessageBox.Show("Successful booking!");
                MakeReservationView.Content = new MyReservation();

            }
            else MessageBox.Show("you must select date ranges!");

        }

        private void FirstDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = DatePickerStartDate.SelectedDate;

            if (selectedDate.HasValue)
            {
                DatePickerEndDate.DisplayDateStart = selectedDate.Value;
                DatePickerEndDate.IsEnabled = true;

            }

         
        }
      
        private Regex _NumberRegex = new Regex("[1-9]+");

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
        
                 if (columnName == "DaysNumber")
                {
                    if (string.IsNullOrEmpty(DaysNumber))
                        return "Required field";

                    Match match = _NumberRegex.Match(DaysNumber);
                    if (!match.Success)
                        return "Number!!!";

                    if (Convert.ToInt32(DaysNumber) < Convert.ToInt32(MinDays))
                    {
                        return "Mora biti veci od min dozvoljenog broja dana";
                    }
                }
                else if (columnName == "GuestNumber")
                {
                    if (string.IsNullOrEmpty(GuestNumber))
                        return "Required field";

                    Match match = _NumberRegex.Match(GuestNumber);
                    if (!match.Success)
                        return "Broj gostiju je prirodan broj";

                    if (Convert.ToInt32(GuestNumber) > Convert.ToInt32(MaxGuests))
                    {
                        return "Mora biti manji od maks dozvoljenog broja gostiju";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validnaObelezja = {"DaysNumber", "GuestNumber" };


        public bool IsValid
        {
            get
            {
                foreach (var obelezje in _validnaObelezja)
                {
                    if (this[obelezje] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
