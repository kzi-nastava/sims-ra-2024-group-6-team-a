using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class ComplexRequestDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<SimpleRequestDTO> SimpleRequests { get; set; } = new ObservableCollection<SimpleRequestDTO>();
        public ComplexRequestDTO ComplexRequest { get; set; }

        public int Number { get; set; } = 0;
        public ComplexRequestDetailsViewModel(ComplexRequestDTO complexRequest)
        {
            ComplexRequest = complexRequest;
            Update();
        }

        private void Update()
        {
            foreach(TourRequest simpleRequest in TourRequestService.GetInstance().GetAllByComplexRequest(ComplexRequest.Id))
            {
                SimpleRequestDTO component = new SimpleRequestDTO(simpleRequest, LocationService.GetInstance().GetById(simpleRequest.LocationId), LanguageService.GetInstance().GetById(simpleRequest.LanguageId));
                component.Number++;
                SimpleRequests.Add(component);
            }
        }
    }
}
