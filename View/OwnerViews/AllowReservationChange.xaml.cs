using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModels;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AllowReservationChange.xaml
    /// </summary>
    public partial class AllowReservationChange : Window
    {

        private AllowChangeVM vm;
        public AllowReservationChange(ReservationChangeDTO reservation)
        {
            InitializeComponent();

            vm = new AllowChangeVM(reservation);   
            DataContext = vm;

        }
    
    }
}
