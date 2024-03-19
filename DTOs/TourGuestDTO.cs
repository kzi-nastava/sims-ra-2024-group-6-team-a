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
    public class TourGuestDTO : INotifyPropertyChanged
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
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");

                }
            }
        }
        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged("Surname");

                }
            }
        }
        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged("Age");

                }
            }
        }

        private int _reservationId;
        public int ReservationId
        {
            get
            {
                return _reservationId;
            }
            set
            {
                if (_reservationId != value)
                {
                    _reservationId = value;
                    OnPropertyChanged("ReservationId");

                }
            }
        }
        public TourGuestDTO(string name, string surname, int age, int reservationId)
        {
            _name = name;
            _surname = surname;
            _age = age;
            _reservationId = reservationId;
        }

        //public TourGuestDTO(string name, string surname, int age)
        //{
        //    _name = name;
        //    _surname = surname;
        //    _age = age;
        //    _reservationId = reservationId;
        //}

        public TourGuestDTO()
        {

        }
    }
   
    

}
