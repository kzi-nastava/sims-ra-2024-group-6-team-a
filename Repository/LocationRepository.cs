using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Location Save(Location Location)
        {
            Location.Id = NextId();
            _locations = _serializer.FromCSV(FilePath);
            _locations.Add(Location);
            _serializer.ToCSV(FilePath, _locations);
            return Location;

        }

        public int NextId()
        {
            _locations = _serializer.FromCSV(FilePath);
            if (_locations.Count < 1)
            {
                return 1;
            }
            return _locations.Max(c => c.Id) + 1;
        }

        public void Delete(Location Location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location found = _locations.Find(c => c.Id == Location.Id);
            if (found != null)
            {
                _locations.Remove(found);
            }
            _serializer.ToCSV(FilePath, _locations);
        }

        public Location GetByAccommodation(Accommodation accommodation)
        {
           
            return _locations.Find(c => c.Id == accommodation.LocationId);
        }

    }
}
