using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces
{
    public interface IReservationChangeRepository
    {
        public List<ReservationChanges> GetAll();
        public ReservationChanges Save(ReservationChanges ReservationChanges);
        public int NextId();
        public void Delete(ReservationChanges ReservationChanges);
        public List<ReservationChanges> GetAllByGuest(int guestId);
        public ReservationChanges Update(ReservationChanges ReservationChanges);
        public ReservationChanges Get(int id);
    }
}
