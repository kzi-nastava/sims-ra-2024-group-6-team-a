using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.OwnerViews;
using BookingApp.ViewModels;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationDetailedMenu.xaml
    /// </summary>
    public partial class AccommodationDetailedMenu : Window
    {
        AccommodationDetailedVM ViewModel {  get; set; }
        public AccommodationDetailedMenu(List<Model.Image> images,ObservableCollection<ReservationOwnerDTO> Reservations,AccommodationOwnerDTO accommodation)
        {
            ViewModel = new AccommodationDetailedVM(images, Reservations,accommodation) { };
            InitializeComponent();
            DataContext = ViewModel;

            Title = accommodation.Name;

        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            else if (e.Key == Key.R)
            {
                Tabs.SelectedItem = ReservationsTab;

                SelectFirstReservation();
            }
            else if(e.Key == Key.S)
            {
                Tabs.SelectedItem = StatisticsTab;

                SelectFirstStatistic();
            }
            else if (e.Key == Key.N)
            {
                Tabs.SelectedItem = RenovationsTab;

                SelectFirstRenovation();
            }
            else if (e.Key == Key.B)
            {
                Tabs.SelectedItem = BlogsTab;
            }
            else if (e.Key == Key.I)
            {
                ImagesList.SelectedIndex = 0;
                ImagesList.UpdateLayout();
                ImagesList.Focus();
            }
        }

        public void SelectFirstReservation()
        {

                ViewModel.SelectedStatistic = null;
                ViewModel.SelectedRenovation = null;

                ViewModel.SelectedReservation = ViewModel.Reservations.First();
                ReservationsList.SelectedIndex = 0;
                ReservationsList.UpdateLayout();
                ReservationsList.Focus();

        }

        public void SelectFirstStatistic()
        {
 
                ViewModel.SelectedReservation = null;
                ViewModel.SelectedRenovation = null;

                ViewModel.SelectedStatistic = ViewModel.Statistics.First();
                StatisticsList.SelectedIndex = 0;
                StatisticsList.UpdateLayout();
                StatisticsList.Focus();


  
        }

        public void SelectFirstRenovation()
        {
                ViewModel.SelectedReservation = null;
                ViewModel.SelectedStatistic = null;

                ViewModel.SelectedRenovation = ViewModel.Renovations.First();
                RenovationsList.SelectedIndex = 0;
                RenovationsList.UpdateLayout();
                RenovationsList.Focus();

        }

        private void StatisticsClick(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(Tabs.SelectedItem == StatisticsTab && ViewModel.SelectedStatistic != null) 
                {
                    ViewModel.OpenMonthStatistic();

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ScheduleRenovation();
        }

        private void RenovationsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Tabs.SelectedItem == RenovationsTab && ViewModel.SelectedRenovation != null)
                {
                    if (RenovationService.GetInstance().IsFiveDays(ViewModel.SelectedRenovation))
                    {
                        if ((MessageBox.Show("Do you want to remove the renovation?", "Remove renovation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
                        {
                            ViewModel.RemoveRenovation();
                        }
                    }
                }
            }
        }
    }
}
