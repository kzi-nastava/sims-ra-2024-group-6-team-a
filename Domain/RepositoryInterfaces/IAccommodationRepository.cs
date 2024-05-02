using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        public List<Accommodation> GetAll();
        public Accommodation Save(Accommodation accommodation);
        public int NextId();
        public void Delete(Accommodation accommodation);
        public List<Accommodation> GetByOwnerId(int id);
        public Accommodation GetByReservationId(int id);
        public void Delete(int id);
    }
}
