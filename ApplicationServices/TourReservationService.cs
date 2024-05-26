using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.ApplicationServices
{
    public class TourReservationService
    {
        private ITourReservationRepository _tourReservationRepository;
       

        public TourReservationService(ITourReservationRepository tourReservationRepository)
        {
            _tourReservationRepository = tourReservationRepository;
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

        private void SaveTourGuests(int reservationId, List<TourGuestDTO> guests, User loggedUser)
        {
            foreach (TourGuestDTO guest in guests)
            {
                TourGuests newGuest = new TourGuests(guest, reservationId, -1);
                TourGuestService.GetInstance().Save(newGuest);
            }
        }

        public void MakeReservation(TourScheduleDTO tourScheduleDTO, User loggedUser, List<TourGuestDTO> guests)
        {
            Tourist tourist = TouristService.GetInstance().GetByTouristId(loggedUser.Id);
            TourReservation reservation = new TourReservation(guests.Count(), tourScheduleDTO.Id, tourScheduleDTO.TourId, loggedUser.Id);

            UpdateCurrentGuestNumber(tourScheduleDTO.Id, reservation.GuestNumber);
            Save(reservation);
            SaveTourGuests(reservation.Id, guests, loggedUser);
            
            if(tourist.Points == 5)
            {
                Voucher voucher= new Voucher(tourist.Name, tourist.Surname, tourist.Age, System.DateTime.Now.AddMonths(6),tourist.UserId, System.DateTime.Now);
                VoucherService.GetInstance().Save(voucher);
                TouristService.GetInstance().GetByTouristId(loggedUser.Id).Points = 0;
                TouristService.GetInstance().Update(tourist);
            }
            
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
