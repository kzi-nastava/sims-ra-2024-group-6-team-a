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

        TourScheduleRepository _tourScheduleRepository;
        TourGuestRepository _tourGuestRepository;
        VoucherRepository _voucherRepository;
        TourRepository _tourRepository;

        TourReservationRepository _tourReservationRepository;

        public event EventHandler TourCanceledCard;
        public AllToursCard()
        {
            InitializeComponent();
            _tourScheduleRepository = new TourScheduleRepository();
            _tourGuestRepository = new TourGuestRepository();
            _voucherRepository = new VoucherRepository();
            _tourRepository = new TourRepository();
            _tourReservationRepository  = new TourReservationRepository();
        }

        private void DeleteTourMouseDown(object sender, MouseButtonEventArgs e)
        {
            TourSchedule tourSchedule = _tourScheduleRepository.GetById(Convert.ToInt32(textBoxId.Text));
            List<TourGuests> guests = _tourGuestRepository.GetAllByTourId(tourSchedule.Id);


            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel your tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Convert.ToDateTime(txtTourStart.Text) > DateTime.Now.AddHours(48))
            {
                if (result == MessageBoxResult.Yes)
                {
                    if (guests.Count != 0)
                    {
                        _voucherRepository.SaveAllGuests(guests);
                        
                        foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
                        {
                            if (tourReservation.TourRealisationId == tourSchedule.Id)
                                _tourReservationRepository.Delete(tourReservation);
                        
                        }

                    }

                    foreach (TourGuests guest in guests)
                    {
                        _tourGuestRepository.Delete(guest);
                    }


                    if (_tourScheduleRepository.GetAllByTourId(tourSchedule.TourId).Count == 1)
                    {
                        _tourRepository.Delete(_tourRepository.GetById(tourSchedule.TourId));
                    }

                    _tourScheduleRepository.Delete(tourSchedule);
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
