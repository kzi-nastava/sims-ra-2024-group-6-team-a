using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class TouristNotification : ISerializable
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Recieved { get; set; }
        public int UserId { get; set; }
        public Enums.NotificationType Type { get; set; }

        public TouristNotification()
        {
            
        }

        public TouristNotification(string message,int userId, string title, Enums.NotificationType type)
        {
            Message = message; 
            UserId = userId;
            Title = title;  
            Recieved = DateTime.Now;
            Type = type;
        }

        public TouristNotification(TouristNotificationDTO notification)
        {
            Message = notification.Message;
            UserId = notification.UserId;
            Recieved = notification.Recieved;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Message, UserId.ToString(), Title, Recieved.ToString(), Type.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Message = values[1];
            UserId = Convert.ToInt32(values[2]);
            Title = values[3];
            Recieved = DateTime.Parse(values[4]);
            Type = (Enums.NotificationType)Enum.Parse(typeof(Enums.NotificationType), values[5]);
        }
    }
}
