using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class AccountSettingsViewModel
    {
        public User LoggedUser { get; set; }
        
        public Guide Guide { get; set; }

        public AccountSettingsPage SettingsWindow { get; set; }

        public string Rank { get; set; }

        public AccountSettingsViewModel(AccountSettingsPage settingsWindow, User user)
        {
            LoggedUser = user;
            SettingsWindow = settingsWindow;
            Guide = GuideService.GetInstance().GetByUserId(LoggedUser.Id);
            GuideService.GetInstance().UpdateGuideDetails(LoggedUser);
            Rank = Guide.Rank.ToString();

        }
    }
}
