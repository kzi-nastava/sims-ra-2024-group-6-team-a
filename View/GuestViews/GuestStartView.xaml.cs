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
using System.Windows.Shapes;

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for GuestStartView.xaml
    /// </summary>
    public partial class GuestStartView : Window
    {
        public Guest guest { get; set; }
        public GuestStartView(Guest _guest)
        {
            guest = _guest;
            InitializeComponent();
            SelectedTab.Content = new GuestAccommodationsView(guest, SelectedTab.NavigationService);
        }
        private void HomePage(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new GuestAccommodationsView(guest, SelectedTab.NavigationService);

        }
        private void MyReservationPage(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new GuestMyReservationsView(guest, SelectedTab.NavigationService);
        }
    }
}
