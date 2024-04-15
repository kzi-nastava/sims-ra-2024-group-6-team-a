using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        void Delete(Location location);
        List<Location> GetAll();
        Location GetByAccommodation(Accommodation accommodation);
        Location GetById(int id);
        int NextId();
        Location Save(Location location);
    }
}