using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class TourRequestsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public User LoggedUser { get; set; }


        public RequestsPage requestsPage;
        public RequestedTourStatistics statistics;
        public ComplexTourRequestView complexTourRequest;

        private bool _isStatisticsSelected;
        private bool _isComplexToursSelected;
        private bool _isRequestsSelected;

        public bool IsStatisticsSelected
        {
            get => _isStatisticsSelected;
            set
            {
                _isStatisticsSelected = value;
                OnPropertyChanged("IsStatisticsSelected");
            }
        }

        public bool IsComplexToursSelected
        {
            get => _isComplexToursSelected;
            set
            {
                _isComplexToursSelected = value;
                OnPropertyChanged("IsComplexToursSelected");
            }
        }

        public bool IsRequestsSelected
        {
            get => _isRequestsSelected;
            set
            {
                _isRequestsSelected = value;
                OnPropertyChanged("IsRequestsSelected");
            }
        }



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
            IsStatisticsSelected = true;
            Window.SecondFrame.Content = statistics;


        }
        public void ComplexTourClick()
        {
            ResetButtonColors();
            IsComplexToursSelected = true;
            Window.SecondFrame.Content = complexTourRequest;


        }

        public void RequestsClick()
        {
            ResetButtonColors();
            IsRequestsSelected = true;
            Window.SecondFrame.Content = requestsPage;

        }

        private void ResetButtonColors()
        {
            IsStatisticsSelected = false;
            IsComplexToursSelected = false;
            IsRequestsSelected = false;
        }

    }
}
