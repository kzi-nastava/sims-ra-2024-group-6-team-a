using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourScheduleRepository
    {
        public List<TourSchedule> GetAll();

        public TourSchedule Save(TourSchedule schedule);

        public int NextId();

        public void Delete(TourSchedule schedule);

        public TourSchedule Update(TourSchedule schedule);

        public int FindOngoingTour(List<Tour> tours);
        public List<TourSchedule> GetAllByTourId(int tourId);


        public TourSchedule GetByTour(Tour tour);
        public TourSchedule GetById(int id);
    }
}
