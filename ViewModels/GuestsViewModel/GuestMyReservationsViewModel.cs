using BookingApp.DTOs;
using BookingApp.View.GuestViews;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using BookingApp.ApplicationServices;
using System.Windows.Controls;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class GuestMyReservationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ReservationGuestDTO> Reservations { get; set; }
        public Guest Guest { get; set; }
        public GuestMyReservationsView ReservationView { get; set; }

        public NavigationService NavService { get; set; }
        public RelayCommand SeeMoreCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand FirstDateCommand { get; set; }

        public GuestMyReservationsViewModel(Guest guest, GuestMyReservationsView reservationView, NavigationService navigation)
        {
            Guest = guest;
            ReservationView = reservationView;

            Reservations = new ObservableCollection<ReservationGuestDTO>();
            SeeMoreCommand = new RelayCommand(Execute_SeeMoreCommand);
            FirstDateCommand = new RelayCommand(Execute_FirstDateCommand);
            CreateCommand = new RelayCommand(Execute_CreateCommand);

            NavService = navigation;
            Update();
        }
        public void Execute_FirstDateCommand(object obj)
        {
            ReservationView.LastDatePicker.IsEnabled = true;
            DateTime? firstDatePicekr = ReservationView.FirstDatePicker.SelectedDate;
            if (firstDatePicekr.HasValue) ReservationView.LastDatePicker.DisplayDateStart = firstDatePicekr.Value.AddDays(1);
        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetActiveReservationsByGuest(Guest.Id))
            {
                Model.Image image = new Model.Image();
                foreach (Model.Image i in ImageService.GetInstance().GetByEntity(reservation.AccommodationId, Enums.ImageType.Accommodation))
                {
                    image = i;
                    break;
                }
                Accommodation accommodation = AccommodationService.GetInstance().GetByReservationId(reservation.AccommodationId);
                Location location = LocationService.GetInstance().GetByAccommodation(accommodation);
                Reservations.Add(new ReservationGuestDTO(Guest, reservation, accommodation, location, image.Path));
            }
        }
        public void Execute_CreateCommand(object obj)
        {
            if (obj is Button button)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }
        private void Execute_SeeMoreCommand(object obj)
        {
            ReservationGuestDTO reservationGuestDTO = obj as ReservationGuestDTO;
            NavService.Navigate(new ReservationSeeMoreView(reservationGuestDTO, Guest, NavService));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
