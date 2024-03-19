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

        private int reservationId;
        public int ReservationId
        {
            get
            {
                return reservationId;
            }
            set
            {
                if (reservationId != value)
                {
                    reservationId = value;
                    OnPropertyChanged("ReservationId");

                }
            }
        }

        public TourGuestDTO(string name, string surname, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
            this.ReservationId = reservationId;
        }

        public TourGuestDTO()
        {

        }
    }
   
    

}
