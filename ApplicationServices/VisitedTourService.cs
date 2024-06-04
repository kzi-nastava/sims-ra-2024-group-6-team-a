using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class VisitedTourService

    { 

     private IVisitedTourRepository _visitedTourRepository;

    public VisitedTourService(IVisitedTourRepository visitedRepository)
    {
            _visitedTourRepository = visitedRepository;
    }

    public static VisitedTourService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<VisitedTourService>();
    }

    public void Delete(VisitedTour tour)
    {
        _visitedTourRepository.Delete(tour);
    }

    public void DeleteAllByTouristId(int id)
        {
            foreach(VisitedTour tour in _visitedTourRepository.GetAll())
            {
                if (tour.TouristId == id)
                    Delete(tour);
            }
        }
        public List<VisitedTour> GetByTouristId(int id)
        {
            return _visitedTourRepository.GetAll().Where(v => v.TouristId == id).ToList();
        }
        public void Save(int touristId, int scheduleId)
        {
            TourSchedule schedule = TourScheduleService.GetInstance().GetById(scheduleId);
            _visitedTourRepository.Save(new VisitedTour(touristId, DateOnly.FromDateTime(schedule.Start)));
            CheckForVoucher(touristId, schedule);
        }

        private void CheckForVoucher(int touristId, TourSchedule currentTour)
        {
            if (CountVisitedTours(touristId, currentTour) == 5)
            {
                DeleteAllByTouristId(touristId);
                Tourist tourist = TouristService.GetInstance().GetByTouristId(touristId);
                VoucherService.GetInstance().Save(new Voucher(tourist.Name, tourist.Surname, tourist.Age, DateTime.Now.AddMonths(6), tourist.UserId, DateTime.Now));
            }
        }

        private int CountVisitedTours(int touristId, TourSchedule currentTour)
        {
            int visitedTourCounter = 0;

            foreach (var visitedTour in GetByTouristId(touristId))
            {
                if (IsOutdated(visitedTour, currentTour))
                {
                    _visitedTourRepository.Delete(visitedTour);
                    continue;
                }

                visitedTourCounter++;
            }

            return visitedTourCounter;
        }
        private bool IsOutdated(VisitedTour visitedTour, TourSchedule currentTour)
        {
            return (visitedTour.Date < DateOnly.FromDateTime( currentTour.Start).AddYears(-1));
        }
    }
}
