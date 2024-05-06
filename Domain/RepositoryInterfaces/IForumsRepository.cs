using BookingApp.Domain.Model;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumsRepository
    {
        public List<Forums> GetAll();
        public Forums Save(Forums reservation);
        public int NextId();
        public void Delete(Forums reservation);
        public Forums Update(Forums AccommodationReservation);
        public Forums Get(int id);

    }
}
