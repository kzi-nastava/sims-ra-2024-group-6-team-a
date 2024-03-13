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
        private ObservableCollection<DateTime> tourSchedules;

        public ObservableCollection<DateTime> TourSchedules
        {
            get { return tourSchedules; }
            set
            {
                if (value != tourSchedules)
                {
                    tourSchedules = value;
                    OnPropertyChanged("TourSchedules");
                }
            }
        }
        public int Id { get; set; }

            public string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    if (value != name)
                    {
                        name = value;
                        OnPropertyChanged("Name");

                    }

                }
            }

            public string description;
            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    if (value != description)
                    {
                        description = value;
                        OnPropertyChanged("Description");

                    }

                }
            }

            public string language;
            public string Language
            {
                get
                {
                    return language;
                }
                set
                {
                    if (value != language)
                    {
                        language = value;
                        OnPropertyChanged("Language");

                    }
                }
            }


            public int capacity;
            public int Capacity
            {
                get
                {
                    return capacity;
                }
                set
                {
                    if (value != capacity)
                    {
                        capacity = value;
                        OnPropertyChanged("Capacity");

                    }
                }

            }


            public double duration;
            public double Duration
            {
                get
                {
                    return duration;
                }
                set
                {
                    if (value != duration)
                    {
                        duration = value;
                        OnPropertyChanged("Duration");

                    }
                }
            }

            public string city;
            public string City
            {
                get
                {
                    return city;
                }

                set
                {
                    if (value != city)
                    {
                        city = value;
                        OnPropertyChanged("City");
                    }

                }
            }
            public string state;

            public string State
            {
                get
                {
                    return state;
                }

                set
                {
                    if (value != state)
                    {
                        state = value;
                        OnPropertyChanged("State");
                    }

                }
            }

            public String image;

            public String Image
            {
                get
                {
                    return image;
                }

                set
                {
                    if (value != image)
                    {
                        image = value;
                        OnPropertyChanged("image");
                    }
                }
            }

        public DateTime start;
        public DateTime Start
        {
            get
            {
                return start;
            }

            set
            {
                if(value != start)
                {
                    start = value;
                    OnPropertyChanged("Start");
                }
            }
        }

            public TourTouristDTO(Tour tour)
            {
                Id = tour.Id;
                name = tour.Name;
                description = tour.Description;
                language = tour.Language;
                capacity = tour.Capacity;
                duration = tour.Duration;
               

            }
        public TourTouristDTO(Tour tour, Location location, TourSchedule tourSchedule)
        {
            Id = tour.Id;
            name = tour.Name;
            description = tour.Description;
            language = tour.Language;
            capacity = tour.Capacity;
            duration = tour.Duration;
            city = location.City;
            state = location.State;
            start = tourSchedule.Start;
            

        }

    }
    }

