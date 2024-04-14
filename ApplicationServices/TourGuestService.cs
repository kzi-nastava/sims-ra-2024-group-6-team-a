using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class TourGuestService
    {
        private ITourGuestRepository _guestRepository;
        public TourGuestService()
        {
            _guestRepository = new TourGuestRepository();
        }

        public List<TourGuests> GetAll()
        {
            return _guestRepository.GetAll();
        }
        public List<TourGuests> GetAllPresentGuestsByReservation(int reservationId) //pronadji sve goste u nekoj rezervaciji koji su se pojavili 
        {
            List<TourGuests> guests = new List<TourGuests>();

            foreach (TourGuests guest in GetAll())
            {
                if (guest.ReservationId == reservationId && guest.IsPresent == true)
                    guests.Add(guest);

            }
            return guests;
        }
    }
}
