using BookingApp.ViewModels.TouristViewModel;
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

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for RequestStatistics.xaml
    /// </summary>
    public partial class RequestStatistics : Window
    {
        public RequestStatisticsViewModel StatisticsVM { get; set; }
        public RequestStatistics(int userId)
        {
            InitializeComponent();
            StatisticsVM = new RequestStatisticsViewModel(userId);
            DataContext = StatisticsVM;
        }
    }
}
