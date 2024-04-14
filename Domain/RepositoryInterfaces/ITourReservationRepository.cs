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
    public interface ITourReservationRepository
    {
        public List<TourReservation> GetAll();

        public TourReservation Save(TourReservation reservation);

        public int NextId();

        public void Delete(TourReservation reservation);

        public List<TourReservation> GetAllByRealisationId(int tourRealisationId);

        public TourReservation Update(TourReservation reservation);
        public List<TourReservation> GetAllByUser(User user);

        public TourReservation GetById(int id);

        public TourReservation GetBySchedule(int schdeuleId);
    }
}
