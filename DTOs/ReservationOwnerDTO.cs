using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Resources;

namespace BookingApp.DTOs
{
    public class ReservationOwnerDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int Id { get; set; }

        private string guestName;
        public string GuestName
        {
            get
            {
                return guestName;
            }

            set
            {
                if (value != guestName)
                {
                    guestName = value;
                    OnPropertyChanged("GuestName");
                }
            }
        }

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }

            set
            {
                if(value != accommodationName) 
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
                if(value != checkIn) 
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
            { return checkOut;}

            set
            {
                if(value != checkOut)
                {
                    checkOut = value;
                    OnPropertyChanged("CheckOut");
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

        public String reservationStatus;

        public String ReservationStatus
        {
            get { return reservationStatus; }

            set
            {
                if (value != reservationStatus)
                {
                    reservationStatus = value;
                    OnPropertyChanged("reservationStatus");
                }
            }
        }


        public ReservationOwnerDTO(string guestName,AccommodationReservation reservation,string accommodationName,Location location,String image)
        {
            this.guestName = guestName;
            this.checkIn = reservation.CheckInDate;
            this.checkOut = reservation.CheckOutDate;
            this.Id = reservation.Id;
            this.accommodationName = accommodationName;
            this.city = location.City;
            this.state = location.State;
            this.image = image;


            if (reservation.Status == Enums.ReservationStatus.Active && reservation.CheckOutDate >= DateOnly.FromDateTime(DateTime.Now))
                ReservationStatus = "#64d9a8";
            else if (reservation.Status == Enums.ReservationStatus.Active && reservation.CheckOutDate < DateOnly.FromDateTime(DateTime.Now))
                ReservationStatus = "#b8d1ce";
            else
                ReservationStatus = "#e6a8b4";

        }
    }
}
