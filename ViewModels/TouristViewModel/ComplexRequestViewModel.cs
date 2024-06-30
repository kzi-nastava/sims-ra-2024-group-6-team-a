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
using System.Reflection.Metadata;
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
        public RelayCommand RemoveRequestCommand { get; set; }
        public ComplexRequestViewModel(User user)
        {
            LoggedUser = user;
            MakeComplexRequestCommand = new RelayCommand(Execute_MakeComplexRequestCommand);
            SimpleRequestCommand = new RelayCommand(Execute_SimpleRequestCommand);
            CancelRequestCommand = new RelayCommand(Execute_CancelRequestCommand);
            RemoveRequestCommand = new RelayCommand(Execute_RemoveRequestCommand);
        }

        private void Execute_MakeComplexRequestCommand(object sender)
        {
            TourRequest request = new TourRequest();
            if(SimpleRequests.Count() >= 2)
            {
                ComplexTourRequestService.GetInstance().MakeComplexRequest(ComplexRequestDTO, LoggedUser.Id, SimpleRequests.ToList());
                CustomMessageBox.Show("Request successfully created!");
                CloseAction();
            }
            else
            {
                if (MessageBox.Show("You cannot create a complex request without 2 or more simple requests. Do you wish to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    CloseAction();
            }

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
        private void Execute_RemoveRequestCommand(object parameter)
        {
            var simpleToRemove = parameter as SimpleRequestDTO;
            SimpleRequests.Remove(simpleToRemove);
        }
    }
}
