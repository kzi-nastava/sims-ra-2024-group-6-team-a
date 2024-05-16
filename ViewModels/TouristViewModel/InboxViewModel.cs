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
        public ObservableCollection<TouristNotificationDTO> StatisticTourNotifications { get; set; }
        public User LoggedUser { get; set; }

        public  InboxViewModel(User user) 
        {
            LoggedUser = user;

            Notifications = new ObservableCollection<TouristNotificationDTO>();
            RequestNotifications = new ObservableCollection<TouristNotificationDTO>();
            StatisticTourNotifications = new ObservableCollection<TouristNotificationDTO>();

            Update();
        }

        private void Update()
        {
            Notifications.Clear();
            StatisticTourNotifications.Clear();
            RequestNotifications.Clear();

            foreach (TouristNotification notification in TouristNotificationService.GetInstance().GetAllByUser(LoggedUser.Id))
            {
                switch (notification.Type)
                {
                    case Enums.NotificationType.Attendance:
                        Notifications.Add(new TouristNotificationDTO(notification));
                        break;
                    case Enums.NotificationType.AcceptedRequest:
                        RequestNotifications.Add(new TouristNotificationDTO(notification));
                        break;
                    case Enums.NotificationType.NewTour:
                        StatisticTourNotifications.Add(new TouristNotificationDTO(notification));
                        break;                    
                }
                

            }


        }

    }
}
