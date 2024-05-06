using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BookingApp.Model;
using BookingApp.Resources;

namespace BookingApp.DTOs
{
    public class GuestWheneverDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public int AccommodationId { get; set; }
        public int GuestNumber { get; set; }
        private string accommodationName;
        private Enums.AccommodationType type;
        public Enums.AccommodationType Type
        {
            get { return type; }

            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }

        }
        public string AccommodationName
        {
            get { return accommodationName; }

            set
            {
                if (value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }
            }

        }
        private DateOnly checkIn;
        public DateOnly CheckIn
        {
            get
            {
                return checkIn;
            }

            set
            {
                if (value != checkIn)
                {
                    checkIn = value;
                    OnPropertyChanged("CheckIn");
                }
            }
        }

        private DateOnly checkOut;
        public DateOnly CheckOut
        {
            get
            { return checkOut; }

            set
            {
                if (value != checkOut)
                {
                    checkOut = value;
                    OnPropertyChanged("CheckOut");
                }
            }
        }
        public String image;
        public String Image
        {
            get
            {
                return image;
            }

            set
            {
                if (value != image)
                {
                    image = value;
                    OnPropertyChanged("image");
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
                if (value != city)
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

        public GuestWheneverDTO( DateOnly CheckInDate, DateOnly CheckOutDate, Accommodation accommodation, Location location, String image, int guestNumber)
        {
            this.checkOut = CheckOutDate;
            this.checkIn = CheckInDate;
            this.AccommodationId = accommodation.Id;
            this.accommodationName = accommodation.Name;
            this.Type = accommodation.Type;
            this.image = image;
            this.city = location.City;
            this.state = location.State;
            this.GuestNumber = guestNumber;
        }



    }
}
