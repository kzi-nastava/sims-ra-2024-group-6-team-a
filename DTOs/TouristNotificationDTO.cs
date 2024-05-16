using BookingApp.Domain.Model;
using System;
using System.ComponentModel;

namespace BookingApp.DTOs
{
    public class TouristNotificationDTO : INotifyPropertyChanged
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

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (value != _message)
                {
                    _message = value;
                    OnPropertyChanged("Message");

                }
            }
        }
        private DateTime _recieved;
        public DateTime Recieved
        {
            get
            {
                return _recieved;
            }
            set
            {
                if(value != _recieved)
                {
                    _recieved = value;
                    OnPropertyChanged("Recieved");
                }
            }
        }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");

                }
            }
        }
        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (value != _userId)
                {
                    _userId = value;
                    OnPropertyChanged("UserId");

                }
            }
        }
        public TouristNotificationDTO(TouristNotification notification)
        {
            Title = notification.Title;
            Id = notification.Id;
            Message = notification.Message;
            UserId = notification.UserId;
            Recieved = notification.Recieved;
        }
    }
}
