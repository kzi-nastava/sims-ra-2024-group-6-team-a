using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class ComplexRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<SimpleRequestDTO> SimpleRequests { get; set; } = new ObservableCollection<SimpleRequestDTO>();
        public ObservableCollection<TourGuestDTO> Guests { get; set; } = new ObservableCollection<TourGuestDTO>();
        public User LoggedUser { get; set; }
        public Action CloseAction { get; set; }
        public ComplexRequestDTO ComplexRequestDTO { get; set; } = new ComplexRequestDTO();
        public RelayCommand MakeComplexRequestCommand { get; set; }
        public RelayCommand CancelRequestCommand { get; set; }
        public RelayCommand SimpleRequestCommand { get; set; }
        public ComplexRequestViewModel(User user)
        {
            LoggedUser = user;
            MakeComplexRequestCommand = new RelayCommand(Execute_MakeComplexRequestCommand);
            SimpleRequestCommand = new RelayCommand(Execute_SimpleRequestCommand);
            CancelRequestCommand = new RelayCommand(Execute_CancelRequestCommand);
        }

        private void Execute_MakeComplexRequestCommand(object sender)
        {
            TourRequest request = new TourRequest();

           /* foreach (SimpleRequestDTO simplRequest in SimpleRequests)
            {
                if (simplRequest != null)
                {
                    //simplRequest.ComplexRequestId = ComplexRequestDTO.Id;
                    TourRequestService.GetInstance().MakeTourRequest(simplRequest, simplRequest.Guests.ToList(), LoggedUser);
                }
            }*/

            ComplexTourRequestService.GetInstance().MakeComplexRequest(ComplexRequestDTO, LoggedUser.Id, SimpleRequests.ToList());
            CloseAction();
        }
        private void Execute_CancelRequestCommand(object sender)
        {
            CloseAction();
        }
        private void Execute_SimpleRequestCommand(object sender)
        {
            ComplexRequestComponent component = new ComplexRequestComponent(LoggedUser, this);
            component.Owner = Application.Current.MainWindow;
            component.ShowDialog();
            
        }
    }
}
