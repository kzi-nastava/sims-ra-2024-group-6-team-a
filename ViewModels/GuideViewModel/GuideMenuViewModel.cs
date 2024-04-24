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

            toursPage = new ToursPage(LoggedUser,tourStatisticsPage,tourReviewPage);

            MainWindow.MainFrame.Content = toursPage;

        }

        public void ToursPageClick()
        {
            MainWindow.MainFrame.Content = toursPage;
        }
       

        public void TourStatisticsPageClick()
        {
            MainWindow.MainFrame.Content = tourStatisticsPage;
        }

        public void TourReviewsPageClick()
        {
            MainWindow.MainFrame.Content = tourReviewPage;
        }

        public void TourRequestsPageClick()
        {

        }
    }
}
