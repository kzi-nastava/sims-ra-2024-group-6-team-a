using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView;
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
    public partial class GuideViewMenu : Window, IObserver
    {
        public static ObservableCollection<TourGuideDTO> TodaysTours { get; set; }
        public TourGuideDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }
       
        
        private  TourRepository _tourRepository;
        private  LocationRepository _locationRepository;
        private  ImageRepository _imageRepository;
        private  CheckpointRepository _checkRepository;
        private  TourScheduleRepository _tourScheduleRepository;

        public GuideViewMenu(User user,LocationRepository locationRepository,ImageRepository imageRepository)
        {
            InitializeComponent();
            DataContext = this;
           
            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = new TourRepository();
            _checkRepository = new CheckpointRepository();
            _tourScheduleRepository = new TourScheduleRepository();

            LoggedUser = user;
            TodaysTours = new ObservableCollection<TourGuideDTO>();


            Update();
        }
       
        public void Update()
        {
            TodaysTours.Clear();
            foreach(TourSchedule tourSchedule in  _tourScheduleRepository.GetAll())
            {
                if(tourSchedule.Start.Date != System.DateTime.Now.Date)
                {
                    continue;
                }
                Tour tour = _tourRepository.GetById(tourSchedule.TourId);
                if (tour.GuideId != LoggedUser.Id)
                    continue;
                Location location = _locationRepository.GetById(tour.LocationId);

                Model.Image image = new Model.Image();
                DateTime dateTime = tourSchedule.Start;
               
                foreach (Model.Image i in _imageRepository.GetByEntity(tour.Id, Enums.ImageType.Tour))
                {
                    image = i;
                    break;
                }
                TodaysTours.Add(new TourGuideDTO(tour, location, image.Path,dateTime));
            }
        }

         private void ShowCreateTourForm(object sender, EventArgs e)
        {
            TourForm createTourForm = new TourForm(LoggedUser,_tourRepository,_locationRepository,_imageRepository,_checkRepository,_tourScheduleRepository);
            createTourForm.ShowDialog();
            Update();
        }


        private void BeginTourMouseDown(object sender, MouseButtonEventArgs e)
        {
            TourLiveTracking tourLiveTracking = new TourLiveTracking();
            tourLiveTracking.ShowDialog();
            Update();

        }
    }
}
