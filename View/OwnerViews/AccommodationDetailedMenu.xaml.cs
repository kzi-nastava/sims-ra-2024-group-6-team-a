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
using BookingApp.DTOs;
using BookingApp.Observer;
using BookingApp.Repository;
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

                ViewModel.SelectFirstReservation(ReservationsList);
            }
            else if(e.Key == Key.S)
            {
                Tabs.SelectedItem = StatisticsTab;
            }
            else if (e.Key == Key.N)
            {
                Tabs.SelectedItem = RenovationsTab;
            }
            else if (e.Key == Key.B)
            {
                Tabs.SelectedItem = BlogsTab;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
