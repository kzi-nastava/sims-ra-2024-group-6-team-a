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
    public class TourScheduleDTO : INotifyPropertyChanged
    {

        public TourScheduleDTO() { }

        public TourScheduleDTO(TourSchedule tourSchedule)
        {
            Id = tourSchedule.Id;
            Start = tourSchedule.Start;
            CurrentGuestNumber = tourSchedule.CurrentGuestNumber;
            TourId = tourSchedule.TourId;
        }

        public string TourScheduleDisplay => $"{Start}";

        private int _tourId;
        public int TourId
        {
            get
            {
                return _tourId;
            }
            set
            {
                if ( _tourId != value )
                {
                    _tourId = value;
                    OnPropertyChanged("TourId");
                }
            }
        }
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");

                }
            }

        }

        private DateTime _start;
        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                if (value != _start)
                {
                    _start = value;
                    OnPropertyChanged("Start");

                }
            }

        }

        private int _currentGuestNumber;
        public int CurrentGuestNumber
        {
            get
            {
                return _currentGuestNumber;
            }
            set
            {
                if (value != _currentGuestNumber)
                {
                    _currentGuestNumber = value;
                    OnPropertyChanged("CurrentGuestNumber");
                }
            }
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
