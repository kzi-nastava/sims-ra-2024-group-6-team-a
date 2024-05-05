using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class ForumsDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private DateOnly creationTime;
        public DateOnly CreationTime
        {
            get
            {
                return creationTime;
            }
            set
            {
                if (value != creationTime)
                {
                    creationTime = value;
                    OnPropertyChanged("CreationTime");
                }
            }
        }
        public int userId;
        public  int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                if (value != userId)
                {
                    userId = value;
                    OnPropertyChanged("UserId");
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
                    OnPropertyChanged("state");
                }
            }
        }
        public string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != text)
                {
                    text = value;
                    OnPropertyChanged("text");
                }
            }
        }
        public string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged("username");
                }
            }
        }
        public Enums.UserType _userType;
        public Enums.UserType userType
        {
            get { return _userType; }

            set
            {
                if (value != _userType)
                {
                    _userType = value;
                    OnPropertyChanged("userType");
                }
            }
        }
        public ForumsDTO(string username, Location location,Forums forum)
        {
            this.Id = forum.Id;
            this.city = location.City;
            this.state = location.State;
            this.creationTime = forum.CreationTime;
            this.text = forum.Text;
            this.userId = forum.UserId;
            this._userType = forum.UserType;
            this.username = username;
           
        }

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
