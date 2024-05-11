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


        public TourRequestsPage Window { get; set; }
        public TourRequestsViewModel(TourRequestsPage window, User user) 
        {
            LoggedUser = user;
            this.Window = window;

            requestsPage = new RequestsPage(LoggedUser);

            RequestsClick();

        }


        public void StatisticsClick()
        {
            ResetButtonColors();
            Window.btnRequestStatistics.Foreground = Brushes.Black;


        }
        public void ComplexTourClick()
        {
            ResetButtonColors();
            Window.btnComplexTours.Foreground = Brushes.Black;


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
