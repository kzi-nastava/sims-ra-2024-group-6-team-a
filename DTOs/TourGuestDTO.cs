﻿using BookingApp.Model;
using System.ComponentModel;

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

        private int _requestId;
        public int RequestId
        {
            get
            {
                return _requestId;
            }
            set
            {
                if (value != _requestId)
                {
                    _requestId = value;
                    OnPropertyChanged("RequestId");
                }
            }
        }

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

        private int _userType;
        public int UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                if (_userType != value)
                {
                    _userType = value;
                    OnPropertyChanged("UserType");

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
        private int _checkpointId;
        public int CheckpointId
        {
            get
            {
                return _checkpointId;
            }
            set
            {
                if (_checkpointId != value)
                {
                    _checkpointId = value;
                    OnPropertyChanged("CheckpointId");

                }
            }
        }
        private bool _present;
        public bool Present
        {
            get
            {
                return _present;
            }
            set
            {
                if(_present != value)
                {
                    _present = value;
                    OnPropertyChanged("Present");
                }
            }
        }

        public TourGuestDTO(TourGuests guest)
        {
            Id = guest.Id;
            Name = guest.Name;
            Surname = guest.Surname;
            Age = guest.Age;
            ReservationId = reservationId;
            UserType = guest.UserTypeId;
            CheckpointId = guest.CheckpointId;
            Present = guest.IsPresent;
            RequestId = _requestId;
        }
        public TourGuestDTO(string name,  int age, string surname, int userType)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
            this.ReservationId = reservationId;
            UserType = userType;
            this.RequestId = _requestId;
        }

        public TourGuestDTO()
        {

        }
    }
   
    

}
