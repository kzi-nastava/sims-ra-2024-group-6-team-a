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
    public class ForumViewDTO : INotifyPropertyChanged
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
        public int UserId
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
        public Enums.ReservationChangeStatus reservationChangeStatus;

        public Enums.ReservationChangeStatus ReservationChangeStatus
        {
            get { return reservationChangeStatus; }

            set
            {
                if (value != reservationChangeStatus)
                {
                    reservationChangeStatus = value;
                    OnPropertyChanged("ReservationChangeStatus");
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
                    OnPropertyChanged("_userType");
                }
            }
        }
        public ForumViewDTO(string username, ForumsComment forumComment, Enums.ReservationChangeStatus reservationChange)
        {
            this.Id = forumComment.ForumId;
            this.creationTime = forumComment.CreationTime;
            this.text = forumComment.Text;
            this.userId = forumComment.UserId;
            this.username = username;
            this._userType = forumComment.UserType;
            this.ReservationChangeStatus = reservationChange;


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
