using BookingApp.ApplicationServices;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace BookingApp.ViewModels.GuideViewModel
{
    

    
    public class ToursViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private bool isTodays;
        private bool isAll;
        private bool isCreate;

        public bool IsTodays { get => isTodays; set { isTodays = value; OnPropertyChanged("IsTodays"); } }
        public bool IsAll { get => isAll; set { isAll = value; OnPropertyChanged("IsAll"); } }
        public bool IsCreate { get => isCreate; set { isCreate = value; OnPropertyChanged("IsCreate"); } }

        public User LoggedUser { get; set; }


        public LiveToursPage liveToursPage;
        public AllToursPage allToursPage;
        public AlreadyStartedTour startedTourPage;
        public TourCreationPage tourCreationPage;


        public ToursPage Window {  get; set; }


        public ICommand LiveToursPageCommand { get; }
        public ICommand AllToursPageCommand { get; }
        public ICommand ShowCreateTourFormCommand { get; }

        public ToursViewModel(ToursPage window,User user)
        {
            LiveToursPageCommand = new RelayCommand(LiveTourPage);
            AllToursPageCommand = new RelayCommand(AllToursPageClick);
            ShowCreateTourFormCommand = new RelayCommand(ShowCreateTourForm);

            LoggedUser = user;
            this.Window = window;

            tourCreationPage = new TourCreationPage(user);
            liveToursPage = new LiveToursPage(LoggedUser);
            allToursPage = new AllToursPage(LoggedUser);
            LiveToursPageClick();

        }

        public void LiveTourPage(object obj) 
        {
            LiveToursPageClick();
        }

        public void LiveToursPageClick()
        {
            ResetButtonColors();
            IsTodays = true;

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
        
        public void AllToursPageClick(object obj)
        {
            ResetButtonColors();
            IsAll = true;

            Window.SecondFrame.Content = allToursPage;
        }
        public void ShowCreateTourForm(object obj)
        {
            ResetButtonColors();
            IsCreate = true;    

            Window.SecondFrame.Content = tourCreationPage;
        }

        public void LoadTodaysTours()
        {
            LiveToursPageClick();
        }

        private void ResetButtonColors()
        {
            IsTodays = false;
            IsAll = false;
            IsCreate = false;
        }
    }
}
