using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView;
using BookingApp.View.GuideView.Components;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuideViewMenu.xaml
    /// </summary>
    public partial class GuideViewMenu : Window
    {
        public static ObservableCollection<TourGuideDTO> TodaysTours { get; set; }
        public TourGuideDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }

        public Frame mainFrame;
       
        
        private  TourRepository _tourRepository;
        private  LocationRepository _locationRepository;
        private  ImageRepository _imageRepository;
        private  CheckpointRepository _checkRepository;
        private  TourScheduleRepository _tourScheduleRepository;

        public LiveToursPage liveToursPage;
        public TourCreationPage tourCreationPage;
        public AllToursPage allToursPage;

        public GuideViewMenu(User user,LocationRepository locationRepository,ImageRepository imageRepository)
        {
            InitializeComponent();
            DataContext = this;

            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = new TourRepository();
            _checkRepository = new CheckpointRepository();
            _tourScheduleRepository = new TourScheduleRepository();

            mainFrame = MainFrame;
            LoggedUser = user;

            tourCreationPage = new TourCreationPage(LoggedUser, _tourRepository, _locationRepository, _imageRepository, _checkRepository, _tourScheduleRepository);
            liveToursPage = new LiveToursPage(mainFrame,tourCreationPage, LoggedUser, _locationRepository, _imageRepository, _tourScheduleRepository, _tourRepository);
            allToursPage = new AllToursPage(mainFrame, tourCreationPage, LoggedUser, _locationRepository, _imageRepository, _tourScheduleRepository, _tourRepository);
        }
           
            

        private void ShowCreateTourForm(object sender, EventArgs e)
        {
            MainFrame.Content = tourCreationPage;
        }

        private void LiveToursPageEvent(object sender, EventArgs e)
        {
            liveToursPage.Update();
        }

       
        private void LiveToursPageClick(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = _tourRepository.GetAllByUser(LoggedUser);
            int tourScheduleId = _tourScheduleRepository.FindOngoingTour(tours);

            if (tourScheduleId != 0 )
            {
                LiveTour liveTour = new LiveTour(tourScheduleId);
                MainFrame.Content = liveTour;
                liveTour.TourEndedMainWindow += LiveToursPageEvent;
            }
            else
            {
                MainFrame.Content = liveToursPage;
            }
        }

        private void StatisticsClick(object sender, RoutedEventArgs e)
        {
           
        }



        private void AllToursPageClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = allToursPage;
        }
    }
}
