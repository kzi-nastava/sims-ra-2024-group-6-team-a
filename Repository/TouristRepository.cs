using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TouristRepository : ITouristRepository
    {
        private const string FilePath = "../../../Resources/Data/tourist.csv";

        private readonly Serializer<Tourist> _serializer;
        private readonly List<IObserver> _observers;

        private List<Tourist> _tourist;
        public Subject subject;

        public TouristRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<Tourist>();
            _tourist = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Tourist> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Tourist Save(Tourist tourist)
        {
            tourist.Id = NextId();
            _tourist = _serializer.FromCSV(FilePath);
            _tourist.Add(tourist);
            _serializer.ToCSV(FilePath, _tourist);
            subject.NotifyObservers();
            return tourist;
        }
        public Tourist GetByTouristId(int id)
        {
            _tourist = _serializer.FromCSV(FilePath);
            return _tourist.Find(c => c.UserId == id);
        }
        public int NextId()
        {
            _tourist = _serializer.FromCSV(FilePath);
            if (_tourist.Count < 1)
            {
                return 1;
            }
            return _tourist.Max(c => c.Id) + 1;
        }

        public void Delete(Tourist tourist)
        {
            _tourist = _serializer.FromCSV(FilePath);
            Tourist founded = _tourist.Find(c => c.Id == tourist.Id);
            _tourist.Remove(founded);
            _serializer.ToCSV(FilePath, _tourist);
            subject.NotifyObservers();
        }

        public Tourist Update(Tourist tourist)
        {
            _tourist = _serializer.FromCSV(FilePath);
            Tourist current = _tourist.Find(c => c.Id == tourist.Id);
            int index = _tourist.IndexOf(current);
            _tourist.Remove(current);
            _tourist.Insert(index, tourist);
            _serializer.ToCSV(FilePath, _tourist);
            subject.NotifyObservers();
            return tourist;
        }

    }
}
