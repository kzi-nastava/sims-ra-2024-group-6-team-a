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

        public string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");

                }
            }
        }
        public string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged("Surname");

                }
            }
        }
        public int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged("Age");

                }
            }
        }

        public int reservationId;
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
        public TourGuestDTO(string name, string surname, int age, int reservationId)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.reservationId = reservationId;
        }

        public TourGuestDTO(string name, string surname, int age)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.reservationId = reservationId;
        }

    }

    

}
