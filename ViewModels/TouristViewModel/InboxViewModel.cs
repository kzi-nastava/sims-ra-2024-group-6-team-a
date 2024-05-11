using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class InboxViewModel
    {
        public  ObservableCollection<TouristNotificationDTO> Notifications { get; set; }
        public ObservableCollection<TouristNotificationDTO> RequestNotifications { get; set; }
        public User LoggedUser { get; set; }

        public  InboxViewModel(User user) 
        {
            LoggedUser = user;

            Notifications = new ObservableCollection<TouristNotificationDTO>();
            RequestNotifications = new ObservableCollection<TouristNotificationDTO>();


            Update();
        }

        private void Update()
        {
            Notifications.Clear();
            
            foreach(TouristNotification notification in TouristNotificationService.GetInstance().GetAll())
            {
                if (notification.UserId == LoggedUser.Id && notification.Type == Enums.NotificationType.Attendance)
                    Notifications.Add(new TouristNotificationDTO(notification));

            }

            RequestNotifications.Clear();

            foreach (TouristNotification notification in TouristNotificationService.GetInstance().GetAll())
            {
                if (notification.UserId == LoggedUser.Id && notification.Type == Enums.NotificationType.AcceptedRequest)
                    RequestNotifications.Add(new TouristNotificationDTO(notification));

            }
        }

    }
}
