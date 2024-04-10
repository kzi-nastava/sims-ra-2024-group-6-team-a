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
        public List<Model.Image> ListImages { get; set; }
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
        private string _imageAccommodation;
        public string ImageAccommodation
        {
            get => _imageAccommodation;
            set
            {
                if (value != _imageAccommodation)
                {
                    _imageAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _currentImageIndex = 0;
        public int CurrentImageIndex
        {
            get => _currentImageIndex;
            set
            {
                _currentImageIndex = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }
        public Model.Image CurrentImage => ListImages[CurrentImageIndex];

       

        public ObservableCollection<DateRanges> AvailableDates { get; set; }
        public DateRanges SelectedDates;

        private AccommodationReservationRepository _reservationRepository;

        public MakeReservation(AccommodationOwnerDTO SelectedAccommodation )
        {
            InitializeComponent();
            DataContext = this;
            ImageRepository _imageRepository = new ImageRepository();

            _accommodationId = SelectedAccommodation.Id;
            AccommodationName = SelectedAccommodation.Name;
            State = SelectedAccommodation.State;
            City = SelectedAccommodation.City;
            Type = SelectedAccommodation.Type.ToString();
            CancelationDays = SelectedAccommodation.CancelationDays.ToString();
            MaxGuests = SelectedAccommodation.MaxGuests.ToString();
            MinDays = SelectedAccommodation.MinReservationDays.ToString();
            List<Model.Image> lista = new List<Model.Image>(); 
            foreach (Model.Image image in _imageRepository.GetByEntity(SelectedAccommodation.Id, Enums.ImageType.Accommodation))
            {
                lista.Add(image);

            }
            ListImages = lista;

            _accommodationReservationRepository = new AccommodationReservationRepository();
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
                MessageBox.Show("The fields are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            SelectedDates = (DateRanges)DatesTable.SelectedItem;
            if (IsValid && SelectedDates != null)
            {
                //AccommodationReservation accommodationReservation= new AccommodationReservation(_accommodationId, 3, SelectedDates.CheckIn, SelectedDates.CheckOut, Convert.ToInt32(GuestNumber), Enums.ReservationStatus.Active);
              //  _accommodationReservationRepository.Save(accommodationReservation);
                MessageBox.Show("Successful booking!", "WELL DONE", MessageBoxButton.OK);

                MakeReservationView.Content = new MyReservation();

            }

            else MessageBox.Show("You must select date ranges!", "Select date", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
        private void Next_Image_click(object sender, RoutedEventArgs e)
        {
                if (CurrentImageIndex < ListImages.Count - 1)
                    CurrentImageIndex++;
        }
        private void Previous_Image_click(object sender, RoutedEventArgs e)
        {
            if (CurrentImageIndex > 0)
                CurrentImageIndex--;
        }


        private void FirstDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = DatePickerStartDate.SelectedDate;

            if (selectedDate.HasValue )
            {
                DatePickerEndDate.DisplayDateStart = selectedDate.Value.AddDays(Convert.ToInt32(DaysNumber));
                DatePickerEndDate.IsEnabled = true;

            }
        }
        private void TextboxDaysNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextboxDaysNumber.Text) && int.TryParse(TextboxDaysNumber.Text, out _))
            {
                    DatePickerStartDate.IsEnabled = true;
            }
            else
                DatePickerStartDate.IsEnabled = false;
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
                        return "Must be number!";

                    if (Convert.ToInt32(DaysNumber) < Convert.ToInt32(MinDays))
                    {
                        return "Must be a larger number!";
                    }
                }
                else if (columnName == "GuestNumber")
                {
                    if (string.IsNullOrEmpty(GuestNumber))
                        return "Required field";

                    Match match = _NumberRegex.Match(GuestNumber);
                    if (!match.Success)
                        return "Must be number!";

                    if (Convert.ToInt32(GuestNumber) > Convert.ToInt32(MaxGuests))
                    {
                        return "Must be a lower number!";
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
