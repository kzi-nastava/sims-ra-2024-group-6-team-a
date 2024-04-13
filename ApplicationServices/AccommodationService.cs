using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class AccommodationService
    {
        public IAccommodationRepository AccommodationRepository { get; set; }

        public AccommodationService() 
        {
            AccommodationRepository = new AccommodationRepository();
        }

        public int GetTotalReservationCount(AccommodationReservationRepository reservationRepository, int id)
        {
            int total = 0;
            Accommodation temp = new Accommodation();
            foreach (AccommodationReservation reservation in reservationRepository.GetAll())
            {
                temp = AccommodationRepository.GetByReservationId(reservation.AccommodationId);
                if (temp.OwnerId == id)
                {
                    total++;
                }
            }

            return total;
        }

        public int GetTotalAccommodationCount(int id)
        {
            int total = 0;
            foreach (Accommodation accommodation in AccommodationRepository.GetAll())
            {
                if (accommodation.OwnerId == id)
                {
                    total++;
                }
            }
            return total;
        }


    }
}
