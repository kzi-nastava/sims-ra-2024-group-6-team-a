using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.DTOs
{
    public class TourReservationDTO : INotifyPropertyChanged
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

        private int _tourRealisationId;
        public int TourRealisationId
        {
            get
            {
                return _tourRealisationId;
            }
            set
            {
                if(value != _tourRealisationId)
                {
                    _tourRealisationId = value;
                    OnPropertyChanged("TourRealisationId");
                }
            }
        }

        private int _guestNumber;
        public int GuestNumber
        {
            get
            {
                return _guestNumber;
            }
            set
            {
                if (value != _guestNumber)
                {
                    _guestNumber = value;
                    OnPropertyChanged("GuestNumber");

                }
            }

        }
        public int tourId;
        public int TourId
        {
            get
            {
                return tourId;
            }
            set
            {
                if (value != tourId)
                {
                    tourId = value;
                    OnPropertyChanged("TourId");

                }
            }

        }

        public int touristId;
        public int TouristId
        {
            get
            {
                return touristId;
            }
            set
            {
                if (value != touristId)
                {
                    touristId = value;
                    OnPropertyChanged("TouristId");

                }
            }

        }


    }
}
