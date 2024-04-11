using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
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

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page
    {
        public static ObservableCollection<TourGuideDTO> AllTours { get; set; }
        public User LoggedUser { get; set; }

        public Frame mainFrame;

        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private TourRepository _tourRepository;

        public AllToursPage(Frame mainFrame, TourCreationPage tourCreationPage, User user, LocationRepository locationRepository, ImageRepository imageRepository, TourScheduleRepository tourScheduleRepository, TourRepository tourRepository)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = tourRepository;
            _tourScheduleRepository = tourScheduleRepository;

            AllTours = new ObservableCollection<TourGuideDTO>();
            this.mainFrame = mainFrame;


            Update();
        }

        public void Update()
        {
            AllTours.Clear();

            foreach (TourSchedule tourSchedule in _tourScheduleRepository.GetAll())
            {
                Tour tour = _tourRepository.GetById(tourSchedule.TourId);
                if (!CheckUpdateConditions(tourSchedule, tour))
                    continue;

                Location location = _locationRepository.GetById(tour.LocationId);

                DateTime dateTime = tourSchedule.Start;

                Model.Image image = GetFirstTourImage(tour.Id);

                AllTours.Add(new TourGuideDTO(tour, location, image.Path, dateTime, tourSchedule.Id));
            }

        }
        private bool CheckUpdateConditions(TourSchedule tourSchedule, Tour tour)
        {
            if (tourSchedule.TourActivity == Enums.TourActivity.Finished || tour.GuideId != LoggedUser.Id)
            {
                return false;
            }

            return true;
        }
        private Model.Image GetFirstTourImage(int tourId)
        {
            return _imageRepository.GetByEntity(tourId, Enums.ImageType.Tour).First();
        }

        private void TourCanceledEventHandler(object sender, EventArgs e)
        {
            Update();
        }
    }
}
