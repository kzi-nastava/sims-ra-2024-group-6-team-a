using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.TouristView;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class SimpleRequestService
    {
        private ISimpleRequestRepository _simpleRequestRepository;

        public SimpleRequestService(ISimpleRequestRepository simpleRequestRepository)
        {
            _simpleRequestRepository = simpleRequestRepository;
        }

        public SimpleRequestService()
        {
            _simpleRequestRepository = new SimpleRequestRepository();

        }

        public static SimpleRequestService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<SimpleRequestService>();
        }

        public List<TourRequest> GetAll()
        {
            return _simpleRequestRepository.GetAll();
        }
        public TourRequest Save(TourRequest request)
        {
            return _simpleRequestRepository.Save(request);
        }
        private void SaveRequestGuests(int requestId, List<TourGuestDTO> guests, User loggedUser)
        {
            foreach (TourGuestDTO guest in guests)
            {
                TourGuests newGuest = new TourGuests(guest,-1, requestId);
                TourGuestService.GetInstance().Save(newGuest);
            }
        }

        public void MakeTourRequest(SimpleRequestDTO request, List<TourGuestDTO> guests, User user)
        {
            TourRequest simpleRequest = new TourRequest(request.locationId, request.languageId, request.Description, request.Start, request.End, request.TouristId, request.Status);
            Save(simpleRequest);
            SaveRequestGuests(simpleRequest.Id, guests, user);
        }

        public void CheckRequestStatus()
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly tresholdDate = currentDate.AddDays(2);

            foreach (TourRequest request in GetAll())
            {
                if (request.StartDate < tresholdDate)
                {
                    request.Status = Resources.Enums.RequestStatus.Invalid;
                    _simpleRequestRepository.Update(request);
                }
            }
            
        }
        public TourRequest GetById(int id)
        {
            return _simpleRequestRepository.GetById(id);
        }
    }
}
