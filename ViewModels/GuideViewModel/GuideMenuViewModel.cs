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

namespace BookingApp.ViewModels.GuideViewModel
{
    public class GuideMenuViewModel
    {
        public User LoggedUser { get; set; }

        public Frame mainFrame;

        public ToursPage toursPage; 

       
        public TourStatisticsPage tourStatisticsPage;
        public ReviewDetailsPage reviewDetailsPage;
        public TourReviewsPage tourReviewPage;

        public GuideViewMenu MainWindow { get; set; }

        public GuideMenuViewModel(GuideViewMenu mainWindow,User user)
        {
            

            
            
            LoggedUser = user;
            MainWindow = mainWindow;

            tourStatisticsPage = new TourStatisticsPage(LoggedUser);
            

            
            tourReviewPage = new TourReviewsPage(mainFrame, LoggedUser);

            toursPage = new ToursPage(LoggedUser);

            ToursPageClick();
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

        private void ResetButtonColors()
        {
            MainWindow.btnTours.Foreground = Brushes.DarkGray;
            MainWindow.btnReviews.Foreground = Brushes.DarkGray;
            MainWindow.btnStatistics.Foreground = Brushes.DarkGray;
        }
    }
}
