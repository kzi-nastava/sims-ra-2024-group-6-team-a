using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces
{
    public interface IAccommodationRenovationRepository
    {
        public List<AccommodationRenovation> GetAll();
        public AccommodationRenovation Save(AccommodationRenovation AccommodationRenovation);
        public int NextId();
        public void Delete(AccommodationRenovation AccommodationRenovation);
        public AccommodationRenovation GetByAccommodation(int id);
    }
}
