using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
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
        ReservationChangeDTO reservation;
        private AccommodationReservationRepository _reservationRepository;
        private ReservationChangeRepository _changesRepository;
        public AllowReservationChange(ReservationChangeDTO reservation,AccommodationReservationRepository _reservationRepository,ReservationChangeRepository _changesRepository)
        {
            InitializeComponent();
            
            this.reservation = reservation;
            DataContext = this.reservation;

            this._reservationRepository = _reservationRepository;
            this._changesRepository = _changesRepository;
        }

        private void YesClick(object sender, RoutedEventArgs e)
        {

            
            
        }

        private void NoClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                Close();
            }
        }
    }
}
