using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.DTOs
{
    class NotificationDTO : INotifyPropertyChanged
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

        private List<TourGuests> _tourGuests;
        public List<TourGuests> TourGuests
        {
            get
            {
                return _tourGuests;
            }
            set
            {
                if (value != _tourGuests)
                {
                    _tourGuests = value;
                    OnPropertyChanged("TourGuests");

                }
            }
        }
    }
}
