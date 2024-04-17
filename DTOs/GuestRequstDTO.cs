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
    public class GuestRequstDTO : INotifyPropertyChanged
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
        private string comment;
        public string Comment
        {
            get { return comment; }

            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }

        }

        private DateOnly newCheckIn;

        public DateOnly NewCheckIn
        {
            get
            {
                return newCheckIn;
            }

            set
            {
                if (value != newCheckIn)
                {
                    newCheckIn = value;
                    OnPropertyChanged("NewCheckIn");
                }
            }
        }

        private DateOnly newCheckOut;
        public DateOnly NewCheckOut
        {
            get
            { return newCheckOut; }

            set
            {
                if (value != newCheckOut)
                {
                    newCheckOut = value;
                    OnPropertyChanged("NewCheckOut");
                }
            }
        }

        private DateOnly oldCheckIn;

        public DateOnly OldCheckIn
        {
            get
            {
                return oldCheckIn;
            }

            set
            {
                if (value != oldCheckIn)
                {
                    oldCheckIn = value;
                    OnPropertyChanged("OldCheckIn");
                }
            }
        }

        private DateOnly oldCheckOut;
        public DateOnly OldCheckOut
        {
            get
            { return oldCheckOut; }

            set
            {
                if (value != oldCheckOut)
                {
                    oldCheckOut = value;
                    OnPropertyChanged("OldCheckOut");
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
        public Enums.ReservationChangeStatus reservationChangeStatus;

        public Enums.ReservationChangeStatus ReservationChangeStatus
        {
            get { return reservationChangeStatus; }

            set
            {
                if (value != reservationChangeStatus)
                {
                    reservationChangeStatus = value;
                    OnPropertyChanged("ReservationChangeStatus");
                }
            }
        }
        public GuestRequstDTO(Guest guest, ReservationChanges reservationChange, Accommodation accommodation, String image)
        {
            //dodaj i za guest
            this.newCheckOut = reservationChange.NewCheckOut;
            this.newCheckIn = reservationChange.NewCheckIn;
            this.oldCheckOut = reservationChange.OldCheckOut;
            this.oldCheckIn = reservationChange.OldCheckIn;
            this.comment = reservationChange.Comment;
            this.Id = reservationChange.ReservationId;
            this.AccommodationId = reservationChange.AccommodationId;
            this.accommodationName = accommodation.Name;
            this.ReservationChangeStatus = reservationChange.Status;
            this.image = image;
        }

    }
}
