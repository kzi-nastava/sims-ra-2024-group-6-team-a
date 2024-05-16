using BookingApp.ApplicationServices;
using BookingApp.Model;
using BookingApp.ViewModels.GuestsViewModel;
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
        public GuestStartView(Guest guest)
        {
            InitializeComponent();
            this.DataContext = new GuestStartViewModel(guest, SelectedTab.NavigationService);
        }
    }
}
