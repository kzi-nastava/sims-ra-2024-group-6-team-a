using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class AccountSettingsViewModel : INotifyPropertyChanged
    {
        public User LoggedUser { get; set; }
        
        public Guide Guide { get; set; }

        public AccountSettingsPage SettingsWindow { get; set; }

        public string Rank { get; set; }

        public ObservableCollection<string> Languages { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string _selectedLanguage;
        public string SelectedLanguage
        {
            get
            {
                return _selectedLanguage;
            }

            set
            {
                if (value != _selectedLanguage)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged("SelectedLanguage");
                }
            }
        }

        public AccountSettingsViewModel(AccountSettingsPage settingsWindow, User user)
        {
            LoggedUser = user;
            SettingsWindow = settingsWindow;
            Guide = GuideService.GetInstance().GetByUserId(LoggedUser.Id);
            GuideService.GetInstance().UpdateGuideDetails(LoggedUser);
            Rank = Guide.Rank.ToString();
            SelectedLanguage = Guide.WebsiteLanguage;
            Languages =new ObservableCollection<string>() { "English", "Serbian" };

        }

        public void DeleteAccountClick()
        {
            GuideService.GetInstance().DeleteGuide(Guide);
            System.Windows.Application.Current.Shutdown();
        }
        public void GoBackButtonClick() 
        {
            SettingsWindow.NavigationService.GoBack();
        }
        public void ChangeLanguage()
        {
            Guide.WebsiteLanguage = SelectedLanguage;
            GuideService.GetInstance().Update(Guide);
            App app = (App)System.Windows.Application.Current;
            app.ChangeLanguage(Guide.WebsiteLanguage);


        }
    }
}
