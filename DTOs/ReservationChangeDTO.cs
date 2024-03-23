using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class ReservationChangeDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int ReservationID { get; set; }

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
                if (value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }
            }

        }

        private string oldDate;

        public string OldDate
        {
            get { return oldDate; }

            set
            {
                if(value != oldDate)
                {
                    oldDate = value;
                    OnPropertyChanged("OldDate");
                }
            }
        }

        private string newDate;

        public string NewDate
        {
            get { return newDate; }

            set
            {
                if (value != newDate)
                {
                    newDate = value;
                    OnPropertyChanged("NewDate");
                }
            }
        }


        public string bookedStatus;

        public string BookedStatus
        {
            get { return bookedStatus; }

            set
            {
                if( value != bookedStatus)
                {
                    bookedStatus = value;
                    OnPropertyChanged("BookedStatus");
                }
            }
        }

        public ReservationChangeDTO(int id,string guestName, string accommodationName, string oldDate,  string newDate,  string bookedStatus)
        {
            ReservationID = id;
            GuestName = guestName;
            AccommodationName = accommodationName;
            OldDate = oldDate;
            NewDate = newDate;
            BookedStatus = bookedStatus;
        }
    }
}
