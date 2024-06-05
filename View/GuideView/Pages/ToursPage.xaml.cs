using BookingApp.ApplicationServices;
using BookingApp.Model;
using BookingApp.ViewModels.GuideViewModel;
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

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursViewModel ToursViewModel {  get; set; }

      

        public ToursPage(User user)
        {
            InitializeComponent();
            ToursViewModel = new ToursViewModel(this,user);
            DataContext = ToursViewModel;

        }

        public void LoadTodaysTours(object sender, EventArgs e)
        {
            ToursViewModel.LiveToursPageClick();
        }

    }
}
