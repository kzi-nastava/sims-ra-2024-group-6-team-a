using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
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
        public static  ObservableCollection<TouristNotificationDTO> Notifications { get; set; }
        public User LoggedUser { get; set; }

        public  InboxViewModel(Inbox window,  User user) 
        {
            LoggedUser = user;

            Notifications = new ObservableCollection<TouristNotificationDTO>();


            Update();
        }

        private void Update()
        {
            Notifications.Clear();
            
            foreach(TouristNotification notification in TouristNotificationService.GetInstance().GetAll())
            {
                if (notification.UserId == LoggedUser.Id)
                    Notifications.Add(new TouristNotificationDTO(notification));

            }
        }

    }
}
