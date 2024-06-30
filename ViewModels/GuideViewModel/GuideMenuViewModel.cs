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
    public class GuideMenuViewModel
    {
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
            MainWindow.btnTours.Foreground = Brushes.Black;
            MainWindow.MainFrame.Content = toursPage;
        }
       

        public void TourStatisticsPageClick()
        {
            ResetButtonColors();
            MainWindow.btnStatistics.Foreground = Brushes.Black;

            MainWindow.MainFrame.Content = tourStatisticsPage;
        }

        public void TourReviewsPageClick()
        {
            ResetButtonColors();
            MainWindow.btnReviews.Foreground = Brushes.Black;
            MainWindow.MainFrame.Content = tourReviewPage;
        }

        public void TourRequestsPageClick() 
        {

            ResetButtonColors();
            MainWindow.btnRequests.Foreground = Brushes.Black;

            MainWindow.MainFrame.Content = tourRequestsPage;

        }

        private void ResetButtonColors()
        {
            MainWindow.btnRequests.Foreground = Brushes.DarkGray;  
            MainWindow.btnTours.Foreground = Brushes.DarkGray;
            MainWindow.btnReviews.Foreground = Brushes.DarkGray;
            MainWindow.btnStatistics.Foreground = Brushes.DarkGray;
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
