using BookingApp.ApplicationServices;
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
    public class GuestStartViewModel : INotifyPropertyChanged
    {
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand AccommodationCommand { get; set; }
        public RelayCommand ProfileCommand { get; set; }
        public RelayCommand MyReservationCommand { get; set; }
        public RelayCommand MyRequestCommand { get; set; }
        public RelayCommand RatingsCommand { get; set; }
        public RelayCommand ForumCommand { get; set; }
        public RelayCommand MenuOpenCommand { get; set; }
        public RelayCommand MenuClosedCommand { get; set; }
        public RelayCommand WhereverCommand { get; set; }
        public GuestStartViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;
            NavService = navigation;

            ProfileCommand = new RelayCommand(Execute_ProfileCommandCommand);
            AccommodationCommand = new RelayCommand(Execute_AccommodationCommand);
            MyReservationCommand = new RelayCommand(Execute_MyReservationCommand);
            ForumCommand = new RelayCommand(Execute_ForumCommand);
            MenuOpenCommand = new RelayCommand(Execute_MenuOpenCommand);
            MenuClosedCommand = new RelayCommand(Execute_MenuClosedCommand);
            MyRequestCommand = new RelayCommand(Execute_MyRequestCommand);
            RatingsCommand = new RelayCommand(Execute_RatingsCommand);
            WhereverCommand = new RelayCommand(Execute_WhereverCommand);
            MenuOpenVisibility = Visibility.Collapsed;
            RedDotVisibility = Visibility.Collapsed;
            NavService.Navigate(new GuestAccommodationsView(Guest, NavService));
            ShowNotice();
        }
        public void ShowNotice()
        {
            NumberOfNotifications = 0;
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetAllReservationsByGuest(Guest.Id))
                NumberOfNotifications += ReservationChangeService.GetInstance().GetNumberOfNotifications(reservation.Id);
            if (NumberOfNotifications > 0)
                RedDotVisibility = Visibility.Visible;

        }
        public void Execute_MenuOpenCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Visible;
        }

        public void Execute_MenuClosedCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
        }
        public void Execute_ForumCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
            NavService.Navigate(new ForumView(Guest, NavService));
        }   
        public void Execute_WhereverCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
            NavService.Navigate(new WhereverWheneverView(Guest, NavService));
        }  
        public void Execute_MyRequestCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
            RedDotVisibility = Visibility.Collapsed;

            NavService.Navigate(new GuestMyRequestView(Guest, NavService));
        }
        public void Execute_MyReservationCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
            NavService.Navigate(new GuestMyReservationsView(Guest, NavService));

        }
        public void Execute_AccommodationCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
            NavService.Navigate(new GuestAccommodationsView(Guest, NavService));
        }
        public void Execute_RatingsCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
            NavService.Navigate(new MyRatingView(Guest, NavService));
        }
        public void Execute_ProfileCommandCommand(object obj)
        {
            MenuOpenVisibility = Visibility.Collapsed;
            NavService.Navigate(new GuestProfilView(Guest, NavService));

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private Visibility redDotVisibility;
        public Visibility RedDotVisibility
        {
            get { return redDotVisibility; }
            set
            {
                if (redDotVisibility != value)
                {
                    redDotVisibility = value;
                    OnPropertyChanged();
                }
            }
        }
        private Visibility menuOpenVisibility;
        public Visibility MenuOpenVisibility
        {
            get { return menuOpenVisibility; }
            set
            {
                if (menuOpenVisibility != value)
                {
                    menuOpenVisibility = value;
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
                if (value != numberOfNotifications)
                {
                    numberOfNotifications = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
