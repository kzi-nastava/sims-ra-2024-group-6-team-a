using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.Observer;

namespace BookingApp.Repository
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;
        public Subject subject;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations = _serializer.FromCSV(FilePath);
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);

            subject.NotifyObservers();
            return location;
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

        public void Delete(Location location)
        {
            _locations = _serializer.FromCSV(FilePath);
            Location found = _locations.Find(c => c.Id == location.Id);
            if (found != null)
            {
                _locations.Remove(found);
            }
            _serializer.ToCSV(FilePath, _locations);
            subject.NotifyObservers();
        }


        public Location GetById(int id) 
        {
           
            return _locations.Find(c => c.Id == id);
        }

        public Location GetByAccommodation(Accommodation accommodation)
        {
            return _locations.Find(c => c.Id == accommodation.LocationId);
        }
    }
}
