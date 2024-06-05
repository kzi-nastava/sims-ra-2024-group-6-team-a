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
using LiveCharts;
using LiveCharts.Wpf;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for RequestedTourStatistics.xaml
    /// </summary>
    public partial class RequestedTourStatistics : Page
    {
        public RequestedTourStatisticsViewModel StatisticsViewModel { get; set; }
        public RequestedTourStatistics(User user)
        {
            InitializeComponent();
            StatisticsViewModel = new RequestedTourStatisticsViewModel(this, user);
            DataContext = StatisticsViewModel;
        }

    }
}
