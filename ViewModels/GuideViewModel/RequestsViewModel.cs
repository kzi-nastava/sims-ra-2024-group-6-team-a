using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using BookingApp.View.GuideView.Pages;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class RequestsViewModel
    {
        public User LoggedUser { get; set; }
        public static ObservableCollection<SimpleRequestDTO> SimpleRequests { get; set; }

        public ObservableCollection<LanguageDTO> Languages { get; set; } = new ObservableCollection<LanguageDTO>();
        public ObservableCollection<LocationDTO> Locations { get; set; } = new ObservableCollection<LocationDTO>();

        public RequestFilterDTO Filter {  get; set; }

        public RequestsPage Window { get; set; }

        public RequestsViewModel(RequestsPage window, User user)
        {
            LoggedUser = user;
            Window = window;
            SimpleRequests = new ObservableCollection<SimpleRequestDTO>();
            Filter= new RequestFilterDTO();
            UpdateRequests();
            SetLanguages();
            SetLocations();
        }
        private void SetLanguages()
        {
            Languages.Clear();
            foreach (var language in LanguageService.GetInstance().GetAll())
                Languages.Add(new LanguageDTO(language));
        }

        private void SetLocations()
        {
            Locations.Clear();
            foreach (var location in LocationService.GetInstance().GetAll())
                Locations.Add(new LocationDTO(location));
        }

        public void UpdateRequests()
        {
            SimpleRequests.Clear();
            foreach (TourRequest request in TourRequestService.GetInstance().GetFiltered(Filter))
            {
                if (request.Status != Enums.RequestStatus.Onhold)
                    continue;

                Location location = LocationService.GetInstance().GetById(request.LocationId);
                Language language = LanguageService.GetInstance().GetById(request.LanguageId);
                int touristNumber = TourGuestService.GetInstance().CountGuestsInRequest(request.Id);
                SimpleRequests.Add(new SimpleRequestDTO(request, location, language, touristNumber, LoggedUser.Id));
            }
        }
        public void languageComboBox_SelectionChanged()
        {
            if (Window.languageComboBox.SelectedItem != null)
            {
                Filter.Language = (LanguageDTO)Window.languageComboBox.SelectedItem;
                return;
            }
            Filter.Language = new LanguageDTO();
        }
        public void locationComboBox_SelectionChanged()
        {
            if (Window.locationComboBox.SelectedItem != null)
            {
                Filter.Location = (LocationDTO)Window.locationComboBox.SelectedItem;
                return;
            }
            Filter.Location = new LocationDTO();
        }

        public void Search_Click()
        {
            UpdateRequests();
        }
        public void DatePickerBeginning_SelectedDateChanged()
        {
            if (Window.startDateComboBox.SelectedDate != null)
            {
                Filter.Beggining = DateOnly.FromDateTime(Window.startDateComboBox.SelectedDate.Value);
                return;
            }
            Filter.Beggining = DateOnly.MinValue;
        }
        public void DatePickerEnding_SelectedDateChanged()
        {
            if (Window.endDateComboBox.SelectedDate != null)
            {
                Filter.Ending = DateOnly.FromDateTime(Window.endDateComboBox.SelectedDate.Value);
                return;
            }
            Filter.Ending = DateOnly.MinValue;
        }
    }
}
