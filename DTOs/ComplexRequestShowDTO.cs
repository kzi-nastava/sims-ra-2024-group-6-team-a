using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class ComplexRequestShowDTO : INotifyPropertyChanged
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

        private string _touristName;

        public string TouristName
        {
            get
            {
                return _touristName;
            }
            set
            {
                if (value != _touristName)
                {
                    _touristName = value;
                    OnPropertyChanged("TouristName");
                }
            }
        }

        private string _touristLastName;
        public string TouristLastName 
        {
            get
            {
                return _touristLastName;
            }
            set
            {
                if (value != _touristLastName)
                {
                    _touristLastName = value;
                    OnPropertyChanged("TouristLastName");
                }
            }   
        }

        private int _numberOfFragments;

        public int NumberOfFragments
        {
            get
            {
                return _numberOfFragments;
            }
            set
            {
                if (value != _numberOfFragments)
                {
                    _numberOfFragments = value;
                    OnPropertyChanged("NumberOfFragments");
                }
            }
        }
        public ComplexRequestShowDTO() { }

        public ComplexRequestShowDTO(int id, string touristName, string touristLastName, int numberOfFragments)
        {
            Id = id;
            TouristName = touristName;
            TouristLastName = touristLastName;
            NumberOfFragments = numberOfFragments;
        }
    }
}
