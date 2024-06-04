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
    /// Interaction logic for TourRequestsPage.xaml
    /// </summary>
    public partial class TourRequestsPage : Page
    {
        
        public TourRequestsViewModel RequestsViewModel { get; set; }
        
        public TourRequestsPage(User user)
        {
            InitializeComponent();
            RequestsViewModel = new TourRequestsViewModel(this, user);
            DataContext = RequestsViewModel;
        }


        public void RequestsClick(object sender, RoutedEventArgs e)
        {

            RequestsViewModel.RequestsClick();
        }

        public void ComplexTourClick(object sender, RoutedEventArgs e)
        {
            RequestsViewModel.ComplexTourClick();
        }

        public void StatisticsClick(object sender, RoutedEventArgs e)
        {
            RequestsViewModel.StatisticsClick();
        }

    }
}

