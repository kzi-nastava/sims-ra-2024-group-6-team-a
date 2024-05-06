using BookingApp.Domain.Model;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookingApp.DTOs
{
    public class TourDetailsDTO : INotifyPropertyChanged
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

        public string _name;

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

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
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

        private List<string> _checkpoints;

        public List<string> Checkpoints
        {
            get
            {
                return _checkpoints;
            }
            set
            {
                if (value != _checkpoints)
                {
                    _checkpoints = value;
                    OnPropertyChanged("Checkpoints");
                }

            }
        }

        private List<DateTime> _dateRealisations;

        public List<DateTime> DateRealisations
        {
            get
            {
                return _dateRealisations;
            }
            set
            {
                if (value != _dateRealisations)
                {
                    _dateRealisations = value;
                    OnPropertyChanged("DateRealisations");
                }

            }
        }

        private List<BitmapImage> _images; 

        public List<BitmapImage> Images
        {
            get
            {
                return _images;
            }
            set
            {
                if (value != _images)
                {
                    _images = value;
                    OnPropertyChanged("Images");
                }

            }
        }

        public TourDetailsDTO(Tour tour,Language language,Location location,List<string> checkpoints, List<DateTime> dateRealisations)
        {
            Name = tour.Name;
            Language = language.Name;
            Location = location.City + ", " + location.State;
            Duration = tour.Duration;
            Capacity = tour.Capacity;
            Description = tour.Description;
            Checkpoints = checkpoints;
            DateRealisations = dateRealisations;
            //Images = images;
        }









    }
}
