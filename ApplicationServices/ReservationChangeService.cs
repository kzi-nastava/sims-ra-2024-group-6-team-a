using BookingApp.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Resources;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;

namespace BookingApp.ApplicationServices
{
    public class ReservationChangeService
    {
        private IReservationChangeRepository reservationChangeRepository;
        
        public ReservationChangeService(IReservationChangeRepository reservationChangeRepository)
        {
            this.reservationChangeRepository = reservationChangeRepository;

        }
        public static ReservationChangeService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ReservationChangeService>();
        }
        public List<ReservationChanges> GetAll()
        {
            return reservationChangeRepository.GetAll();
        }
        public int GetNumberOfNotifications(int reservationId)
        {
            int numberOfNotifications = 0;
            foreach (ReservationChanges change in GetAll())
                if (change.ReservationId == reservationId && (change.Status == Enums.ReservationChangeStatus.Rejected || change.Status == Enums.ReservationChangeStatus.Accepted))
                    numberOfNotifications++;
            return numberOfNotifications;
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
        public List<ReservationChanges> GetAllChangesByGuest(int guestId)
        {
            List<ReservationChanges> reservationsChanges = new List<ReservationChanges>();
            List<ReservationChanges> _changes = reservationChangeRepository.GetAll();
            foreach (ReservationChanges change in _changes)
            {
                //if (change.ReservationId.GuestId == guestId)
                //{
                reservationsChanges.Add(change);
                //}
            }
            return reservationsChanges;
        }
        public ReservationChanges Get(int id)
        {
            return reservationChangeRepository.Get(id);
        }
    }
}
