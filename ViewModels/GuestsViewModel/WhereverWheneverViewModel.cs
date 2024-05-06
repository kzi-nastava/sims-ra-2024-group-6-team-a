using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using BookingApp.ApplicationServices;

namespace BookingApp.ViewModels.GuestsViewModel 
{
    public class WhereverWheneverViewModel : INotifyPropertyChanged
    {
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public WhereverWheneverView ReservationView { get; set; }
        public ObservableCollection<GuestWheneverDTO> Accommodations { get; set; }
        public RelayCommand ReserveCommand { get; set; }
        public RelayCommand FindAccommodationsCommand { get; set; }
        public RelayCommand FirstDateCommand { get; set; }
        public WhereverWheneverViewModel(Guest guest, WhereverWheneverView reservationView, NavigationService navigation)
        {
            Guest = guest;
            ReservationView = reservationView;
            NavService = navigation;
            Accommodations = new ObservableCollection<GuestWheneverDTO>();
            FirstDateCommand = new RelayCommand(Execute_FirstDateCommand);
            ReserveCommand = new RelayCommand(Execute_ReserveCommand);
            FindAccommodationsCommand = new RelayCommand(Execute_FindAccommodationsCommand);
            AvaibleDatesVisibility = Visibility.Collapsed;

        }

        public void Execute_FirstDateCommand(object obj)
        {
            ReservationView.LastDatePicker.IsEnabled = true;
            DateTime? firstDatePicekr = ReservationView.FirstDatePicker.SelectedDate;
            if (firstDatePicekr.HasValue) ReservationView.LastDatePicker.DisplayDateStart = firstDatePicekr.Value.AddDays(1);
        }  
        public void Execute_FindAccommodationsCommand(object obj)
        {
            DateTime? firstDatePicekr = ReservationView.FirstDatePicker.SelectedDate;
            DateTime? lastDatePicekr = ReservationView.LastDatePicker.SelectedDate;
            if (firstDatePicekr == null && Convert.ToInt32(DaysNumber) > 0 && Convert.ToInt32(GuestNumber) > 0)
            {
                List<GuestWheneverDTO> accommodations = AccommodationService.GetInstance().GetAvailableAccommodationsWithoutDates(Convert.ToInt32(GuestNumber), Convert.ToInt32(DaysNumber));
                Accommodations.Clear();
                foreach (GuestWheneverDTO accommodation in accommodations)
                    Accommodations.Add(accommodation);
                AvaibleDatesVisibility = Visibility.Visible;
                return;
            } else if  (firstDatePicekr.HasValue && Convert.ToInt32(DaysNumber) > 0 && Convert.ToInt32(GuestNumber) > 0 && lastDatePicekr.HasValue){
                DateOnly firstDate = DateOnly.FromDateTime((DateTime)firstDatePicekr);
                DateOnly lastDate = DateOnly.FromDateTime((DateTime)lastDatePicekr);
                if (lastDate < firstDate.AddDays(Convert.ToInt32(DaysNumber))) {
                    MessageBox.Show("The end date must be greater than the start date by a minimum number of days", "Date entry error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Accommodations.Clear();
                }else{ 
                    List<GuestWheneverDTO> accommodations = AccommodationService.GetInstance().GetAvailableAccommodations(firstDate, lastDate, Convert.ToInt32(GuestNumber), Convert.ToInt32(DaysNumber));
                    Accommodations.Clear();
                    foreach (GuestWheneverDTO accommodation in accommodations)
                        Accommodations.Add(accommodation);
                    AvaibleDatesVisibility = Visibility.Visible;
                }
            } else  MessageBox.Show("The fields are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        public void Execute_ReserveCommand(object obj)
        {
            if (Guest.BonusPoints != 0) Guest.BonusPoints--;
            GuestService.GetInstance().Update(Guest);
            GuestWheneverDTO guestWheneverDTO = obj as GuestWheneverDTO;
            AccommodationReservation accommodationReservation = new AccommodationReservation(guestWheneverDTO.AccommodationId, Guest.Id, guestWheneverDTO.CheckIn, guestWheneverDTO.CheckOut, Convert.ToInt32(GuestNumber), Enums.ReservationStatus.Active, AccommodationService.GetInstance().GetByReservationId(guestWheneverDTO.AccommodationId));
            AccommodationReservationService.GetInstance().Save(accommodationReservation);
            MessageBox.Show("Successful booking!", "WELL DONE", MessageBoxButton.OK);
            NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
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
        private Visibility avaibleDatesVisibility;
        public Visibility AvaibleDatesVisibility
        {
            get { return avaibleDatesVisibility; }
            set
            {
                if (avaibleDatesVisibility != value)
                {
                    avaibleDatesVisibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    }
}
