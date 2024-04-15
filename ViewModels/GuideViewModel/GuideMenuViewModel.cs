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
        public static ObservableCollection<TourGuideDTO> TodaysTours { get; set; }
        public TourGuideDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }

        public Frame mainFrame;

        public LiveToursPage liveToursPage;
        public TourCreationPage tourCreationPage;
        public TourStatisticsPage tourStatisticsPage;
        public AllToursPage allToursPage;
        public ReviewDetailsPage reviewDetailsPage;
        public TourReviewsPage tourReviewPage;

        public GuideViewMenu MainWindow { get; set; }

        public GuideMenuViewModel(GuideViewMenu mainWindow,User user)
        {
            

            
            
            LoggedUser = user;
            MainWindow = mainWindow;

            tourStatisticsPage = new TourStatisticsPage(LoggedUser);
            tourCreationPage = new TourCreationPage(LoggedUser);
            liveToursPage = new LiveToursPage(mainFrame, tourStatisticsPage, tourCreationPage, LoggedUser);
            liveToursPage.tourEnded += UpdateWindows;
            
            allToursPage = new AllToursPage(mainFrame, tourCreationPage, LoggedUser);
            tourReviewPage = new TourReviewsPage(mainFrame, tourCreationPage, LoggedUser);
            
        }

        public void UpdateWindows(object sender, EventArgs e)
        {
            tourReviewPage.Update();
            tourStatisticsPage.Update();
        }

   
        public void ShowCreateTourForm()
        {
            MainWindow.MainFrame.Content = tourCreationPage;
        }

        public void LiveToursPageEvent(object sender, EventArgs e)
        {
            liveToursPage.Update();
            tourStatisticsPage.Update();
            tourReviewPage.Update();
        }

        public void TourStatisticsPageClick()
        {
            MainWindow.MainFrame.Content = tourStatisticsPage;
        }

        public void TourReviewsPageClick()
        {
            MainWindow.MainFrame.Content = tourReviewPage;
        }


        public void LiveToursPageClick()
        {
            List<Tour> tours = TourService.GetInstance().GetAllByUser(LoggedUser);
            int tourScheduleId = TourScheduleService.GetInstance().FindOngoingTour(tours);

            if (tourScheduleId != 0)
            {
                LiveTour liveTour = new LiveTour(tourScheduleId);
                MainWindow.MainFrame.Content = liveTour;
                liveTour.TourEndedMainWindow += LiveToursPageEvent;
            }
            else
            {
                MainWindow.MainFrame.Content = liveToursPage;
            }
        }

        public void AllToursPageClick()
        {
            MainWindow.MainFrame.Content = allToursPage;
        }
    }
}
