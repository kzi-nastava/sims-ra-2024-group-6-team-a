using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/simpleRequests.csv";

        private readonly Serializer<TourRequest> _serializer;
        private readonly List<IObserver> _observers;

        private List<TourRequest> _request;
        public Subject subject;

        public TourRequestRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<TourRequest>();
            _request = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<TourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequest Save(TourRequest request)
        {
            request.Id = NextId();
            _request = _serializer.FromCSV(FilePath);
            _request.Add(request);
            _serializer.ToCSV(FilePath, _request);
            subject.NotifyObservers();
            return request;
        }
        public int NextId()
        {
            _request = _serializer.FromCSV(FilePath);
            if (_request.Count < 1)
            {
                return 1;
            }
            return _request.Max(c => c.Id) + 1;
        }

        public void Delete(TourRequest request)
        {
            _request = _serializer.FromCSV(FilePath);
            TourRequest founded = _request.Find(c => c.Id == request.Id);
            _request.Remove(founded);
            _serializer.ToCSV(FilePath, _request);
            subject.NotifyObservers();
        }

        public TourRequest Update(TourRequest request)
        {
            _request = _serializer.FromCSV(FilePath);
            TourRequest current = _request.Find(c => c.Id == request.Id);
            int index = _request.IndexOf(current);
            _request.Remove(current);
            _request.Insert(index, request);
            _serializer.ToCSV(FilePath, _request);
            subject.NotifyObservers();
            return request;
        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }
        public TourRequest GetById(int id)
        {

            return _request.Find(c => c.Id == id);
        }
    }
}
