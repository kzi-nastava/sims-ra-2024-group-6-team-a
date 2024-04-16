using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.View.TouristView;
using BookingApp.ViewModels.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourReservationForm.xaml
    /// </summary>
    public partial class TourReservationForm : Window

    {
        public TourReservationFormViewModel ReservationWindow { get; set; }

        public TourReservationForm(User user, TourTouristDTO selectedTour, TourReservationService reservationService, TourScheduleService scheduleService)
        {
            InitializeComponent();
            ReservationWindow = new TourReservationFormViewModel(this, user, selectedTour, reservationService, scheduleService);
            DataContext = ReservationWindow;
        }

    }
}

