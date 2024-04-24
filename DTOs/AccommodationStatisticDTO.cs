using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class AccommodationStatisticDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int year;

        public int Year
        {
            get { return year; } 
            set
            {
                if(year != value) 
                {
                year = value;
                }
            }
        }

        private int reservationCount;

        public int ReservationCount
        {
            get { return reservationCount; }

            set
            {
                if (reservationCount != value)
                {
                    reservationCount = value;
                }
            }
        }

        public int cancelationCount;

        public int CancelationCount
        {
            get { return cancelationCount; }

            set 
            {
                if(cancelationCount != value)
                {
                    cancelationCount = value;
                }
            }
        }

        private int changesCount;

        public int ChangesCount
        {
            get { return changesCount; }

            set
            {
                if(changesCount != value)
                {
                    changesCount = value;
                }
            }
        }

        private int renovationSuggestions;

        public int RenovationSuggestions
        {
            get { return renovationSuggestions; }

            set
            {
                if (renovationSuggestions != value)
                {
                    renovationSuggestions = value;
                }
            }
        }

        public AccommodationStatisticDTO(int year,int reservationCount,int changesCount,int cancelationCount,int renovationSuggestions)
        {
            this.year = year;
            this.reservationCount = reservationCount;
            this.changesCount = changesCount;
            this.cancelationCount = cancelationCount;
            this.renovationSuggestions = renovationSuggestions;
        }


    }
}
