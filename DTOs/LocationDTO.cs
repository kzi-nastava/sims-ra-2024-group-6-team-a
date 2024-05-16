using BookingApp.Model;
using System.ComponentModel;

namespace BookingApp.DTOs
{
    public class LocationDTO : INotifyPropertyChanged
    {
        public LocationDTO() { }

        public LocationDTO(Location location)
        {
            Id = location.Id;
            City = location.City;
            State = location.State;
        }

        private int _id = 0;
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

        public string LocationDisplayFormat => $"{City} - {State}";

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
