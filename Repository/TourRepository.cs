using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Observer;
using BookingApp.DTOs;

namespace BookingApp.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer <Tour> _serializer;
        private readonly List<IObserver> _observers;

        private List <Tour> _tours;
        public Subject subject;


        public TourRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
            subject = new Subject();

        }

        public List<Tour>GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            subject.NotifyObservers();

            return tour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if(_tours.Count < 1 )
            {
                return 1;
            }
            return _tours.Max(x => x.Id) + 1;
        }

        public void Delete(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour found = _tours.Find(x => x.Id == tour.Id);
            _tours.Remove(found);
            _serializer.ToCSV(FilePath,_tours);
            subject.NotifyObservers();
        }

        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(x => x.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(FilePath, _tours);
            subject.NotifyObservers();

            return tour;
        }

        public List<Tour> GetByUser(User user)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(x => x.GuideId == user.Id);
        }

        public Tour GetById(int id)
        {
            return  _tours.Find(c => c.Id == id);
        }

        public List<Tour> GetOtherToursOnSameLocation(TourScheduleDTO schedule) //vraca ostale ture na istoj lokaciji
        {
            TourScheduleRepository tourScheduleRepository = new TourScheduleRepository();
            LocationRepository locationRepository = new LocationRepository();
            List<Tour> tours = new List<Tour>();
            Tour currentTour = GetById(schedule.TourId);
            

            foreach(Tour tour in GetAll())
            {
                if (locationRepository.GetById(tour.LocationId).City == locationRepository.GetById(currentTour.LocationId).City)
                {
                   /* foreach(var scheduleTour in  tourScheduleRepository.GetAll().Where(s => (s.TourId == tour.Id)))
                    {
                        if(scheduleTour.Id == schedule.Id)
                        {
                            tour.TourSchedules.Remove(tourScheduleRepository.GetById(schedule.Id));
                        }
                    }*/
                   if(tour.Id == schedule.TourId)
                    {
                        continue;
                    }
                        
                    tours.Add(tour);
                }
            }
            return tours;
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
