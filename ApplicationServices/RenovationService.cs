using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class RenovationService
    {
        public IAccommodationRenovationRepository accommodationRenovationRepository;

        public RenovationService(IAccommodationRenovationRepository accommodationRenovationRepository)
        {
            this.accommodationRenovationRepository = accommodationRenovationRepository;
        }

        public static RenovationService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<RenovationService>();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return accommodationRenovationRepository.GetAll();
        }

        public AccommodationRenovation Save(AccommodationRenovation AccommodationRenovation)
        {
            return accommodationRenovationRepository.Save(AccommodationRenovation);
        }

        public void Delete(AccommodationRenovation AccommodationRenovation)
        {
            accommodationRenovationRepository.Delete(AccommodationRenovation);
        }

        public AccommodationRenovation GetByAccommodation(int id)
        {
            return accommodationRenovationRepository.GetByAccommodation(id);
        }

        public List<AccommodationRenovation> GetAllByAccommodation(int id)
        {
            List<AccommodationRenovation> accommodationRenovations = new List<AccommodationRenovation>();

            foreach(AccommodationRenovation ren in accommodationRenovationRepository.GetAll())
            {
                if(ren.AccommodationId == id)
                    accommodationRenovations.Add(ren);
            }

            return accommodationRenovations;
        }

        public List<DateOnly> GetAvailableDatesForRenovation(List<ReservationOwnerDTO> reservations,DateTime StartDate,DateTime EndDate,int duration)
        {
            List<DateOnly> availableDates = new List<DateOnly>();
            availableDates.Add(new DateOnly(2024, 4, 15));
            availableDates.Add(new DateOnly(2024, 4, 15));
            DateTime? startingDay = null;
            DateTime? endDay = null;
            int reqDuration = 0;

            foreach(DateTime day in EachDay(StartDate,EndDate))
            {
                if(startingDay == null)
                    startingDay = day;

                
                if(reqDuration == duration)
                {
                    endDay = day;
                    foreach (ReservationOwnerDTO reservation in reservations)
                    {
                        if ((DateOnly.FromDateTime((DateTime)startingDay) <= reservation.CheckIn && DateOnly.FromDateTime((DateTime)endDay) >= reservation.CheckIn) 
                            ||
                            (DateOnly.FromDateTime((DateTime)startingDay) <= reservation.CheckOut && DateOnly.FromDateTime((DateTime)endDay) >= reservation.CheckOut)
                            ||
                            ((DateOnly.FromDateTime((DateTime)startingDay) >= reservation.CheckIn) && (DateOnly.FromDateTime((DateTime)endDay) <= reservation.CheckOut)))
                        {

                            reqDuration = 0;
                            startingDay = null;
                            endDay = null;
                            break;
                        }
                        else
                        {
                            availableDates[0] = DateOnly.FromDateTime((DateTime)startingDay);
                            availableDates[1] = DateOnly.FromDateTime((DateTime)endDay);
                        }

                    }


                }

                reqDuration++;

 

            }


            return availableDates;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
