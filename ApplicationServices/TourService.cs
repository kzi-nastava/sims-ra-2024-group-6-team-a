using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
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
       
        public TourService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
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
            
            return currentTour.LocationId == tour.LocationId && tour.Id != schedule.TourId;
        }

        private bool IsFiltered(Tour tour, TourFilterDTO filter)
        {
            return MatchesLocation(tour, filter) &&
                   MatchesDuration(tour, filter) &&
                   MatchesLanguage(tour, filter) &&
                   MatchesMaxTouristNumber(tour, filter);
        }

        private bool MatchesLocation(Tour tour, TourFilterDTO filter)
        {
            return tour.LocationId == filter.Location.Id || filter.Location.Id == 0;
        }

        private bool MatchesDuration(Tour tour, TourFilterDTO filter)
        {
            return tour.Duration <= filter.Duration || filter.Duration == 0;
        }

        private bool MatchesLanguage(Tour tour, TourFilterDTO filter)
        {
            return tour.LanguageId == filter.Language.Id || filter.Language.Id == 0;
        }

        private bool MatchesMaxTouristNumber(Tour tour, TourFilterDTO filter)
        {
            return tour.Capacity >= filter.TouristNumber || filter.TouristNumber == 0;
        }
        private List<Tour> GetAllForUser()
        {
            List<Tour> tours = new List<Tour>();
            foreach(Tour tour in GetAll())
            {
                if(tour.Type == Enums.TourType.Ordinary || tour.Type == Enums.TourType.Statistics)
                    tours.Add(tour);
            }
            return tours;
        }
        public List<Tour> GetFiltered(TourFilterDTO filter)
        {
            List<Tour> allTours = GetAllForUser();

            if (filter.isEmpty())
                return allTours;

            return allTours.Where(t => IsFiltered(t, filter)).ToList();
        }
    }
}
