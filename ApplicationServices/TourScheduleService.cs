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
    public class TourScheduleService
    {
        private ITourScheduleRepository _scheduleRepository;
        private TourGuestService _guestService;
        private TourReservationService _reservationService;

        public TourScheduleService(ITourScheduleRepository tourScheduleRepository)
        {
            _scheduleRepository = tourScheduleRepository;
            _guestService = TourGuestService.GetInstance();
            _reservationService = TourReservationService.GetInstance();
        }

        public TourScheduleService()
        {
            _scheduleRepository = new TourScheduleRepository();
            _guestService = new TourGuestService();
            _reservationService = new TourReservationService();
        }


        public static TourScheduleService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourScheduleService>();
        }

        public void Delete(TourSchedule schedule)
        {
          _scheduleRepository.Delete(schedule); 
        }
        public TourSchedule Update(TourSchedule schedule)
        {
            return _scheduleRepository.Update(schedule);    
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

            foreach (TourReservation reservation in _reservationService.GetAllByUser(user))
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

                    foreach (TourReservation reservation in TourReservationService.GetInstance().GetAllByUser(user))
                    {
                        if (reservation.TourRealisationId == schedule.Id)//sve moje rezervacije tog termina
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
        public int FindOngoingTour(List<Tour> tours)
        {
            int id = 0;

            foreach (Tour tour in tours)
            {
                foreach (TourSchedule tourSchedule in _scheduleRepository.GetAll())
                {
                    if (tourSchedule.TourId == tour.Id && tourSchedule.TourActivity == Resources.Enums.TourActivity.Ongoing)
                    {
                        return tourSchedule.Id;
                    }
                }
            }
            return id;
        }
        public TourSchedule Save(TourSchedule schedule)
        {
           return _scheduleRepository.Save(schedule);
        }
    }
}
