using BookingApp.DTOs;
using BookingApp.Model;
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
        
        public int UserId { get; set; }

        public TouristNotification()
        {
            Message = "Following guests have shown up: ";
        }

        public TouristNotification(int userId, string title)
        {
            Message = "Following guests have shown up: ";
            UserId = userId;
            Title = title;  
        }

        public TouristNotification(TouristNotificationDTO notification)
        {
            Message = notification.Message;
            UserId = notification.UserId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Message, UserId.ToString(), Title};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Message = values[1];
            UserId = Convert.ToInt32(values[2]);
            Title = values[3];
        }
    }
}
