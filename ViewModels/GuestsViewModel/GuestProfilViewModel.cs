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
    public class GuestProfilViewModel : INotifyPropertyChanged
    {
        public double AverageGrade { get; set; }
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand MyReservationCommand { get; set; }
        public RelayCommand MyRequestCommand { get; set; }
        public Visibility FiveStarVisibility { get; set; }
        public Visibility FourStarVisibility { get; set; }
        public Visibility ThreeStarVisibility { get; set; }
        public Visibility TwoStarVisibility { get; set; }
        public Visibility OneStarVisibility { get; set; }
        public Visibility OneHStarVisibility { get; set; }
        public Visibility FiveHStarVisibility { get; set; }
        public Visibility FourHStarVisibility { get; set; }
        public Visibility ThreeHStarVisibility { get; set; }
        public Visibility TwoHStarVisibility { get; set; }
        public GuestProfilViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;

            NumberOfReservations = AccommodationReservationService.GetInstance().GetActiveReservationsByGuest(guest.Id).Count.ToString();
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetAllReservationsByGuest(Guest.Id))
                NumberOfNotifications += ReservationChangeService.GetInstance().GetNumberOfNotifications(reservation.Id);
            MyReservationCommand = new RelayCommand(Execute_MyReservationCommand);
            MyRequestCommand = new RelayCommand(Execute_MyRequestCommand);
            NavService = navigation;
            CheckRating();
        }
        public void ShowZeroStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
            FiveHStarVisibility = Visibility.Collapsed;
            FourStarVisibility = Visibility.Collapsed;
            FourHStarVisibility = Visibility.Collapsed;
            ThreeStarVisibility = Visibility.Collapsed;
            ThreeHStarVisibility = Visibility.Collapsed;
            TwoStarVisibility = Visibility.Collapsed;
            TwoHStarVisibility = Visibility.Collapsed;
            OneStarVisibility = Visibility.Collapsed;
            OneHStarVisibility = Visibility.Collapsed;
        }
        public void ShowOneStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
            FiveHStarVisibility = Visibility.Collapsed;
            FourStarVisibility = Visibility.Collapsed;
            FourHStarVisibility = Visibility.Collapsed;
            ThreeStarVisibility = Visibility.Collapsed;
            ThreeHStarVisibility = Visibility.Collapsed;
            TwoStarVisibility = Visibility.Collapsed;
            TwoHStarVisibility = Visibility.Collapsed;
        }
        public void ShowOneHalfStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
            FiveHStarVisibility = Visibility.Collapsed;
            FourStarVisibility = Visibility.Collapsed;
            FourHStarVisibility = Visibility.Collapsed;
            ThreeStarVisibility = Visibility.Collapsed;
            ThreeHStarVisibility = Visibility.Collapsed;
            TwoStarVisibility = Visibility.Collapsed;
        }
        public void ShowTwoStar()
        {
                FiveStarVisibility = Visibility.Collapsed;
                FiveHStarVisibility = Visibility.Collapsed;
                FourStarVisibility = Visibility.Collapsed;
                FourHStarVisibility = Visibility.Collapsed;
                ThreeStarVisibility = Visibility.Collapsed;
                ThreeHStarVisibility = Visibility.Collapsed;
        }
        public void ShowTwoHalfStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
            FiveHStarVisibility = Visibility.Collapsed;
            FourStarVisibility = Visibility.Collapsed;
            FourHStarVisibility = Visibility.Collapsed;
            ThreeStarVisibility = Visibility.Collapsed;
        }
        public void ShowThreeStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
            FiveHStarVisibility = Visibility.Collapsed;
            FourStarVisibility = Visibility.Collapsed;
            FourHStarVisibility = Visibility.Collapsed;
        }
        public void ShowThreeHalfStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
            FiveHStarVisibility = Visibility.Collapsed;
            FourStarVisibility = Visibility.Collapsed;

        }
        public void ShowFourStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
            FiveHStarVisibility = Visibility.Collapsed;
        }
        public void ShowFourHalfStar()
        {
            FiveStarVisibility = Visibility.Collapsed;
        }
        public void CheckRating() {
            AverageGrade = Math.Round(GuestReviewService.GetInstance().GetAverageGradeByGuest(Guest), 2);
            if(AverageGrade==0.00) ShowZeroStar();
            if (AverageGrade <= 1.25) ShowOneStar();
            else if (AverageGrade <= 1.75) ShowOneHalfStar();
            else if (AverageGrade <= 2.25) ShowTwoStar();
            else if (AverageGrade <= 2.75) ShowTwoHalfStar();
            else if (AverageGrade <= 3.25) ShowThreeStar();
            else if (AverageGrade <= 3.75) ShowThreeHalfStar();
            else if (AverageGrade <= 4.25) ShowFourStar();
            else if (AverageGrade <= 4.75) ShowFourHalfStar();

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
