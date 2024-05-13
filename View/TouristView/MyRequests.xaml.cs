using BookingApp.Model;
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
    /// Interaction logic for MyRequests.xaml
    /// </summary>
    public partial class MyRequests : Window
    {
        public MyRequests(User user)
        {
            InitializeComponent();
            MyRequestsViewModel request = new MyRequestsViewModel(user);
            DataContext = request;
        }
    }
}
