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
        public TourCreationPage tourCreationPage;
        public AllToursPage allToursPage;
        public TourStatisticsPage tourStatisticsPage;
        public TourReviewsPage tourReviewsPage;
        public AlreadyStartedTour startedTourPage;


        public ToursPage window;

        public ToursViewModel(ToursPage window,User user)
        {
            LoggedUser = user;
            this.window = window;



            

            tourCreationPage = new TourCreationPage(LoggedUser);
            liveToursPage = new LiveToursPage(tourStatisticsPage, tourCreationPage, LoggedUser);
            allToursPage = new AllToursPage(tourCreationPage, LoggedUser);
            liveToursPage.tourEnded += UpdateWindows;
            LiveToursPageClick();

        }

        public void UpdateWindows(object sender, EventArgs e)
        {
            tourReviewsPage.Update();
            tourStatisticsPage.Update();
        }

        public void LiveToursPageClick()
        {
            List<Tour> tours = TourService.GetInstance().GetAllByUser(LoggedUser);
            int tourScheduleId = TourScheduleService.GetInstance().FindOngoingTour(tours);

            if (tourScheduleId != 0)
            {
                startedTourPage = new AlreadyStartedTour(tourScheduleId, tourStatisticsPage, tourCreationPage, LoggedUser);
                window.SecondFrame.Content = startedTourPage;
                //liveTour.TourEndedMainWindow += LiveToursPageEvent;
            }
            else
            {
                window.SecondFrame.Content = liveToursPage;
            }
        }
        public void LiveToursPageEvent(object sender, EventArgs e)
        {
            liveToursPage.Update();
            tourStatisticsPage.Update();
            tourReviewsPage.Update();
        }

        public void AllToursPageClick()
        {
            window.SecondFrame.Content = allToursPage;
        }
        public void ShowCreateTourForm()
        {
            window.SecondFrame.Content = tourCreationPage;
        }

    }
}
