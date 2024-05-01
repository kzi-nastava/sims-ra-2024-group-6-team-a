using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
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
    /// Interaction logic for AlreadyStartedTour.xaml
    /// </summary>
    public partial class AlreadyStartedTour : Page
    {


        public static ObservableCollection<TourGuideDTO> TodaysTours { get; set; }

        public static ObservableCollection<TourGuideDTO> SelectedTour {  get; set; }
        public User LoggedUser { get; set; }




        private LiveTour liveTour;
        private TourStatisticsPage _tourStatisticsPage;

        public TourGuideDTO StartedTour {  get; set; }

        public event EventHandler tourEnded;

        public int TourScheduleId {  get; set; }


        public AlreadyStartedTour(int tourScheduleId, TourStatisticsPage tourStatisticsPage, TourCreationPage tourCreationPage, User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;
            TourScheduleId = tourScheduleId;
            _tourStatisticsPage = tourStatisticsPage;

            TodaysTours = new ObservableCollection<TourGuideDTO>();
            SelectedTour = new ObservableCollection<TourGuideDTO>();  

            tourCreationPage.SomethingHappened += tourCreationPage_SomethingHappened;

            UpdateStartedTour();
            Update();
        }


        private void tourCreationPage_SomethingHappened(object sender, EventArgs e)
        {
            Update();
        }

        private void UpdateStartedTour()
        {
            TourSchedule tourSchedule = TourScheduleService.GetInstance().GetById(TourScheduleId);
            Tour tour = TourService.GetInstance().GetById(tourSchedule.TourId);
            Location location = LocationService.GetInstance().GetById(tour.LocationId);
            DateTime dateTime = tourSchedule.Start;
            Model.Image image = GetFirstTourImage(tour.Id);
            Language language = LanguageService.GetInstance().GetById(tour.LanguageId);
            SelectedTour.Add(new TourGuideDTO(tour, language, location, image.Path, dateTime, tourSchedule.Id));
        }


        public void Update()
        {
            TodaysTours.Clear();
            foreach (TourSchedule tourSchedule in TourScheduleService.GetInstance().GetAll())
            {
                Tour tour = TourService.GetInstance().GetById(tourSchedule.TourId);
                if (!CheckUpdateConditions(tourSchedule, tour))
                    continue;

                Location location = LocationService.GetInstance().GetById(tour.LocationId);

                DateTime dateTime = tourSchedule.Start;

                Model.Image image = GetFirstTourImage(tour.Id);

                Language language = LanguageService.GetInstance().GetById(tour.LanguageId);

                TodaysTours.Add(new TourGuideDTO(tour, language, location, image.Path, dateTime, tourSchedule.Id, true));
            }
        }



        private bool CheckUpdateConditions(TourSchedule tourSchedule, Tour tour)
        {
            if (tourSchedule.Start.Date != System.DateTime.Now.Date || tourSchedule.TourActivity == Enums.TourActivity.Finished || tour.GuideId != LoggedUser.Id)
            {
                return false;
            }

            return true;
        }

        private Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }



        private void TourEndedEventHandler(object sender, EventArgs e)
        {
            Update();
            tourEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
