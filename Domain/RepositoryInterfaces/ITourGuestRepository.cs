using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourGuestRepository
    {
        public List<TourGuests> GetAll();
        public TourGuests Save(TourGuests guest);

        public int NextId();

        public void Delete(TourGuests guest);

        public List<TourGuests> GetAllByTourId(int tourRealisationId);
        public TourGuests Update(TourGuests guest);

        public List<TourGuests> GetAllByReservationId(int reservationId);
    }
}
