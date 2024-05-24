using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static SimpleRequestDTO SelectedRequest { get; set; } = new SimpleRequestDTO();
        public static User LoggedUser { get; set; } 
        public MyRequestsViewModel(User user)
        {
            LoggedUser = user;
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
        }
    }
}
