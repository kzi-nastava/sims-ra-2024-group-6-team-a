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

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class GuestMyReservationsViewModel
    {
        public ObservableCollection<ReservationGuestDTO> Reservations { get; set; }
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand SeeMoreCommand { get; set; }
        public RelayCommand MyRequestCommand { get; set; }
        private AccommodationReservationRepository accommodationReservationRepository;
        public GuestMyReservationsViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;
            Reservations = new ObservableCollection<ReservationGuestDTO>();
            SeeMoreCommand = new RelayCommand(Execute_SeeMoreCommand);
            MyRequestCommand = new RelayCommand(Execute_MyRequestCommand);
            NavService = navigation;
            Update();
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
        private void Execute_MyRequestCommand(object obj)
        {
            NavService.Navigate(new GuestMyRequestView(Guest, NavService));
        }
        private void Execute_SeeMoreCommand(object obj)
        {
            ReservationGuestDTO reservationGuestDTO = obj as ReservationGuestDTO;
            NavService.Navigate(new ReservationSeeMoreView(reservationGuestDTO, Guest, NavService));
        }
    }
}
