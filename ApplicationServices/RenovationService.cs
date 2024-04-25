using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public int NextId()
        {
            return accommodationRenovationRepository.NextId();
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

        public ObservableCollection<AccommodationRenovation> GetAllByAccommodation(int id)
        {
            ObservableCollection<AccommodationRenovation> accommodationRenovations = new ObservableCollection<AccommodationRenovation>();

            foreach (AccommodationRenovation ren in accommodationRenovationRepository.GetAll())
            {
                if (ren.AccommodationId == id)
                    accommodationRenovations.Add(ren);
            }

            return accommodationRenovations;
        }

        public List<DateOnly?> GetAvailableDatesForRenovation(List<ReservationOwnerDTO> reservations, DateOnly StartDate, DateOnly EndDate, int duration)
        {
            List<DateOnly?> availableDates = new List<DateOnly?>();
            List<DateOnly> reservedDates = new List<DateOnly>();

            availableDates.Add(new DateOnly(2024, 4, 15));
            availableDates.Add(new DateOnly(2024, 4, 15));
            DateOnly? startingDay = null;
            DateOnly? endDay = null;


            bool found = false;


            foreach (ReservationOwnerDTO reservation in reservations)
            {
                foreach (DateOnly day in EachDay(reservation.CheckIn, reservation.CheckOut))
                {
                    if (!reservedDates.Contains(day))
                        reservedDates.Add(day);
                }
            }

            foreach (DateOnly day in EachDay(StartDate, EndDate))
            {
                if (startingDay == null)
                    startingDay = day;

                foreach (DateOnly day_sec in EachDay(StartDate.AddDays(1), EndDate))
                {
                    endDay = day_sec;

                    DateTime startingTime = ((DateOnly)startingDay).ToDateTime(TimeOnly.MinValue);
                    DateTime endingTime = ((DateOnly)endDay).ToDateTime(TimeOnly.MinValue);

                    int differenceInDays = (int)(endingTime - startingTime).TotalDays;

                    if (differenceInDays != duration)
                        continue;
                    else
                    {

                        bool interfere = false;
                        foreach (DateOnly resDay in reservedDates)
                        {
                            if (resDay >= startingDay && resDay <= endDay)
                            {
                                interfere = true;
                                startingDay = null;
                                endDay = null;
                                break;
                            }
                        }

                        if (!interfere)
                        {

                            availableDates[0] = startingDay;
                            availableDates[1] = endDay;
                            return availableDates;
                        }
                        else
                            break;
                    }

                }
            }
            availableDates[0] = null;
            availableDates[1] = endDay;
            return availableDates;


        }

        public IEnumerable<DateOnly> EachDay(DateOnly from, DateOnly thru)
        {
            for (var day = from; day <= thru; day = day.AddDays(1))
                yield return day;
        }

        internal bool IsFiveDays(AccommodationRenovation selectedRenovation)
        {
            DateTime RenovationStart = selectedRenovation.StartDate.ToDateTime(TimeOnly.MinValue);
            double DaysPassedForReview = (RenovationStart - DateTime.Today).TotalDays;
            return DaysPassedForReview > 5.0;
        }
    }
}
