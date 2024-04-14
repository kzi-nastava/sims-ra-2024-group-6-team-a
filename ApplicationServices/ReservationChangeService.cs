using BookingApp.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.ApplicationServices
{
    public class ReservationChangeService
    {
        private IReservationChangeRepository reservationChangeRepository {  get; set; }
        
        public ReservationChangeService()
        {
            reservationChangeRepository = new ReservationChangeRepository();
        }

        public List<ReservationChanges> GetAll()
        {
            return reservationChangeRepository.GetAll();
        }

        public ReservationChanges Save(ReservationChanges ReservationChanges)
        {
            return reservationChangeRepository.Save(ReservationChanges);
        }

        public void Delete(ReservationChanges ReservationChanges) 
        {
            reservationChangeRepository.Delete(ReservationChanges);
        }

        public List<ReservationChanges> GetAllByGuest(int guestId)
        {
            return reservationChangeRepository.GetAllByGuest(guestId);
        }

        public ReservationChanges Update(ReservationChanges ReservationChanges)
        {
            return reservationChangeRepository.Update(ReservationChanges);
        }

        public ReservationChanges Get(int id)
        {
            return reservationChangeRepository.Get(id);
        }
    }
}
