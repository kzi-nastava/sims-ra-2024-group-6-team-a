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
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complexRequests.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;
        private readonly List<IObserver> _observers;

        private List<ComplexTourRequest> _request;
        public Subject subject;

        public ComplexTourRequestRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<ComplexTourRequest>();
            _request = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ComplexTourRequest Save(ComplexTourRequest request)
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

        public void Delete(ComplexTourRequest request)
        {
            _request = _serializer.FromCSV(FilePath);
            ComplexTourRequest founded = _request.Find(c => c.Id == request.Id);
            _request.Remove(founded);
            _serializer.ToCSV(FilePath, _request);
            subject.NotifyObservers();
        }

        public ComplexTourRequest Update(ComplexTourRequest request)
        {
            _request = _serializer.FromCSV(FilePath);
            ComplexTourRequest current = _request.Find(c => c.Id == request.Id);
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
        public ComplexTourRequest GetById(int id)
        {

            return _request.Find(c => c.Id == id);
        }
    }
}
