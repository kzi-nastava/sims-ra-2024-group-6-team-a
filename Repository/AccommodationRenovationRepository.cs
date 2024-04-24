using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationRenovationRepository : IAccommodationRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_renovations.csv";

        private readonly Serializer<AccommodationRenovation> _serializer;
        private readonly List<IObserver> _observers;

        private List<AccommodationRenovation> _accommodations;
        public Subject subject;

        public AccommodationRenovationRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<AccommodationRenovation>();
            _accommodations = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationRenovation Save(AccommodationRenovation AccommodationRenovation)
        {
            AccommodationRenovation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(AccommodationRenovation);
            _serializer.ToCSV(FilePath, _accommodations);
            subject.NotifyObservers();
            return AccommodationRenovation;

        }

        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(c => c.Id) + 1;
        }

        public void Delete(AccommodationRenovation AccommodationRenovation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            AccommodationRenovation found = _accommodations.Find(c => c.Id == AccommodationRenovation.Id);
            if (found != null)
            {
                _accommodations.Remove(found);
            }

            _serializer.ToCSV(FilePath, _accommodations);
            subject.NotifyObservers();
        }


        public AccommodationRenovation GetByAccommodation(int id)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.Find(c => c.AccommodationId == id);
        }

    }
}
