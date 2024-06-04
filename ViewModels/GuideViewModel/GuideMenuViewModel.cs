using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.View;
using BookingApp.ApplicationServices;
using System.Windows.Media;
using System.Net.NetworkInformation;
using BookingApp.Domain.Model;
using System.ComponentModel;
using BookingApp.Resources;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class GuideMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public User LoggedUser { get; set; }

        public Guide Guide { get; set; }

        public Frame mainFrame;

        public ToursPage toursPage;

        public AccountSettingsPage accountSettingsPage;
       
        public TourStatisticsPage tourStatisticsPage;
        public ReviewDetailsPage reviewDetailsPage;
        public TourReviewsPage tourReviewPage;
        public TourRequestsPage tourRequestsPage;

        public bool IsSuperGuide { get; set; }
        public GuideViewMenu MainWindow { get; set; }

        private bool _isTours;
        private bool _isReviews;
        private bool _isStatistics;
        private bool _isRequests;

        public bool IsTours { get => _isTours; set { _isTours = value; OnPropertyChanged("IsTours"); } }
        public bool IsReviews { get => _isReviews; set { _isReviews = value; OnPropertyChanged("IsReviews"); } }
        public bool IsStatistics { get => _isStatistics; set { _isStatistics = value; OnPropertyChanged("IsStatistics"); } }
        public bool IsRequests { get => _isRequests; set { _isRequests = value; OnPropertyChanged("IsRequests"); } }


        public GuideMenuViewModel(GuideViewMenu mainWindow,User user)
        {
            

            
            
            LoggedUser = user;
            MainWindow = mainWindow;
            InitializeGuide();
            tourStatisticsPage = new TourStatisticsPage(LoggedUser);
            tourReviewPage = new TourReviewsPage(mainFrame, LoggedUser);
            toursPage = new ToursPage(LoggedUser);
            tourRequestsPage = new TourRequestsPage(LoggedUser);
            accountSettingsPage = new AccountSettingsPage(LoggedUser);  


            ToursPageClick();
        }
        public void InitializeGuide() 
        {
            GuideService.GetInstance().UpdateGuideDetails(LoggedUser);
            Guide = GuideService.GetInstance().GetByUserId(LoggedUser.Id);
            IsSuperGuide = Guide.Rank == Enums.GuideRank.SuperGuide ? true : false;
            IsSuperGuide = true;
        }

        public void ToursPageClick()
        {
            ResetButtonColors();
            IsTours = true;
            MainWindow.MainFrame.Content = toursPage;
        }
       

        public void TourStatisticsPageClick()
        {
            ResetButtonColors();
            IsStatistics = true;
            MainWindow.MainFrame.Content = tourStatisticsPage;
        }

        public void TourReviewsPageClick()
        {
            ResetButtonColors();
            IsReviews = true;
            MainWindow.MainFrame.Content = tourReviewPage;
        }

        public void TourRequestsPageClick() 
        {

            ResetButtonColors();
            IsRequests = true;
            MainWindow.MainFrame.Content = tourRequestsPage;

        }

        private void ResetButtonColors()
        {
            IsRequests = false;
            IsStatistics = false;
            IsReviews = false;
            IsTours = false;
        }

        public void LiveTourPageClick(int id) 
        { 
            toursPage.SecondFrame.Content = new LiveTour(id);
        }
        public void LogOut_Click()
        {
            MainWindow.Close();
        }
        public void AccountSettings_Click()
        {
            MainWindow.MainFrame.Content = accountSettingsPage;
        }   


    }
}
