using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Observer;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository
{
    public class TourGuestRepository : ITourGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/guests.csv";

        private readonly Serializer<TourGuests> _serializer;
        private readonly List<IObserver> _observers;

        private List<TourGuests> _guests;
        public Subject subject;


        public TourGuestRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<TourGuests>();
            _guests = _serializer.FromCSV(FilePath);
            subject = new Subject();

        }

        public List<TourGuests> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourGuests Save(TourGuests guest)
        {
            guest.Id = NextId();
            _guests = _serializer.FromCSV(FilePath);
            _guests.Add(guest);
            _serializer.ToCSV(FilePath, _guests);
            subject.NotifyObservers();

            return guest;
        }

        public int NextId()
        {
            _guests = _serializer.FromCSV(FilePath);
            if (_guests.Count < 1)
            {
                return 1;
            }
            return _guests.Max(x => x.Id) + 1;
        }

        public void Delete(TourGuests guest)
        {
            _guests = _serializer.FromCSV(FilePath);
            TourGuests found = _guests.Find(x => x.Id == guest.Id);
            _guests.Remove(found);
            _serializer.ToCSV(FilePath, _guests);
            subject.NotifyObservers();
        }

        public TourGuests Update(TourGuests guest)
        {
            _guests = _serializer.FromCSV(FilePath);
            TourGuests current = _guests.Find(x => x.Id == guest.Id);
            int index = _guests.IndexOf(current);
            _guests.Remove(current);
            _guests.Insert(index, guest);
            _serializer.ToCSV(FilePath, _guests);
            subject.NotifyObservers();
            return guest;
        }

        public List<TourGuests> GetAllByReservationId(int reservationId)
        {
            return GetAll().Where(g => g.ReservationId == reservationId && g.IsPresent == true).ToList();
        }

     
    }
}

