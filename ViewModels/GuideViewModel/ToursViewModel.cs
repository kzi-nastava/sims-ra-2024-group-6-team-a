using BookingApp.ApplicationServices;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.GuideViewModel
{
    

    
    public class ToursViewModel
    {
        public User LoggedUser { get; set; }


        public LiveToursPage liveToursPage;
        public AllToursPage allToursPage;
        public AlreadyStartedTour startedTourPage;
        public TourCreationPage tourCreationPage;


        public ToursPage window;

        public ToursViewModel(ToursPage window,User user)
        {
            LoggedUser = user;
            this.window = window;

            tourCreationPage = new TourCreationPage(user);
            liveToursPage = new LiveToursPage(LoggedUser);
            allToursPage = new AllToursPage(LoggedUser);
            LiveToursPageClick();

        }

        public void LiveToursPageClick()
        {
            List<Tour> tours = TourService.GetInstance().GetAllByUser(LoggedUser);
            int tourScheduleId = TourScheduleService.GetInstance().FindOngoingTour(tours);

            if (tourScheduleId != 0)
            {
                startedTourPage = new AlreadyStartedTour(tourScheduleId, LoggedUser);
                window.SecondFrame.Content = startedTourPage;
            }
            else
            {
                window.SecondFrame.Content = liveToursPage;
            }
        }
        
        public void AllToursPageClick()
        {
            window.SecondFrame.Content = allToursPage;
        }
        public void ShowCreateTourForm()
        {
            window.SecondFrame.Content = tourCreationPage;
        }

        public void LoadTodaysTours()
        {
            LiveToursPageClick();
        }
    }
}
