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

        public TourReservationService()
        {
            _tourReservationRepository = new TourReservationRepository();
        }

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
            TourScheduleRepository tourScheduleRepository = new TourScheduleRepository();

            int currentGuestNumber = tourScheduleRepository.GetById(currentTourRealisationId).CurrentFreeSpace;
            return currentGuestNumber <= 0;
        }

        private void UpdateCurrentGuestNumber(int tourRealisationId, int guestNumber, TourScheduleRepository tourScheduleRepository)
        {
            foreach (TourSchedule tourSchedule in tourScheduleRepository.GetAll())
            {
                if (tourSchedule.Id == tourRealisationId)
                {
                    tourSchedule.CurrentFreeSpace -= guestNumber;
                    tourScheduleRepository.Update(tourSchedule);
                }
            }
        }

        private void SaveTourGuests(int reservationId, List<TourGuestDTO> guests, TourGuestRepository tourGuestRepository)
        {
            foreach (TourGuestDTO guest in guests)
            {
                TourGuests newGuest = new TourGuests(guest, reservationId);
                tourGuestRepository.Save(newGuest);
            }
        }

        public void MakeReservation(TourScheduleDTO tourScheduleDTO, User loggedUser, List<TourGuestDTO> guests)
        {
            TourGuestRepository tourGuestRepository = new TourGuestRepository();
            TourScheduleRepository tourScheduleRepository = new TourScheduleRepository();

            TourReservation reservation = new TourReservation(guests.Count(), tourScheduleDTO.Id, tourScheduleDTO.TourId, loggedUser.Id);

            UpdateCurrentGuestNumber(tourScheduleDTO.Id, reservation.GuestNumber, tourScheduleRepository);
            Save(reservation);


            SaveTourGuests(reservation.Id, guests, tourGuestRepository);
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
