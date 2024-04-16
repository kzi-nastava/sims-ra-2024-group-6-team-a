using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
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
    public class TourService
    {
        private ITourRepository _tourRepository;
        private LocationService _locationService;
       
        public TourService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
            _locationService = LocationService.GetInstance();
        }

        public TourService()
        {
            _tourRepository = new TourRepository();

        }

        public static TourService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourService>();
        }
        public Tour Save(Tour tour)
        {
           return _tourRepository.Save(tour);
        }

        public void Delete(Tour tour)
        {
            _tourRepository.Delete(tour);
        }
        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }
        public List<Tour> GetAllByUser(User user)
        {
            return _tourRepository.GetAllByUser(user);
        }

        public Tour GetById(int id)
        {
            return _tourRepository.GetById(id);
        }

        public List<Tour> GetSameLocationTours(TourScheduleDTO schedule)
        {
            return GetAll().Where(t => HasSameLocation(t, schedule)).ToList();
        }

        private bool HasSameLocation(Tour tour, TourScheduleDTO schedule)
        {
            Tour currentTour = GetById(schedule.TourId);
            string currentTourCity = _locationService.GetById(currentTour.LocationId).City;
            string otherTourCity = _locationService.GetById(tour.LocationId).City;

            return otherTourCity == currentTourCity && tour.Id != schedule.TourId;
        }

      
    }
}
