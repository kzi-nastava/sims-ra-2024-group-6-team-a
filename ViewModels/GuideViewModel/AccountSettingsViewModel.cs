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
using System.Windows;
using System.Windows.Input;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class AccountSettingsViewModel : INotifyPropertyChanged
    {
        public User LoggedUser { get; set; }
        
        public Guide LoggedGuide { get; set; }

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
        public bool IsSuperGuide { get; set; }


        public ICommand DeleteAccountCommand { get; }
        public ICommand GoBackCommand { get; }


        public AccountSettingsViewModel(AccountSettingsPage settingsWindow, User user)
        {
            DeleteAccountCommand = new RelayCommand(DeleteAccountClick);
            GoBackCommand = new RelayCommand(GoBackButtonClick);

            
            LoggedUser = user;
            SettingsWindow = settingsWindow;
            LoggedGuide = GuideService.GetInstance().GetByUserId(LoggedUser.Id);
            GuideService.GetInstance().UpdateGuideDetails(LoggedUser);
            SelectedLanguage = LoggedGuide.WebsiteLanguage;
            Languages = new ObservableCollection<string>() { "English", "Serbian" };
            IsSuperGuide = LoggedGuide.Rank == Resources.Enums.GuideRank.SuperGuide ? true : false;          
        }  

      
        private void SetThemeDark()
        {
            ResourceDictionary darkTheme = new ResourceDictionary
            {
                Source = new Uri("../../../View/GuideView/Themes/Dark.xaml", UriKind.Relative)
            };

            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(darkTheme);
        }

        private void SetThemeLight()
        {
            ResourceDictionary darkTheme = new ResourceDictionary
            {
                Source = new Uri("../../../View/GuideView/Themes/Light.xaml", UriKind.Relative)
            };

            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(darkTheme);
        }

        public void LightModeChecked()
        {
            LoggedGuide.Theme = "Light";
            GuideService.GetInstance().Update(LoggedGuide);
            SetThemeLight();

        }

        public void DarkModeChecked()
        {
            LoggedGuide.Theme = "Dark";
            GuideService.GetInstance().Update(LoggedGuide);
            SetThemeDark(); 

        }



        public void DeleteAccountClick(object obj)
        {

            bool IsAccountDeleted = GuideService.GetInstance().DeleteGuide(LoggedGuide);
            if(IsAccountDeleted)
            {
                System.Windows.Application.Current.Shutdown();
            }
           
        }
        public void GoBackButtonClick(object obj) 
        {
            SettingsWindow.NavigationService.GoBack();
        }
        public void ChangeLanguage()
        {
            LoggedGuide.WebsiteLanguage = SelectedLanguage;
            GuideService.GetInstance().Update(LoggedGuide);
            App app = (App)System.Windows.Application.Current;
            app.ChangeLanguage(LoggedGuide.WebsiteLanguage);


        }
    }
}
