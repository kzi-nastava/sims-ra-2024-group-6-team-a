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

        }


        //public void LiveTourPage(string parameter) 
        //{
        //    int tourScheduleId = Convert.ToInt32(parameter);
        //    LiveTour liveTourPage = new LiveTour(tourScheduleId);
        //    MainFrame.Content = liveTourPage;
        //}




        private void ShowCreateTourForm(object sender, EventArgs e)
        {
            MainFrame.Content = tourCreationPage;
        }

        private void LiveToursPageClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = liveToursPage;
        }

        


    }
}
