using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModels.GuideViewModel;
using BookingApp.ViewModels.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Window
    {
        public FinishedToursViewModel FinishedToursWindow { get; set; }
        public FinishedTours(User user)
        {
            InitializeComponent();
            FinishedToursWindow = new FinishedToursViewModel(this, user);
            DataContext = FinishedToursWindow;

        }
    }
}
