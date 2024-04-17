using System;
using BookingApp.ViewModels.GuestsViewModel;
using BookingApp.DTOs;
using BookingApp.Model;
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
    /// Interaction logic for ReservationSeeMoreView.xaml
    /// </summary>
    public partial class ReservationSeeMoreView : Page
    {
        public ReservationSeeMoreView(ReservationGuestDTO SelectedReservation, Guest guest, NavigationService navigation)
        {
            InitializeComponent();
            this.DataContext = new ReservationSeeMoreViewModel(guest, SelectedReservation, this, navigation);
        }
    }
}
