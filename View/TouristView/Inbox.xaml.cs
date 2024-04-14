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
        private TourRepository _tourRepository;
        private TourGuestRepository _tourGuestRepository;
        private TourReservationRepository _reservationRepository;
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

            _tourRepository = new TourRepository();
            _tourGuestRepository = new TourGuestRepository();
            _reservationRepository = new TourReservationRepository();
            Update();
        }
        private void Update()
        {
            TourGuests.Clear();

            foreach(TourSchedule schedule in _tourRepository.GetAllFinishedTours(LoggedUser)) //za svaki termin u mojim zavrsenim na kojim sam se pojavila mi prikazi osobe koje su se pojavile
            {
                FinishedTours.Add(new TourScheduleDTO(schedule));

                foreach (TourReservation reservation in _reservationRepository.GetAllByRealisationId(schedule.Id))
                {
                    foreach (TourGuests guest in _tourGuestRepository.GetAllPresentGuestsByReservation(reservation.Id))
                    {
                        
                        TourGuests.Add(new TourGuestDTO(guest));
                    }
                }
                
            }
        }
    }
}
