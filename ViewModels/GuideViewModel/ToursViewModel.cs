using BookingApp.ApplicationServices;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.ViewModels.GuideViewModel
{
    

    
    public class ToursViewModel
    {
        public User LoggedUser { get; set; }


        public LiveToursPage liveToursPage;
        public AllToursPage allToursPage;
        public AlreadyStartedTour startedTourPage;
        public TourCreationPage tourCreationPage;


        public ToursPage Window {  get; set; }

        public ToursViewModel(ToursPage window,User user)
        {
            LoggedUser = user;
            this.Window = window;

            tourCreationPage = new TourCreationPage(user);
            liveToursPage = new LiveToursPage(LoggedUser);
            allToursPage = new AllToursPage(LoggedUser);
            LiveToursPageClick();

        }

        public void LiveToursPageClick()
        {
            ResetButtonColors();
            Window.btnTodaysTours.Foreground = Brushes.Black;


            List<Tour> tours = TourService.GetInstance().GetAllByUser(LoggedUser);
            int tourScheduleId = TourScheduleService.GetInstance().FindOngoingTour(tours);

            if (tourScheduleId != 0)
            {
                startedTourPage = new AlreadyStartedTour(tourScheduleId, LoggedUser);
                Window.SecondFrame.Content = startedTourPage;
            }
            else
            {
                Window.SecondFrame.Content = liveToursPage;
            }
        }
        
        public void AllToursPageClick()
        {
            ResetButtonColors();
            Window.btnAllTours.Foreground = Brushes.Black;

            Window.SecondFrame.Content = allToursPage;
        }
        public void ShowCreateTourForm()
        {
            ResetButtonColors();
            Window.btnCreateTour.Foreground = Brushes.Black;

            Window.SecondFrame.Content = tourCreationPage;
        }

        public void LoadTodaysTours()
        {
            LiveToursPageClick();
        }

        private void ResetButtonColors()
        {
            Window.btnTodaysTours.Foreground = Brushes.DarkGray;
            Window.btnAllTours.Foreground = Brushes.DarkGray;
            Window.btnCreateTour.Foreground = Brushes.DarkGray;
        }
    }
}
