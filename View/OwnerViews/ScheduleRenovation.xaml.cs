using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for ScheduleRenovation.xaml
    /// </summary>
    public partial class ScheduleRenovation : Window
    {
        ScheduleRenovationVM ViewModel { get; set; }

        public ScheduleRenovation(AccommodationOwnerDTO Accommodation,List<ReservationOwnerDTO> Reservations)
        {
            ViewModel = new ScheduleRenovationVM(Accommodation, Reservations);

            InitializeComponent();
            DataContext = ViewModel;
        }
   
    }
}
