using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
namespace BookingApp.ViewModels.GuestsViewModel
{
    public class GuestProfilViewModel : INotifyPropertyChanged
    {
        private AccommodationReservationRepository accommodationReservationRepository;
        private ReservationChangeRepository reservationChangeRepository;
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand MyReservationCommand { get; set; }
        public RelayCommand MyRequestCommand { get; set; }

        public GuestProfilViewModel(Guest guest, NavigationService navigation)
        {
            accommodationReservationRepository = new AccommodationReservationRepository();
            reservationChangeRepository = new ReservationChangeRepository();
            Guest = guest;
            NumberOfReservations = accommodationReservationRepository.GetNumberOfReservation(Guest.Id).ToString();
            foreach (AccommodationReservation reservation in accommodationReservationRepository.GetAllReservationsByGuest(Guest.Id))
                NumberOfNotifications += reservationChangeRepository.GetNumberOfNotifications(reservation.Id);
            MyReservationCommand = new RelayCommand(Execute_MyReservationCommand);
            MyRequestCommand = new RelayCommand(Execute_MyRequestCommand);
            NavService = navigation;
        }
        public void Execute_MyReservationCommand(object obj)
        {
            NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
        }
        public void Execute_MyRequestCommand(object obj)
        {
            NavService.Navigate(new GuestMyRequestView(Guest, NavService));
        }
        private string numberOfReservations;
        public string NumberOfReservations
        {
            get => numberOfReservations;
            set
            {
                if (value != numberOfReservations) { 
                    numberOfReservations = value;
                    OnPropertyChanged();
                }
            }
        }
        private int numberOfNotifications;
        public int NumberOfNotifications
        {
            get => numberOfNotifications;
            set
            {
                if (value != numberOfNotifications) { 
                    numberOfNotifications = value;
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
