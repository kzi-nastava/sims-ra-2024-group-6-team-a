using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class MonthlyStatisticDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string month;

        public string Month
        {
            get { return month; }
            set
            {
                if (month != value)
                {
                    month = value;
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
                if (cancelationCount != value)
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
                if (changesCount != value)
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

        public MonthlyStatisticDTO(int month, int reservationCount, int changesCount, int cancelationCount, int renovationSuggestions)
        {
            switch (month)
            {
                case 1:
                    this.month = "January";
                    break;
                case 2:
                    this.month = "February";
                    break;
                case 3:
                    this.month = "March";
                    break;
                case 4:
                    this.month = "April";
                    break;
                case 5:
                    this.month = "May";
                    break;
                case 6:
                    this.month = "June";
                    break;
                case 7:
                    this.month = "July";
                    break;
                case 8:
                    this.month = "August";
                    break;
                case 9:
                    this.month = "September";
                    break;
                case 10:
                    this.month = "October";
                    break;
                case 11:
                    this.month = "November";
                    break;
                case 12:
                    this.month = "December";
                    break;
                default:
                    this.month = "Invalid Month";
                    break;
            }
            this.reservationCount = reservationCount;
            this.changesCount = changesCount;
            this.cancelationCount = cancelationCount;
            this.renovationSuggestions = renovationSuggestions;
        }


    }
}
