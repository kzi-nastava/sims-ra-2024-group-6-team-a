using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using BookingApp.ApplicationServices;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class ReservationSeeMoreViewModel : INotifyPropertyChanged
    {
        public ReservationGuestDTO Reservation { get; set; }
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public ReservationSeeMoreView ReservationView { get; set; }
        public List<Model.Image> ListImages { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }
        public RelayCommand MoveReservationComand { get; set; }
        public RelayCommand RatePageCommand { get; set; }
        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }
        public ReservationSeeMoreViewModel(Guest guest, ReservationGuestDTO SelectedReservation, ReservationSeeMoreView reservationView, NavigationService navigation)
        {
            Guest = guest;
            Reservation = SelectedReservation;
            ReservationView = reservationView;
            NavService = navigation;
            List<Model.Image> lista = new List<Model.Image>();
            foreach (Model.Image image in ImageService.GetInstance().GetByEntity(SelectedReservation.AccommodationId, Enums.ImageType.Accommodation))
            {
                lista.Add(image);
            }
            ListImages = lista;
            NextImageCommand = new RelayCommand(Execute_NextImageCommand);
            PreviousImageCommand = new RelayCommand(Execute_PreviousImageCommand);
            CancelReservationCommand = new RelayCommand(Execute_CancelReservationCommand);
            RatePageCommand = new RelayCommand(Execute_RatePageCommand);
            MoveReservationComand = new RelayCommand(Execute_MoveReservationComand);
        }
        public void Execute_CancelReservationCommand(object obj)
        {
            AccommodationReservation canceledReservationa = AccommodationReservationService.GetInstance().GetByReservationId(Reservation.Id);
            if (DateOnly.FromDateTime(DateTime.Today) <= Reservation.CheckIn.AddDays(-Reservation.CancelationDays)) { 
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to cancel the reservation?", "Cancel reservation", MessageBoxButton.YesNo);
                switch (odgovor) { 
                    case MessageBoxResult.Yes:
                        if (Guest.BonusPoints != 5 && Guest.IsSuperGuest== true) Guest.BonusPoints++;
                        canceledReservationa.Status = Enums.ReservationStatus.Canceled;
                        AccommodationReservationService.GetInstance().Update(canceledReservationa);
                        NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
                        break;
                    default:
                        break;
                }
            } else MessageBox.Show("Reservation at "+ Reservation.AccommodationName + " can be canceled " + Reservation.CancelationDays + " days before start reservation", "Cancellation of this reservation is disabled ", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void Execute_RatePageCommand(object obj)
        {
            if (DateOnly.FromDateTime(DateTime.Today) <= Reservation.CheckOut.AddDays(5) && DateOnly.FromDateTime(DateTime.Today) >= Reservation.CheckOut)
                NavService.Navigate(new RateOwnerAndAccommodationView(Reservation, Guest, NavService));
            else MessageBox.Show("The guest can rate the " + Reservation.AccommodationName + " and its owner after stay, but no later than 5 days after the stay", "Rate of this reservation is disabled ", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void Execute_MoveReservationComand(object obj)
        {
            if (DateOnly.FromDateTime(DateTime.Today) < Reservation.CheckIn)
                NavService.Navigate(new MoveReservationView(Reservation, Guest, NavService));
            else MessageBox.Show("You cannot change the date of the reservation because it has already start or it has been done!", "Can't change dates of reservation", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void Execute_NextImageCommand(object obj)
        {
            if (CurrentImageIndex < ListImages.Count - 1)  CurrentImageIndex++;
            else CurrentImageIndex = 0;
        }
        public void Execute_PreviousImageCommand(object obj)
        {
            if (CurrentImageIndex > 0)  CurrentImageIndex--;
            else CurrentImageIndex = ListImages.Count - 1;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
