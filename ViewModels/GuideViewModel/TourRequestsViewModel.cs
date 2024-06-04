using BookingApp.Domain.Model;
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
    public class TourRequestsViewModel
    {
        public User LoggedUser { get; set; }


        public RequestsPage requestsPage;
        public RequestedTourStatistics statistics;
        public ComplexTourRequestView complexTourRequest;

        public TourRequestsPage Window { get; set; }
        public TourRequestsViewModel(TourRequestsPage window, User user) 
        {
            LoggedUser = user;
            this.Window = window;

            requestsPage = new RequestsPage(LoggedUser);
            statistics = new RequestedTourStatistics(LoggedUser);
            complexTourRequest = new ComplexTourRequestView(LoggedUser);


            RequestsClick();

        }


        public void StatisticsClick()
        {
            ResetButtonColors();
            Window.btnRequestStatistics.Foreground = Brushes.Black;
            Window.SecondFrame.Content = statistics;


        }
        public void ComplexTourClick()
        {
            ResetButtonColors();
            Window.btnComplexTours.Foreground = Brushes.Black;
            Window.SecondFrame.Content = complexTourRequest;


        }

        public void RequestsClick()
        {
            ResetButtonColors();
            Window.btnRequests.Foreground = Brushes.Black;

            Window.SecondFrame.Content = requestsPage;

        }

        private void ResetButtonColors()
        {
            Window.btnComplexTours.Foreground = Brushes.DarkGray;
            Window.btnRequests.Foreground = Brushes.DarkGray;
            Window.btnRequestStatistics.Foreground = Brushes.DarkGray;
        }

    }
}
