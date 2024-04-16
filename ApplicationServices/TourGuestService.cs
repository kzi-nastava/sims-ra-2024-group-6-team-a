using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.ApplicationServices
{
    public class TourGuestService
    {
        private ITourGuestRepository _guestRepository;
        private TourReservationService _reservationService;
        public TourGuestService(ITourGuestRepository guestRepository)
        {
           _guestRepository = guestRepository;
            _reservationService = TourReservationService.GetInstance();
        }
        
        public TourGuestService()
        {
            _guestRepository = new TourGuestRepository();
        }


        public static TourGuestService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourGuestService>();
        }


        public TourGuests Update(TourGuests guest)
        {
            return _guestRepository.Update(guest);
        }


        public void Delete(TourGuests guest)
        {
             _guestRepository.Delete(guest);
        }
        public List<TourGuests> GetAll()
        {
            return _guestRepository.GetAll();
        }
        public List<TourGuests> GetAllPresentGuestsByReservation(int reservationId)
        {
            List<TourGuests> guests = new List<TourGuests>();

            foreach (TourGuests guest in GetAll())
            {
                if (guest.ReservationId == reservationId && guest.IsPresent == true)
                    guests.Add(guest);

            }
            return guests;
        }

        public List<TourGuests> GetAllByTourId(int tourRealisationId)
        {
            List<TourGuests> guests = new List<TourGuests>();
            List<TourReservation> reservations = new List<TourReservation>();

            reservations = _reservationService.GetAllByRealisationId(tourRealisationId);
            foreach (TourReservation reservation in reservations)
            {
                foreach (TourGuests tourGuest in _guestRepository.GetAll())
                {
                    if (reservation.Id == tourGuest.ReservationId)
                    {
                        guests.Add(tourGuest);
                    }
                }
            }
            return guests;
        }

        public List<TourGuests> GetAllByReservationId(int reservationId)
        {
            return _guestRepository.GetAllByReservationId(reservationId);
        }
    }
}
