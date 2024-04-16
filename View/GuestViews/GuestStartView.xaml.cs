using BookingApp.ApplicationServices;
using BookingApp.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for GuestStartView.xaml
    /// </summary>
    public partial class GuestStartView : Window
    {
        public Guest guest { get; set; }
        public NavigationService NavService { get; set; }
        public GuestStartView(Guest _guest)
        {
            guest = _guest;
            InitializeComponent();
            SelectedTab.Content = new GuestAccommodationsView(guest, SelectedTab.NavigationService);
            ShowNotice();
        }
        public void ShowNotice()
        {
            int numberOfNotifications = 0;
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetAllReservationsByGuest(guest.Id))
                numberOfNotifications += ReservationChangeService.GetInstance().GetNumberOfNotifications(reservation.Id);
            if (numberOfNotifications > 0)
            {
                MessageBoxResult odgovor = MessageBox.Show("You have " + numberOfNotifications + " notifications!\n\n Do you want to see notifications now?", "Notice!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        SelectedTab.Content = new GuestMyRequestView(guest, SelectedTab.NavigationService);
                        break;
                    default:
                        break;
                }
            }

        }
        private void HomePage(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new GuestAccommodationsView(guest, SelectedTab.NavigationService);

        }
        private void MyReservationPage(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new GuestMyReservationsView(guest, SelectedTab.NavigationService);
        }
        private void ProfilePage(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new GuestProfilView(guest, SelectedTab.NavigationService);
        }
    }
}
