using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class LiveToursPageViewModel
    {
        public static ObservableCollection<TourGuideDTO> TodaysTours { get; set; }
        public User LoggedUser { get; set; }
        private LiveTour liveTour;

        public LiveToursPageViewModel(User user)
        {
            LoggedUser = user;
            TodaysTours = new ObservableCollection<TourGuideDTO>();
            Update();
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

                TodaysTours.Add(new TourGuideDTO(tour, language, location, image.Path, dateTime, tourSchedule.Id, false));
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
        public void LoadTodaysTours()
        {
            Update();
        }

    }
}
