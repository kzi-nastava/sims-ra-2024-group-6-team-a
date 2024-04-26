using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.OwnerViewModel
{
    public class ScheduleRenovationVM : INotifyPropertyChanged
    {
        List<DateOnly?> availableDates = new List<DateOnly?>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public AccommodationOwnerDTO Accommodation { get; set; }
        public List<ReservationOwnerDTO> Reservations { get; set; }
        public bool isPeriodFound;

        public ScheduleRenovationVM(AccommodationOwnerDTO Accommodation, List<ReservationOwnerDTO> Reservations)
        {
            this.Accommodation = Accommodation;
            this.Reservations = Reservations;
            isPeriodFound = false;
        }

        public string FindDates(DateTime? StartDate,DateTime? EndDate,String Duration)
        {
            int duration;
            if (StartDate != null && EndDate != null && int.TryParse(Duration, out duration))
            {
                availableDates = RenovationService.GetInstance().GetAvailableDatesForRenovation(Reservations, DateOnly.FromDateTime((DateTime)StartDate), DateOnly.FromDateTime((DateTime)EndDate), duration);
                if (availableDates[0] != null)
                {
                    isPeriodFound = true;
                    return "There is an available period between " + ((DateOnly)availableDates[0]).ToString("dd MMMM,yyyy") + " and " + ((DateOnly)availableDates[1]).ToString("dd MMMM,yyyy");
                    
                }
                else
                {
                    isPeriodFound = true;
                    return  "There is no available period between selected dates for the duration.";
                    
                }
            }
            else
            {
                isPeriodFound = true;
                return  "Please make sure you have selected dates and entered a number for the duration.";
                
            }
        }

        internal void ConfirmSchedule(String Title)
        {
            AccommodationRenovation renovation = new AccommodationRenovation(RenovationService.GetInstance().NextId(), Title, Accommodation.Id, (DateOnly)availableDates[0], (DateOnly)availableDates[1]);
            RenovationService.GetInstance().Save(renovation);
        }
    }
}
