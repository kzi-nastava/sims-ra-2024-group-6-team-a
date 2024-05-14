using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using System;
using System.ComponentModel;

namespace BookingApp.DTOs
{
    public class TourScheduleDTO : INotifyPropertyChanged
    {
        private readonly TourRepository _tourRepository = new TourRepository();
        public TourScheduleDTO() { }

        public TourScheduleDTO(TourSchedule tourSchedule)
        {
            Id = tourSchedule.Id;
            Start = tourSchedule.Start;
            CurrentFreeSpace = tourSchedule.CurrentFreeSpace;
            TourId = tourSchedule.TourId;
            TourName = _tourRepository.GetById(TourId).Name;

        }

        public TourScheduleDTO(TourSchedule tourSchedule, Location location, Language langugae, string imagePath)
        {
            Id = tourSchedule.Id;
            Start = tourSchedule.Start;
            CurrentFreeSpace = tourSchedule.CurrentFreeSpace;
            TourId = tourSchedule.TourId;
            TourName = _tourRepository.GetById(TourId).Name;
            TourLanguage = langugae.Name;
            City = location.City;
            State = location.State;
            Image = imagePath;
            Duration = _tourRepository.GetById(TourId).Duration;
        }

        private string _image;

        public string Image
        {
            get
            {
                return _image;
            }

            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged("Image");
                }
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }

            }
        }
        private string _state;

        public string State
        {
            get
            {
                return _state;
            }

            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged("State");
                }

            }
        }



        private string _language;
        public string TourLanguage
        {
            get
            {
                return _language;
            }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged("TourLanguage");
                }
            }
        }
        public string TourScheduleDisplay => $"{Start}";

        private string _tourName;
        public string TourName
        {
            get
            {
                return _tourName;
            }
            set
            {
                if (_tourName != value)
                {
                    _tourName = value;
                    OnPropertyChanged("TourName");
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
                if ( _tourId != value )
                {
                    _tourId = value;
                    OnPropertyChanged("TourId");
                }
            }
        }

        private double _duration;
        public double Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
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

        private Enums.TourActivity _activity;
        public Enums.TourActivity Activity
        {
            get
            {
                return _activity;
            }
            set
            {
                if (value != _activity)
                {
                    _activity = value;
                    OnPropertyChanged("Activity");

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

        private int _currentFreeSpace;
        public int CurrentFreeSpace
        {
            get
            {
                return _currentFreeSpace;
            }
            set
            {
                if (value != _currentFreeSpace)
                {
                    _currentFreeSpace = value;
                    OnPropertyChanged("CurrentFreeSpace");
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
