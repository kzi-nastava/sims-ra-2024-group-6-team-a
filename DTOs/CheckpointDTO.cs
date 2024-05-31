using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class CheckpointDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private int _tourId;
        public int TourId
        {
            get
            {
                return _tourId;
            }
            set
            {
                if (value != _tourId)
                { 
                    _tourId = value;
                    OnPropertyChanged("TourId");
                }
            }
        }

        private bool _isReached;
        public bool IsReached
        {
            get
            {
                return _isReached;
            }

            set
            {
                if (value !=  _isReached)
                {
                    _isReached = value;
                    OnPropertyChanged("IsReached");
                }
            }
        }

        private int _tourSheduleId;
        public int TourSheduleId
        {
            get
            {
                return _tourSheduleId;
            }
            set 
            { 
                if(value != _tourSheduleId)
                {
                    _tourSheduleId = value;
                    OnPropertyChanged("TourScheduleId");
                }
            }
        }

        private bool _isLastReached;
        public bool IsLastReached
        {
            get
            {
                return _isLastReached;
            }

            set
            {
                if( value != _isLastReached)
                {
                    _isLastReached = value;
                    OnPropertyChanged("IsLastReached");
                }
            }
        }
        public CheckpointDTO() { }

        public CheckpointDTO(Checkpoint checkpoint)
        {
            Name = checkpoint.Name;
            TourId = checkpoint.TourId;
            IsReached = checkpoint.IsReached;
            TourSheduleId = checkpoint.TourScheduleId;
        }
    }
}
