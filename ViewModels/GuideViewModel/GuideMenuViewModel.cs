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

namespace BookingApp.ViewModels.GuideViewModel
{
    public class GuideMenuViewModel
    {
        public static ObservableCollection<TourGuideDTO> TodaysTours { get; set; }
        public TourGuideDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }

        public Frame mainFrame;


        private TourRepository _tourRepository;
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private CheckpointRepository _checkRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private TourGuestRepository _tourGuestRepository;
        private TourReviewRepository _tourReviewRepository;

        public LiveToursPage liveToursPage;
        public TourCreationPage tourCreationPage;
        public TourStatisticsPage tourStatisticsPage;
        public AllToursPage allToursPage;
        public ReviewDetailsPage reviewDetailsPage;
        public TourReviewsPage tourReviewPage;

        public GuideViewMenu MainWindow { get; set; }

        public GuideMenuViewModel(GuideViewMenu mainWindow,User user, LocationRepository locationRepository, ImageRepository imageRepository)
        {
            

            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = new TourRepository();
            _checkRepository = new CheckpointRepository();
            _tourScheduleRepository = new TourScheduleRepository();
            _tourGuestRepository = new TourGuestRepository();
            _tourReviewRepository = new TourReviewRepository();
            
            LoggedUser = user;
            MainWindow = mainWindow;

            tourStatisticsPage = new TourStatisticsPage(LoggedUser, _locationRepository, _imageRepository, _tourScheduleRepository, _tourRepository, _tourGuestRepository);
            tourCreationPage = new TourCreationPage(LoggedUser, _tourRepository, _locationRepository, _imageRepository, _checkRepository, _tourScheduleRepository);
            liveToursPage = new LiveToursPage(mainFrame, tourStatisticsPage, tourCreationPage, LoggedUser, _locationRepository, _imageRepository, _tourScheduleRepository, _tourRepository);
            liveToursPage.tourEnded += UpdateWindows;
            
            allToursPage = new AllToursPage(mainFrame, tourCreationPage, LoggedUser, _locationRepository, _imageRepository, _tourScheduleRepository, _tourRepository);
            tourReviewPage = new TourReviewsPage(mainFrame, tourCreationPage, LoggedUser, _locationRepository, _imageRepository, _tourScheduleRepository, _tourRepository, _tourReviewRepository);
            
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
            List<Tour> tours = _tourRepository.GetAllByUser(LoggedUser);
            int tourScheduleId = _tourScheduleRepository.FindOngoingTour(tours);

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
