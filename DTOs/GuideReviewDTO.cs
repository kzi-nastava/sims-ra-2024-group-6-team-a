using BookingApp.Domain.Model;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class GuideReviewDTO :INotifyPropertyChanged
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

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");

                }

            }
        }

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");

                }
            }
        }


        private int _capacity;
        public int Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                if (value != _capacity)
                {
                    _capacity = value;
                    OnPropertyChanged("Capacity");

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
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");

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

        private String _image;

        public String Image
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

        private DateTime _tourStart;

        public DateTime TourStart
        {
            get
            {
                return _tourStart;
            }

            set
            {
                if (value != _tourStart)
                {
                    _tourStart = value;
                    OnPropertyChanged("TourStart");

                }
            }
        }

        private int _tourScheduleId;

        public int TourScheduleId
        {
            get
            {
                return _tourScheduleId;
            }

            set
            {
                if (value != _tourScheduleId)
                {
                    _tourScheduleId = value;
                    OnPropertyChanged("TourScheduleId");

                }
            }

        }

        private double _avgGrade;
        public double AvgGrade
        {
            get
            {
                return _avgGrade;
            }

            set
            {
                if (value != _avgGrade)
                {
                    _avgGrade = value;
                    OnPropertyChanged("AvgGrade");

                }
            }
        }

        public GuideReviewDTO(Tour tour,Language language ,Location location, string imagePath, DateTime tourStart, int tourScheduleId,double avgGrade)
        {
            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            Language = language.Name;
            Capacity = tour.Capacity;
            Duration = tour.Duration;
            City = location.City;
            State = location.State;
            Image = imagePath;
            TourStart = tourStart;
            TourScheduleId = tourScheduleId;
            AvgGrade = avgGrade;
        }



    }
}
