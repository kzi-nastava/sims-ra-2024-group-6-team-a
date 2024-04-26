using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.DTOs
{
    
        public class TourTouristDTO : INotifyPropertyChanged
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
        private DateTime _start;
        public DateTime Start
        {
            get
            {
                return _start;
            }

            set
            {
                if(value != _start)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                }
            }
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

        public TourTouristDTO(Tour tour, Location location, TourSchedule tourSchedule)
        {
            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            //Language = tour.Language;
            Capacity = tour.Capacity;
            Duration = tour.Duration;
            City = location.City;
            State = location.State;
            Start = tourSchedule.Start;   
        }
        public TourTouristDTO(Tour tour, Location location, TourSchedule tourSchedule, string imagePath)
        {
            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            //Language = tour.Language;
            Capacity = tour.Capacity;
            Duration = tour.Duration;
            City = location.City;
            State = location.State;
            Start = tourSchedule.Start;
            Image = imagePath;
        }


    }
    }

