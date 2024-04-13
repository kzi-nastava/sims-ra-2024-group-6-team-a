using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView.Components;
namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for LiveToursPage.xaml
    /// </summary>
    public partial class LiveToursPage : Page 
    {
        public static ObservableCollection<TourGuideDTO> TodaysTours { get; set; }
        public User LoggedUser { get; set; }

        public Frame mainFrame;

        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private TourRepository _tourRepository;
        private LiveTour liveTour;


        public LiveToursPage(Frame mainFrame,TourCreationPage tourCreationPage,User user, LocationRepository locationRepository, ImageRepository imageRepository, TourScheduleRepository tourScheduleRepository, TourRepository tourRepository)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = tourRepository;
            _tourScheduleRepository = tourScheduleRepository;

            TodaysTours = new ObservableCollection<TourGuideDTO>();

            this.mainFrame = mainFrame;

            tourCreationPage.SomethingHappened += tourCreationPage_SomethingHappened;
            
            Update();

        }
        
        private void tourCreationPage_SomethingHappened(object sender, EventArgs e)
        {
            Update();
        }

        public void Update()
        {
            TodaysTours.Clear();
            foreach (TourSchedule tourSchedule in _tourScheduleRepository.GetAll())
            {
                Tour tour = _tourRepository.GetById(tourSchedule.TourId);
                if (!CheckUpdateConditions(tourSchedule, tour))
                    continue;
                 
                Location location = _locationRepository.GetById(tour.LocationId);
               
                DateTime dateTime = tourSchedule.Start;
                
                Model.Image image = GetFirstTourImage(tour.Id);
                
                TodaysTours.Add(new TourGuideDTO(tour, location, image.Path, dateTime, tourSchedule.Id));
            }
        }


        
        private bool CheckUpdateConditions(TourSchedule tourSchedule, Tour tour)
        {
            if(tourSchedule.Start.Date != System.DateTime.Now.Date || tourSchedule.TourActivity == Enums.TourActivity.Finished || tour.GuideId != LoggedUser.Id)
            {
                return false;
            }

            return true;
        }

        private Model.Image GetFirstTourImage(int tourId)
        {
            return _imageRepository.GetByEntity(tourId, Enums.ImageType.Tour).First();
        }



        private void TourEndedEventHandler(object sender, EventArgs e)
        {
            Update();
        }
    }
}
