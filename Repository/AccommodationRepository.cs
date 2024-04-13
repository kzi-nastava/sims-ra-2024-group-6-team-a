using BookingApp.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;
        private readonly List<IObserver> _observers;

        private List<Accommodation> _accommodations;
        public Subject subject;

        public AccommodationRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Accommodation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            subject.NotifyObservers();
            return accommodation;

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

        public void Delete(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation found = _accommodations.Find(c => c.Id == accommodation.Id);
            if (found != null)
            {
                _accommodations.Remove(found);
            }

            _serializer.ToCSV(FilePath, _accommodations);
            subject.NotifyObservers();
        }

        public List<Accommodation> GetByOwnerId(int id)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FindAll(c => c.OwnerId == id);
        }

        public Accommodation GetByReservationId(int id)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.Find(c => c.Id == id);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
