using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.GuideView.Pages;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Resources;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class ComplexTourRequestViewModel
    {

        public User LoggedUser { get; set; }
        public ComplexTourRequestView Window { get; set; }

        public static ObservableCollection<ComplexRequestShowDTO> ComplexRequests { get; set; }

        public static ObservableCollection<SimpleRequestDTO> TourFragments { get; set; }

        public ComplexTourRequestViewModel(ComplexTourRequestView window, User user) 
        {
            this.Window = window;
            LoggedUser = user;
            ComplexRequests = new ObservableCollection<ComplexRequestShowDTO>();
            TourFragments = new ObservableCollection<SimpleRequestDTO>();
            UpdateComplexRequests();
        }

        public void UpdateComplexRequests() 
        {
            ComplexRequests.Clear();
           foreach(ComplexTourRequest complexRequest in ComplexTourRequestService.GetInstance().GetAll())
           {
                User user = UserService.GetInstance().GetById(complexRequest.TouristId);    
                Tourist tourist = TouristService.GetInstance().GetByTouristId(user.Id);
                int numberOfFragments = TourRequestService.GetInstance().GetAllByComplexRequest(complexRequest.Id).Count;
                ComplexRequests.Add(new ComplexRequestShowDTO(complexRequest.Id, tourist.Name, tourist.Surname, numberOfFragments));
           }
        }

        public void UpdateTourFragments(int id) 
        {
            TourFragments.Clear();
            
            foreach (TourRequest request in TourRequestService.GetInstance().GetAllByComplexRequest(id))
            {
                if (request.Status != Enums.RequestStatus.Onhold)
                    continue;

                Location location = LocationService.GetInstance().GetById(request.LocationId);
                Language language = LanguageService.GetInstance().GetById(request.LanguageId);
                int touristNumber = TourGuestService.GetInstance().GetGuestsCountByRequest(request.Id);
                TourFragments.Add(new SimpleRequestDTO(request, location, language, touristNumber, LoggedUser.Id));
            }
        }

        public void HandleDetailsClick(object sender, int id)
        {
            UpdateTourFragments(id);        
        }

    }
}
