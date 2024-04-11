using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using BookingApp.Observer;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourStatisticsPage.xaml
    /// </summary>
    public partial class TourStatisticsPage : Page ,IObserver
    {
        public User LoggedUser { get; set; }
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private TourRepository _tourRepository;
        private TourGuestRepository _tourGuestRepository;
      

        public static ObservableCollection<TourStatisticsDTO> FinishedTours {  get; set; }
        public static ObservableCollection<TourStatisticsDTO> MostVisitedTour { get; set; }

        

        public TourStatisticsPage(User user, LocationRepository locationRepository, ImageRepository imageRepository, TourScheduleRepository tourScheduleRepository, TourRepository tourRepository,TourGuestRepository tourGuestRepository)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = tourRepository;
            _tourScheduleRepository = tourScheduleRepository;
            _tourGuestRepository = tourGuestRepository;
            FinishedTours = new ObservableCollection<TourStatisticsDTO>();
            MostVisitedTour = new ObservableCollection<TourStatisticsDTO>();
            Update();
            FindMostVisitedTour();
        }


        private void FindMostVisitedTour()
        {
            if (FinishedTours.Count == 0) return;

            TourStatisticsDTO mostVisitedTour = FinishedTours.First();
            
            foreach (TourStatisticsDTO tour in FinishedTours)
            {
                if (mostVisitedTour.TouristNumber < tour.TouristNumber)
                {
                    mostVisitedTour = tour;
                }
            }
        MostVisitedTour.Add(mostVisitedTour);
        }




        public void Update()
        {
            FinishedTours.Clear();
            foreach(Tour tour in _tourRepository.GetAll())
            {

                Location location = _locationRepository.GetById(tour.LocationId);
                Model.Image image = GetFirstTourImage(tour.Id);
                int touristCount = 0;
                int childrenCount = 0;
                int adultCount = 0;
                int elderlyCount = 0;
                foreach(TourSchedule schedule in _tourScheduleRepository.GetAllByTourId(tour.Id))
                {
                    
                    if (schedule.TourActivity != Enums.TourActivity.Finished) continue;
                    
                    foreach(TourGuests guest in _tourGuestRepository.GetAllByTourId(schedule.Id))
                    {
                        touristCount++;

                        if (guest.Age < 18)
                        {
                            childrenCount++;
                        }
                        else if ( guest.Age >= 18 &&  guest.Age < 50)
                        {
                            adultCount++;   
                        }
                        else
                        {
                            elderlyCount++;
                        }
                    }
                    FinishedTours.Add(new TourStatisticsDTO(tour.Name, tour.Language,image.Path,location, touristCount, childrenCount, adultCount, elderlyCount));
                }
            }
        }
         


        private Model.Image GetFirstTourImage(int tourId)
        {
            return _imageRepository.GetByEntity(tourId, Enums.ImageType.Tour).First();
        }
    }
}
