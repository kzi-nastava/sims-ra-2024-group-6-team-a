using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Repository;
using BookingApp.DTOs;
using BookingApp.Observer;
using BookingApp.Resources;
using BookingApp.ViewModels;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationViewMenu.xaml
    /// </summary>
    public partial class AccommodationViewMenu : Window
    { 

        public AccommodationMenuVM vm;

        public AccommodationViewMenu(Owner owner, LocationRepository _locationRepository, ImageRepository _imageRepository, AccommodationReservationRepository _reservationRepository, UserRepository _userRepository, ReservationChangeRepository _reservationChangeRepository,OwnerRepository _ownerRepository)
        {
            InitializeComponent();
            vm = new AccommodationMenuVM(owner, _locationRepository, _imageRepository, _reservationRepository, _userRepository, _reservationChangeRepository, _ownerRepository);
            DataContext = vm;

            Title = owner.Name + " " + owner.Surname + "'s accommodations"; // ime prozora ce biti ime vlasnika
           
        }
        private void RegisterAccommodation(object sender, RoutedEventArgs e) //poziva konstruktor dodavanja novog smestaja
        {
            vm.Register();
        }

        private void DetailedView(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                SelectedDetailed(sender, e);
            }
            
        }

        public void EnterDetailedView()
        {
            if (Tabs.SelectedItem == AccommodationsTab && vm.SelectedAccommodation != null)
            {
                vm.DetailedAccommodationView();
            }
            else if (Tabs.SelectedItem == ReviewsTab && vm.SelectedGuestReview != null)
            {
                vm.GradeEmptyReview();
            }
            else if (Tabs.SelectedItem == ReservationChangesTab && vm.SelectedChange != null)
            {

                vm.AllowReservationChange();
            }

            vm.Update();
        }




        //this allows the user to do everything using keyboard buttons,in window or tabs
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                Close();
            }
            else if(e.Key == Key.F1) 
            {
                RegisterAccommodation(sender,e);
            }
            else if(e.Key == Key.F2)
            {
                EnterOwnerInfo(sender,e);
            }
            else if (e.Key == Key.F3)
            {
                SelectedDetailed(sender,e);
            }
            else if(e.Key == Key.A)
            {
                Tabs.SelectedItem = AccommodationsTab;

                SelectFirstAccommodation();

            }
            else if(e.Key == Key.G)
            {
                Tabs.SelectedItem = ReviewsTab;

                SelectFirstReview();

            }
            else if(e.Key == Key.R) 
            {
                Tabs.SelectedItem = ReservationsTab;

                SelectFirstReservation();
            }
            else if(e.Key == Key.C)
            {
                Tabs.SelectedItem = ReservationChangesTab;

                SelectFirstResChange();
            }
        }

        private void SelectFirstAccommodation()
        {
            if (vm.SelectedAccommodation == null)
            {
                vm.SelectedGuestReview = null;
                vm.SelectedReservation = null;
                vm.SelectedChange = null;
                vm.SelectedAccommodation = vm.Accommodations.First();
                AccommodationsList.SelectedIndex = 0;
                AccommodationsList.UpdateLayout();
                AccommodationsList.Focus();
               

            }
        }

        private void SelectFirstReview()
        {
            if (vm.SelectedGuestReview == null)
            {
                vm.SelectedAccommodation = null;
                vm.SelectedReservation = null;
                vm.SelectedChange = null;
                vm.SelectedGuestReview = vm.GuestReviews.First();
                ReviewsList.SelectedIndex = 0;
                ReviewsList.UpdateLayout();
                ReviewsList.Focus();
                
            }

        }

        private void SelectFirstReservation()
        {
            if (vm.SelectedReservation == null)
            {
                vm.SelectedGuestReview = null;
                vm.SelectedAccommodation = null;
                vm.SelectedChange = null;
                vm.SelectedReservation = vm.Reservations.First();
                ReservationsList.SelectedIndex = 0;
                ReservationsList.UpdateLayout();
                ReservationsList.Focus();

            }
        }


        private void SelectFirstResChange()
        {
            if (vm.SelectedChange == null)
            {
                vm.SelectedGuestReview = null;
                vm.SelectedAccommodation = null;
                vm.SelectedReservation = null;
                vm.SelectedChange = vm.ReservationChanges.First();
                ReservationChangesGrid.SelectedIndex = 0;
                ReservationChangesGrid.UpdateLayout();
                ReservationChangesGrid.Focus();

            }
        }


        private void EnterOwnerInfo(object sender, RoutedEventArgs e)
        {
            vm.EnterOwnerInfo();

        }

        
        public void SelectedDetailed(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedAccommodation == null && vm.SelectedGuestReview == null &&   vm.SelectedChange == null)
                MessageBox.Show("You have not selected an item", "Warning", MessageBoxButton.OK);
            else
                EnterDetailedView();
        }

    }
}
