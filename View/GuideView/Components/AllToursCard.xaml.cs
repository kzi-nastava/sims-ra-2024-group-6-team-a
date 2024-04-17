using BookingApp.ApplicationServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuideView.Components
{
    /// <summary>
    /// Interaction logic for AllToursCard.xaml
    /// </summary>
    public partial class AllToursCard : UserControl
    {
        public event EventHandler TourCanceledCard;
        public AllToursCard()
        {
            InitializeComponent();     
        }
        private void DeleteTourMouseDown(object sender, MouseButtonEventArgs e)
        {
            int tourId = Convert.ToInt32(textBoxId.Text);
            TourSchedule tourSchedule = TourScheduleService.GetInstance().GetById(tourId);
            List<TourGuests> guests = TourGuestService.GetInstance().GetAllByTourId(tourSchedule.Id);

            if (CanTourBeCanceled(tourSchedule))
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel your tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CancelTour(tourSchedule, guests);
                    HandleTourCanceledEvent();
                }
            }
            else
            {
                MessageBox.Show("Selected tour can't be canceled (less than 48 hours until tour commences)");
            }
        }

        private bool CanTourBeCanceled(TourSchedule tourSchedule)
        {
            return Convert.ToDateTime(txtTourStart.Text) > DateTime.Now.AddHours(48);
        }


        private void CancelTour(TourSchedule tourSchedule, List<TourGuests> guests)
        {
            SaveGuestVouchers(guests);
            DeleteTourReservations(tourSchedule);
            DeleteTourGuests(guests);
            DeleteTourIfLast(tourSchedule);

            TourScheduleService.GetInstance().Delete(tourSchedule);
        }

        private void SaveGuestVouchers(List<TourGuests> guests)
        {
            if (guests.Count != 0)
            {
                VoucherService.GetInstance().SaveAllGuests(guests);
            }
        }

        private void DeleteTourReservations(TourSchedule tourSchedule)
        {
            foreach (TourReservation tourReservation in TourReservationService.GetInstance().GetAll())
            {
                if (tourReservation.TourRealisationId == tourSchedule.Id)
                {
                    TourReservationService.GetInstance().Delete(tourReservation);
                }
            }
        }

        private void DeleteTourGuests(List<TourGuests> guests)
        {
            foreach (TourGuests guest in guests)
            {
                TourGuestService.GetInstance().Delete(guest);
            }
        }

        private void DeleteTourIfLast(TourSchedule tourSchedule)
        {
            if (TourScheduleService.GetInstance().GetAllByTourId(tourSchedule.TourId).Count == 1)
            {
                TourService.GetInstance().Delete(TourService.GetInstance().GetById(tourSchedule.TourId));
            }
        }

        public void HandleTourCanceledEvent()
        {
            TourCanceledCard?.Invoke(this,EventArgs.Empty);
        }

    }
}
