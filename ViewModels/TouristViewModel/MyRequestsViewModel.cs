using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class MyRequestsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public static ObservableCollection<SimpleRequestDTO> SimpleRequests { get; set;} = new ObservableCollection<SimpleRequestDTO>();
        public ObservableCollection<ComplexRequestDTO> ComplexRequests { get; set; } = new ObservableCollection<ComplexRequestDTO>();
        public static SimpleRequestDTO SelectedRequest { get; set; } = new SimpleRequestDTO();
        public static User LoggedUser { get; set; } 
        public RelayCommand DetailedViewCommand { get; set; }
        private int number { get; set; } = 0;
        public MyRequestsViewModel(User user)
        {
            LoggedUser = user;
            DetailedViewCommand = new RelayCommand(Execute_DetailedViewCommand);
            Update();
        }

        private void Update()
        {
            SimpleRequests.Clear();
            foreach(TourRequest request in TourRequestService.GetInstance().GetAll())
            {
                if (request.TouristId == LoggedUser.Id && request.ComplexRequestId == -1)
                    SimpleRequests.Add(new SimpleRequestDTO(request, LocationService.GetInstance().GetById(TourRequestService.GetInstance().GetById(request.Id).LocationId), LanguageService.GetInstance().GetById(TourRequestService.GetInstance().GetById(request.Id).LanguageId)));
            }

            ComplexRequests.Clear();
            foreach(ComplexTourRequest request in ComplexTourRequestService.GetInstance().GetAll())
            {
                if(request.TouristId == LoggedUser.Id)
                {
                    number++;
                    ComplexRequests.Add(new ComplexRequestDTO(request, number));
                    
                }
            }
        }

        private void Execute_DetailedViewCommand(object parameter)
        {
            ComplexRequestDetailed detailed = new ComplexRequestDetailed((ComplexRequestDTO)parameter);
            detailed.Owner = Application.Current.MainWindow;
            detailed.ShowDialog();
        }
    }
}
