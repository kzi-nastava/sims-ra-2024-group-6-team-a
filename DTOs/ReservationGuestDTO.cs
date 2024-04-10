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
    public class ReservationGuestDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }

        private string accommodationName;
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
        public int guestNumber;

        public int GuestNumber
        {
            get
            {
                return guestNumber;
            }

            set
            {
                if (value != guestNumber)
                {
                    guestNumber = value;
                    OnPropertyChanged("guestNumber");
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
                    OnPropertyChanged("cancelationDays");
                }

            }
        }

        public int minNumberOfDays;

        public int MinNumberOfDays
        {
            get
            {
                return minNumberOfDays;
            }

            set
            {
                if (value != minNumberOfDays)
                {
                    minNumberOfDays = value;
                    OnPropertyChanged("minNumberOfDays");
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

        public Enums.ReservationStatus reservationStatus;

        public Enums.ReservationStatus ReservationStatus
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

        public Enums.AccommodationType type;
        public Enums.AccommodationType Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public ReservationGuestDTO(Guest guest, AccommodationReservation reservation, Accommodation accommodation, Location location, String image)
        {
            //dodaj i za guest
            this.checkIn = reservation.CheckInDate;
            this.checkOut = reservation.CheckOutDate;
            this.Id = reservation.Id;
            this.AccommodationId = reservation.AccommodationId;
            this.guestNumber = reservation.GuestNumber;
            this.accommodationName = accommodation.Name;
            this.type = accommodation.Type;
            this.CancelationDays = accommodation.CancelationDays;
            this.MinNumberOfDays = accommodation.MinReservationDays;
            this.city = location.City;
            this.state = location.State;
            this.reservationStatus = reservation.Status;
            this.image = image;
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
