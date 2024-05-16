using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Repository;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;
using BookingApp.Domain.Model;

namespace BookingApp.ApplicationServices
{
    public class LocationService
    {
        private ILocationRepository _locationRepository;


        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public LocationService() 
        {
            _locationRepository = new LocationRepository();
        }

        public static LocationService GetInstance()
        {
        return App.ServiceProvider.GetRequiredService<LocationService>();  
        }

        public List<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location Save(Location location)
        {
            return _locationRepository.Save(location);
        }
        public Location GetByCityAndState(string ComboBoxLocation)
        {
            Location location = new Location();
            string[] parts = ComboBoxLocation.Split(", ");
            location = _locationRepository.GetByCityAndState(parts[0],parts[1]);
            return location;
        }

        public int NextId()
        {
           return _locationRepository.NextId();
        }

        public void Delete(Location location)
        {
            _locationRepository.Delete(location);
        }


        public Location GetById(int id)
        {

            return _locationRepository.GetById(id);  
        }

        public Location GetByAccommodation(Accommodation accommodation)
        {
            return _locationRepository.GetByAccommodation(accommodation);
        }
        public List<Location> GetAllByUserRequest(int userId)
        {
            List<Location> list = new List<Location>();
            foreach (TourRequest request in TourRequestService.GetInstance().GetByTouristId(userId))
            {
                list.Add(GetById(request.LocationId));
            }
            return list;
        }

    }

}
