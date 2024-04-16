using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        public List<AccommodationReservation> GetAll();

        public AccommodationReservation Save(AccommodationReservation reservation);

        public int NextId();

        public void Delete(AccommodationReservation reservation);

        public AccommodationReservation Update(AccommodationReservation AccommodationReservation);



    }
}
