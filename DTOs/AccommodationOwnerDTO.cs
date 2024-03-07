using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class AccommodationOwnerDTO : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int Id {  get; set; }

        public string name;
        public string Name
        {
            get 
            { 
                return name;
            }

            set
            {
                if(value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public Enums.AccommodationType type;

        public Enums.AccommodationType Type
        {
            get
            { 
                return type;
            }

            set
            {
                if(value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public int maxGuests;

        public int MaxGuests
        {
            get
            {
                return maxGuests;
            }

            set
            {
                if (value != maxGuests)
                {
                    maxGuests = value;
                    OnPropertyChanged("MaxGuests");
                }
            }
        }

        public int minReservationDays;
        public int MinReservationDays
        {
            get
            {
                return minReservationDays;
            }

            set
            {
                if (value != minReservationDays)
                {
                    minReservationDays = value;
                    OnPropertyChanged("MinReservationDays");
                }
            }
        }

        public int cancelationDays;

        public int CancelationDays
        {
            get
            {
                return cancelationDays;
            }

            set
            {
                if (value != cancelationDays)
                {
                    cancelationDays = value;
                    OnPropertyChanged("CancelationDays");
                }
            }
        }

        public string city;

        public string City
        {
            get
            { 
                return city;
            }

            set
            {
                if(value != city)
                {
                    city = value;
                    OnPropertyChanged("City");
                }

            }
        }
        public string state;

        public string State
        {
            get
            {
                return state;
            }

            set
            {
                if (value != state)
                {
                    state = value;
                    OnPropertyChanged("state");
                }

            }
        }


        public AccommodationOwnerDTO(Accommodation accommodation,Location location)
        {
            Id = accommodation.Id;
            name = accommodation.Name;
            type = accommodation.Type;
            maxGuests = accommodation.MaxGuests;
            minReservationDays=accommodation.MinReservationDays;
            cancelationDays=accommodation.CancelationDays;
            city = location.City;
            state = location.State;

        }


    }
}
