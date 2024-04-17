using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class AllToursViewModel
    {
        public static ObservableCollection<TourGuideDTO> AllTours { get; set; }
        public User LoggedUser { get; set; }
        public Frame mainFrame;   
        public AllToursPage Window {  get; set; }
        public AllToursViewModel(AllToursPage window,Frame mainFrame, TourCreationPage tourCreationPage, User user)
        {


            Window = window;
            LoggedUser = user;

            AllTours = new ObservableCollection<TourGuideDTO>();
            this.mainFrame = mainFrame;
            tourCreationPage.SomethingHappened += UpdateWindow;

            Update();
        }

        private void UpdateWindow(object sender, EventArgs e)
        {
            Update();
        }

        public void Update()
        {
            AllTours.Clear();

            foreach (TourSchedule tourSchedule in TourScheduleService.GetInstance().GetAll())
            {
                Tour tour = TourService.GetInstance().GetById(tourSchedule.TourId);
                if (!CheckUpdateConditions(tourSchedule, tour))
                    continue;

                Location location = LocationService.GetInstance().GetById(tour.LocationId);

                DateTime dateTime = tourSchedule.Start;

                Model.Image image = GetFirstTourImage(tour.Id);

                AllTours.Add(new TourGuideDTO(tour, location, image.Path, dateTime, tourSchedule.Id));
            }

        }
        public bool CheckUpdateConditions(TourSchedule tourSchedule, Tour tour)
        {
            if (tourSchedule.TourActivity == Enums.TourActivity.Finished || tour.GuideId != LoggedUser.Id)
            {
                return false;
            }

            return true;
        }
        public Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }

        public void TourCanceledEventHandler()
        {
            Update();
        }
    }
}
