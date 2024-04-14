using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Window
    {
        private TourService _tourService;
        private TourGuestService _tourGuestService;
        private TourReservationService _reservationService;
        private TourScheduleService _scheduleService;
        public User LoggedUser { get; set; }
        public ObservableCollection<TourGuestDTO> TourGuests { get; set; }
        public ObservableCollection<TourScheduleDTO> FinishedTours { get; set; }
        public TourGuestDTO TourGuestDTO { get; set; }
        public Inbox(User user)
        {
            InitializeComponent();
            DataContext = this;
           

            LoggedUser = user;
            TourGuests = new ObservableCollection<TourGuestDTO>();
            FinishedTours = new ObservableCollection<TourScheduleDTO>();
            TourGuestDTO = new TourGuestDTO();

            _tourService = new TourService();
            _tourGuestService = new TourGuestService();
            _reservationService = new TourReservationService();
            _scheduleService = new TourScheduleService();
            Update();
        }
        private void Update()
        {
            TourGuests.Clear();

            foreach(TourSchedule schedule in _scheduleService.GetAllFinishedTours(LoggedUser)) //za svaki termin u mojim zavrsenim na kojim sam se pojavila mi prikazi osobe koje su se pojavile
            {
                FinishedTours.Add(new TourScheduleDTO(schedule));

                foreach (TourReservation reservation in _reservationService.GetAllByRealisationId(schedule.Id))
                {
                    foreach (TourGuests guest in _tourGuestService.GetAllPresentGuestsByReservation(reservation.Id))
                    {
                        
                        TourGuests.Add(new TourGuestDTO(guest));
                    }
                }
                
            }
        }
    }
}
