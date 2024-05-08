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
       

        public AccommodationViewMenu(Owner owner)
        {
            InitializeComponent();
            

            vm = new AccommodationMenuVM(owner);
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
                EnterGuestReviewView();
                    
            }
            else if (Tabs.SelectedItem == ReservationChangesTab && vm.SelectedChange != null)
            {

                vm.AllowReservationChange();
            }

            vm.Update();
        }

        public void EnterGuestReviewView()
        {
            if (vm.SelectedGuestReview.RespectGrade == 0 && vm.SelectedGuestReview.CleanlinessGrade == 0)
                vm.GradeEmptyReview();
            else
                vm.ShowGuestsReview();
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

                vm.SelectedGuestReview = null;
                vm.SelectedReservation = null;
                vm.SelectedChange = null;
                
                AccommodationsList.SelectedIndex = 0;
                AccommodationsList.UpdateLayout();
                AccommodationsList.Focus();
               

        }

        private void SelectFirstReview()
        {

                vm.SelectedAccommodation = null;
                vm.SelectedReservation = null;
                vm.SelectedChange = null;
                
                ReviewsList.SelectedIndex = 0;
                ReviewsList.UpdateLayout();
                ReviewsList.Focus();
                
     
        }

        private void SelectFirstReservation()
        {
 
                vm.SelectedGuestReview = null;
                vm.SelectedAccommodation = null;
                vm.SelectedChange = null;
                
                ReservationsList.SelectedIndex = 0;
                ReservationsList.UpdateLayout();
                ReservationsList.Focus();

            
        }


        private void SelectFirstResChange()
        {

                vm.SelectedGuestReview = null;
                vm.SelectedAccommodation = null;
                vm.SelectedReservation = null;
                
                ChangesList.SelectedIndex = 0;
                ChangesList.UpdateLayout();
                ChangesList.Focus();

            
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

        private void SelectAccommodations(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedItem = AccommodationsTab;

            SelectFirstAccommodation();
        }

        private void SelectReservations(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedItem = ReservationsTab;

            SelectFirstReservation();
        }

        private void SelectReviews(object sender, RoutedEventArgs e)
        {

            Tabs.SelectedItem = ReviewsTab;

            SelectFirstReview();
        }

        private void SelectChanges(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedItem = ReservationChangesTab;

            SelectFirstResChange();
        }
    }
}
