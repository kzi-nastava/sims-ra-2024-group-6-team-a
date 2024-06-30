using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookingApp.ViewModels.OwnerViewModel
{
    public class ScheduleRenovationVM : INotifyPropertyChanged
    {
        private string title;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string duration;
        private string infoBox;
        List<DateOnly?> availableDates = new List<DateOnly?>();

        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }


        public string InfoBox
        {
            get { return infoBox; }
            set
            {
                if (infoBox != value)
                {
                    infoBox = value;
                    OnPropertyChanged(nameof(InfoBox));
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
        public AccommodationOwnerDTO Accommodation { get; set; }
        public List<ReservationOwnerDTO> Reservations { get; set; }
        public bool isPeriodFound;
        public ICommand FindCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand SelectStartDateCommand { get; }
        public ICommand SelectEndDateCommand { get; }
        public ICommand ConfirmCommand { get; }

        public ScheduleRenovationVM(AccommodationOwnerDTO Accommodation, List<ReservationOwnerDTO> Reservations)
        {
            this.Accommodation = Accommodation;
            this.Reservations = Reservations;
            InfoBox ="Choose the start and end date,aswell as the duration,and press Find, so that the system can find a suitable renovation period."
;
            isPeriodFound = false;
            FindCommand = new RelayCommand(FindDates);
            CloseCommand = new RelayCommand(CloseWindow);
            SelectStartDateCommand = new RelayCommand(SelectStartDate);
            SelectEndDateCommand = new RelayCommand(SelectEndDate);
            ConfirmCommand = new RelayCommand(ConfirmSchedule);
        }


        public void FindDates(object parameter)
        {
            int duration;
            if (StartDate != null && EndDate != null && int.TryParse(Duration, out duration))
            {
                availableDates = RenovationService.GetInstance().GetAvailableDatesForRenovation(Reservations, DateOnly.FromDateTime((DateTime)StartDate), DateOnly.FromDateTime((DateTime)EndDate), duration);
                if (availableDates[0] != null)
                {
                    isPeriodFound = true;
                    InfoBox =  "There is an available period between " + ((DateOnly)availableDates[0]).ToString("dd MMMM,yyyy") + " and " + ((DateOnly)availableDates[1]).ToString("dd MMMM,yyyy");
                    
                }
                else
                {
                    isPeriodFound = true;
                    InfoBox = "There is no available period between selected dates for the duration.";
                    
                }
            }
            else
            {
                
                isPeriodFound = true;
                InfoBox =  "Please make sure you have selected dates and entered a number for the duration.";
                
            }
        }

        internal void ConfirmSchedule(object parameter)
        {
            int duration;
            if (isPeriodFound && StartDate != null && EndDate != null && int.TryParse(Duration, out duration))
            {
                if (MessageBox.Show("Confirm renovation?", "Schedule", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    AccommodationRenovation renovation = new AccommodationRenovation(RenovationService.GetInstance().NextId(), Title, Accommodation.Id, (DateOnly)availableDates[0], (DateOnly)availableDates[1]);
                    RenovationService.GetInstance().Save(renovation);
                    CloseWindow(Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));

                }
            }
            else
            {
                InfoBox = "Please find a period first using dates and a duration.";
            }
        }

        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }

        private void SelectStartDate(object parameter)
        {
            if (parameter is DatePicker datePicker)
            {
                datePicker.IsDropDownOpen = true;
            }
        }

        private void SelectEndDate(object parameter)
        {
            if (parameter is DatePicker datePicker)
            {
                datePicker.IsDropDownOpen = true;
            }
        }
    }
}
