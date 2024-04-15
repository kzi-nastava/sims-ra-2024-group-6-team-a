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
            TourSchedule tourSchedule = TourScheduleService.GetInstance().GetById(Convert.ToInt32(textBoxId.Text));
            List<TourGuests> guests = TourGuestService.GetInstance().GetAllByTourId(tourSchedule.Id);


            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel your tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Convert.ToDateTime(txtTourStart.Text) > DateTime.Now.AddHours(48))
            {
                if (result == MessageBoxResult.Yes)
                {
                    if (guests.Count != 0)
                    {
                        VoucherService.GetInstance().SaveAllGuests(guests);
                        
                        foreach (TourReservation tourReservation in TourReservationService.GetInstance().GetAll())
                        {
                            if (tourReservation.TourRealisationId == tourSchedule.Id)
                                TourReservationService.GetInstance().Delete(tourReservation);
                        
                        }

                    }

                    foreach (TourGuests guest in guests)
                    {
                        TourGuestService.GetInstance().Delete(guest);
                    }


                    if (TourScheduleService.GetInstance().GetAllByTourId(tourSchedule.TourId).Count == 1)
                    {
                        TourService.GetInstance().Delete(TourService.GetInstance().GetById(tourSchedule.TourId));
                    }

                    TourScheduleService.GetInstance().Delete(tourSchedule);
                    HandleTourCanceledEvent();
                }
            }
            else
            {
                MessageBox.Show("Selected tour can't be canceled(less than 48 hours til tour comences");
            }

        }

        public void HandleTourCanceledEvent()
        {
            TourCanceledCard?.Invoke(this,EventArgs.Empty);
        }

    }
}
