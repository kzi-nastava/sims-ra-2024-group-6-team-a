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
    public class VisitedTourRepository : IVisitedTourRepository
    {
        private const string FilePath = "../../../Resources/Data/visitedTour.csv";
        private readonly Serializer<VisitedTour> _serializer;
        private List<VisitedTour> _tours;
        public Subject subject;

        public VisitedTourRepository()
        {
            _serializer = new Serializer<VisitedTour>();
            _tours = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<VisitedTour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public VisitedTour Save(VisitedTour visitedTour)
        {
            visitedTour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(visitedTour);
            _serializer.ToCSV(FilePath, _tours);

            subject.NotifyObservers();
            return visitedTour;
        }

        public void Delete(VisitedTour visitedTour)
        {
            _tours = _serializer.FromCSV(FilePath);
            VisitedTour found = _tours.Find(x => x.Id == visitedTour.Id);
            _tours.Remove(found);
            _serializer.ToCSV(FilePath, _tours);
            subject.NotifyObservers();
        }




        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(x => x.Id) + 1;
        }
    }
}
