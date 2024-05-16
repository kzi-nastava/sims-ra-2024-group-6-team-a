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
    public class TourStatisticsDTO : INotifyPropertyChanged
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


        private int _touristNumber;
        public int TouristNumber
        {
            get
            {
                return _touristNumber;
            }
            set
            {
                if (value != _touristNumber)
                {
                    _touristNumber = value;
                    OnPropertyChanged("TouristNumber");

                }

            }
        }

        private int _children;
        public int Children
        {
            get
            {
                return _children;
            }
            set
            {
                if (value != _children)
                {
                    _children = value;
                    OnPropertyChanged("Children");

                }

            }
        }
        private int _adult;
        public int Adult
        {
            get
            {
                return _adult;
            }
            set
            {
                if (value != _adult)
                {
                    _adult = value;
                    OnPropertyChanged("Adult");

                }

            }
        }

        private int _elderly;
        public int Elderly
        {
            get
            {
                return _elderly;
            }
            set
            {
                if (value != _elderly)
                {
                    _elderly = value;
                    OnPropertyChanged("Elderly");

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

        private int _year;
        
        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                if (value != _year)
                {
                    _year = value;
                    OnPropertyChanged("Year");

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
                if(value!= _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }


        }
        public TourStatisticsDTO(string name,Language language, string image, Location location, int touristNumber, int children, int adult, int elderly)
        {
            _name = name;
            _language = language.Name;
            _image = image;
            _touristNumber = touristNumber;
            _children = children;
            _adult = adult;
            _elderly = elderly;
            _location = location.City + ", " + location.State;
                
        }

        public TourStatisticsDTO() 
        {
            _touristNumber = 0;
        
        }
    }
}
