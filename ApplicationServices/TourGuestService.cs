using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
namespace BookingApp.ApplicationServices
{
    public class TourGuestService
    {
        private ITourGuestRepository _guestRepository;
        public TourGuestService(ITourGuestRepository guestRepository)
        {
           _guestRepository = guestRepository;
        }
        public static TourGuestService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourGuestService>();
        }

        public List <TourGuests> GetAllByRequestId(int requestId)   
        {

            List<TourGuests> tourGuests = new List<TourGuests>();

            foreach (TourGuests t in GetAll())
            {
                if (t.RequestId == requestId)
                {
                    tourGuests.Add(t);
                }   
            }
            return tourGuests;
        }


        public TourGuests Update(TourGuests guest)
        {
            return _guestRepository.Update(guest);
        }

        public TourGuests Save(TourGuests guest)
        {
            return _guestRepository.Save(guest);
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

            reservations = TourReservationService.GetInstance().GetAllByRealisationId(tourRealisationId);
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

        public int CountGuests(int tourScheduleId)
        {

            int touristCount = 0;
                    foreach(TourGuests guest in GetAllByTourId(tourScheduleId))
                    {
                      touristCount++;  
                    }
            
            return touristCount;
        }
        public int CountChildren(int tourScheduleId)
        {
            int childrenCount = 0;
                foreach (TourGuests guest in GetAllByTourId(tourScheduleId))
                {
                    if (guest.Age < 18)
                    {
                        childrenCount++;
                    }
                }
            return childrenCount;
        }
        public int CountAdult(int tourScheduleId)
        {
            int adultCount = 0;
           
                foreach (TourGuests guest in GetAllByTourId(tourScheduleId))
                {
                    if (guest.Age >= 18 && guest.Age <50)
                    {
                        adultCount++;
                    }
                }
            return adultCount;
        }
        public int CountElderly(int tourScheduleId)
        {
            int elderlyCount = 0;
                foreach (TourGuests guest in GetAllByTourId(tourScheduleId))
                {
                    if (guest.Age >= 50)
                    {
                        elderlyCount++;
                    }
                }
            
            return elderlyCount;
        }

        public int GetGuestsCountByRequest(int requestId) 
        {
            return GetAll().Count(guest => guest.RequestId == requestId);
        }

        public void UpdateGuestReservation(TourReservation tourReservation, int requestId)
        {
            foreach (TourGuests guest in GetAll())
            {
                if (guest.RequestId == requestId)
                {
                    guest.ReservationId = tourReservation.Id;
                    Update(guest);
                }
            }

        }
    }
}
