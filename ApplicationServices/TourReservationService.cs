using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
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
    public class TourReservationService
    {
        private ITourReservationRepository _tourReservationRepository;
       

        public TourReservationService(ITourReservationRepository tourReservationRepository)
        {
            _tourReservationRepository = tourReservationRepository;
        }

       /* public TourReservationService()
        {
            _tourReservationRepository = new TourReservationRepository();
        }*/

        public static TourReservationService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourReservationService>();
        }


        public TourReservation Save(TourReservation reservation)
        {
            return _tourReservationRepository.Save(reservation);
        }
        public List<TourReservation> GetAllByRealisationId(int tourRealisationId)
        {
            return _tourReservationRepository.GetAllByRealisationId(tourRealisationId) ;
        }

        public bool IsFullyBooked(int currentTourRealisationId)
        {

            int currentGuestNumber = TourScheduleService.GetInstance().GetById(currentTourRealisationId).CurrentFreeSpace;
            return currentGuestNumber <= 0;
        }

        private void UpdateCurrentGuestNumber(int tourRealisationId, int guestNumber)
        {
            foreach (TourSchedule tourSchedule in TourScheduleService.GetInstance().GetAll())
            {
                if (tourSchedule.Id == tourRealisationId)
                {
                    tourSchedule.CurrentFreeSpace -= guestNumber;
                    TourScheduleService.GetInstance().Update(tourSchedule);
                }
            }
        }

        private void SaveTourGuests(int reservationId, List<TourGuestDTO> guests)
        {
            foreach (TourGuestDTO guest in guests)
            {
                TourGuests newGuest = new TourGuests(guest, reservationId);
                TourGuestService.GetInstance().Save(newGuest);
            }
        }

        public void MakeReservation(TourScheduleDTO tourScheduleDTO, User loggedUser, List<TourGuestDTO> guests)
        {

            TourReservation reservation = new TourReservation(guests.Count(), tourScheduleDTO.Id, tourScheduleDTO.TourId, loggedUser.Id);

            UpdateCurrentGuestNumber(tourScheduleDTO.Id, reservation.GuestNumber);
            Save(reservation);


            SaveTourGuests(reservation.Id, guests);
        }

        public List<TourReservation> GetAllByUser(User user) 
        {
            return _tourReservationRepository.GetAllByUser(user);

        }


        public TourReservation GetById(int id)
        {
            return _tourReservationRepository.GetById(id);  

        }

        public List<TourReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }

        public void Delete(TourReservation reservation)
        {
           _tourReservationRepository.Delete(reservation);
        }

    }
}
