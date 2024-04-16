using BookingApp.Domain.Model;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Xceed.Wpf.AvalonDock.Controls;

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

        public TouristNotificationDTO(TouristNotificationDTO notification)
        {
            Id = notification.Id;
            Message = notification.Message;
            UserId = notification.UserId;
        }
        public TouristNotificationDTO(TouristNotification notification)
        {
            Title = notification.Title;
            Id = notification.Id;
            Message = notification.Message;
            UserId = notification.UserId;
        }
    }
}
