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
using System.CodeDom;
using System.Windows.Input;
using BookingApp.View.TouristView;

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
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
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

        public List<TourSchedule> GetOngoingToursByUser(User user)
        {   
            TourReservationRepository reservationRepository = new TourReservationRepository();
            TourScheduleRepository scheduleRepository = new TourScheduleRepository();
            List<TourSchedule> tours = new List<TourSchedule>();

            foreach(TourReservation reservation in reservationRepository.GetAllByUser(user))
            {
                TourSchedule tourSchedule = scheduleRepository.GetById(reservation.TourRealisationId);
                if (tourSchedule.TourActivity == Resources.Enums.TourActivity.Ongoing)
                {
                    tours.Add(tourSchedule);
                }
                
            }
            return tours;
        }
        public List<Tour> GetAllByUser(User user)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(x => x.GuideId == user.Id);
        }

        public Tour GetById(int id)
        {
            return  _tours.Find(c => c.Id == id);
        }

        public List<Tour> GetSameLocationTours(TourScheduleDTO schedule) 
        {
            return GetAll().Where(t => HasSameLocation(t, schedule)).ToList();
        }

        private bool HasSameLocation(Tour tour, TourScheduleDTO schedule)
        {
            LocationRepository locationRepository = new LocationRepository();
            Tour currentTour = GetById(schedule.TourId);
            string currentTourCity = locationRepository.GetById(currentTour.LocationId).City;
            string otherTourCity = locationRepository.GetById(tour.LocationId).City;

            return otherTourCity == currentTourCity && tour.Id != schedule.TourId;
        }

        public List<TourSchedule> GetAllFinishedTours(User user) //svi termini koje sam ja rezervisala, a koji su zavrseni na kojima sam prisustvovala
        {
            TourScheduleRepository scheduleRepository = new TourScheduleRepository();
            TourReservationRepository reservationRepository = new TourReservationRepository();
            TourGuestRepository guestRepository = new TourGuestRepository();

            List<TourSchedule> tours = new List<TourSchedule>();

            foreach(TourSchedule schedule in scheduleRepository.GetAll())
            {
                if (schedule.TourActivity == Resources.Enums.TourActivity.Finished)//imam sve zavrsene termine
                {
                    foreach(TourReservation reservation in reservationRepository.GetAll())
                    {
                        if(reservation.TourRealisationId == schedule.Id && reservation.TouristId == user.Id)//sve moje rezervacije tog termina
                        {
                            foreach(TourGuests guest in guestRepository.GetAllPresentGuestsByReservation(reservation.Id))//gosti na toj rezervaciji
                            {
                                if(guest.UserTypeId == user.Id)
                                {
                                    tours.Add(schedule);
                                }
                            }
                            
                        }
                    }
                    
                }
            }
            return tours;
        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        internal void Subscribe(ActiveTours activeTours)
        {
            throw new NotImplementedException();
        }
    }
}
