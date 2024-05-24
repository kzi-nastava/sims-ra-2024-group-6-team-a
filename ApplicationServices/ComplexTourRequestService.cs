using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.TouristView;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class ComplexTourRequestService
    {
        private IComplexTourRequestRepository _complexRequestRepository;

        public ComplexTourRequestService(IComplexTourRequestRepository complexRequestRepository)
        {
            _complexRequestRepository = complexRequestRepository;
        }

        public ComplexTourRequestService()
        {
            _complexRequestRepository = new ComplexTourRequestRepository();

        }
        public static ComplexTourRequestService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ComplexTourRequestService>();
        }
        public List<ComplexTourRequest> GetAll()
        {
            return _complexRequestRepository.GetAll();
        }

        public ComplexTourRequest Update (ComplexTourRequest request)
        {
            return _complexRequestRepository.Update(request);
        }
        public ComplexTourRequest Save(ComplexTourRequest request)
        {
            return _complexRequestRepository.Save(request);
        }
        private void SaveSimpleRequests(int complexRequestId, List<SimpleRequestDTO> requests, int touristId)
        {
            User user = UserService.GetInstance().GetById(touristId);
            foreach(SimpleRequestDTO request in requests)
            {
                request.ComplexRequestId = complexRequestId;
                TourRequestService.GetInstance().MakeTourRequest(request, request.Guests.ToList(), user);
            }
        }
        public void MakeComplexRequest(ComplexRequestDTO request, int touristId, List<SimpleRequestDTO> simpleRequests)
        {
            ComplexTourRequest complexRequest = new ComplexTourRequest(request.Status, touristId);
            Save(complexRequest);
            SaveSimpleRequests(complexRequest.Id, simpleRequests, touristId);
        }

        public void CheckRequestStatus()
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly tresholdDate = currentDate.AddDays(2);

            foreach (ComplexTourRequest request in GetAll())
            {
                List<TourRequest> simpleRequests = TourRequestService.GetInstance().GetAllByComplexRequest(request.Id);

                bool allWithinThreshold = simpleRequests.All(simpleRequest => simpleRequest.StartDate < tresholdDate);
                bool allAccepted = simpleRequests.All(simpleRequest => simpleRequest.Status == Enums.RequestStatus.Accepted);

                if (allWithinThreshold)
                {
                    request.Status = Enums.RequestStatus.Invalid;
                    Update(request);
                }
                else if (allAccepted)
                {
                    request.Status = Enums.RequestStatus.Accepted;
                    Update(request);
                }
            }
        }
    }
}
