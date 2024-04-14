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
    public class TourScheduleService
    {
        private ITourScheduleRepository _scheduleRepository;
        private TourReservationRepository _reservationRepository;
        private TourGuestService _guestService;

        public TourScheduleService()
        {
            _scheduleRepository = new TourScheduleRepository();
            _reservationRepository = new TourReservationRepository();
            _guestService = new TourGuestService();
        }

        public List<TourSchedule> GetAllByTourId(int tourId)
        {
            return _scheduleRepository.GetAllByTourId(tourId);
        }

        public TourSchedule GetByTour(Tour tour)
        {
            return _scheduleRepository.GetByTour(tour);
        }
        public TourSchedule GetById(int id)
        {
            return _scheduleRepository.GetById(id);
        }

        public List<TourSchedule> GetAll()
        {
            return _scheduleRepository.GetAll();
        }

        public List<TourSchedule> GetOngoingSchedulesByUser(User user)
        {
            List<TourSchedule> tours = new List<TourSchedule>();

            foreach (TourReservation reservation in _reservationRepository.GetAllByUser(user))
            {
                TourSchedule tourSchedule = GetById(reservation.TourRealisationId);
                if (tourSchedule.TourActivity == Resources.Enums.TourActivity.Ongoing)
                {
                    tours.Add(tourSchedule);
                }

            }
            return tours;
        }

        public List<TourSchedule> GetAllFinishedTours(User user) //svi termini koje sam ja rezervisala, a koji su zavrseni na kojima sam prisustvovala
        {

            List<TourSchedule> tours = new List<TourSchedule>();

            foreach (TourSchedule schedule in _scheduleRepository.GetAll())
            {
                if (schedule.TourActivity == Resources.Enums.TourActivity.Finished)//imam sve zavrsene termine
                {
                    foreach (TourReservation reservation in _reservationRepository.GetAll())
                    {
                        if (reservation.TourRealisationId == schedule.Id && reservation.TouristId == user.Id)//sve moje rezervacije tog termina
                        {
                            foreach (TourGuests guest in _guestService.GetAllPresentGuestsByReservation(reservation.Id))//gosti na toj rezervaciji
                            {
                                if (guest.UserTypeId == user.Id)
                                {
                                    tours.Add(schedule);
                                }
                            }

                        }
                    }

                }
            }
            return tours;
        }
    }
}
