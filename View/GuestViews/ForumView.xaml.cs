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
    /// Interaction logic for ForumView.xaml
    /// </summary>
    public partial class ForumView : Page
    {
        public ForumView(Guest guest, NavigationService navService)
        {
            InitializeComponent();
            this.DataContext = new ForumViewModel(guest, navService);
        }
    }
}
