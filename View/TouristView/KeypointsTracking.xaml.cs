using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
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
    /// Interaction logic for KeypointsTracking.xaml
    /// </summary>
    public partial class KeypointsTracking : Window
    {
        public KeypointsTrackingViewModel KeypointsViewModel { get; set; }
        public KeypointsTracking(TourScheduleDTO tourSchedule)
        {
            InitializeComponent();
            KeypointsViewModel = new KeypointsTrackingViewModel(tourSchedule);
            DataContext = KeypointsViewModel;
        }
    }
}
