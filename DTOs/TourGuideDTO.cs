using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class TourGuideDTO : INotifyPropertyChanged
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

        public TourGuideDTO(Tour tour, Location location, String image)
        {
            Id = tour.Id;
            name = tour.Name;
            description = tour.Description;
            language = tour.Language;
            capacity = tour.Capacity;
            duration = tour.Duration;
            city = location.City;
            state = location.State;
            this.image = image;

        }
        public TourGuideDTO(Tour tour, Location location)
        {
            Id = tour.Id;
            name = tour.Name;
            description = tour.Description;
            language = tour.Language;
            capacity = tour.Capacity;
            duration = tour.Duration;
            city = location.City;
            state = location.State;

        }
    }
}
