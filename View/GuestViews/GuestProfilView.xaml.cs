using BookingApp.DTOs;
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
    /// Interaction logic for GuestProfilView.xaml
    /// </summary>
    public partial class GuestProfilView : Page
    {
        public GuestProfilView(Guest guest, NavigationService navigation)
        {
            InitializeComponent();
            this.DataContext = new GuestProfilViewModel(guest, navigation);

        }
    }
}
