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
    public class GuestRepository: IGuestsRepository
    {
         private const string FilePath = "../../../Resources/Data/accommodation_guests.csv";

        private readonly Serializer<Guest> _serializer;
        private readonly List<IObserver> _observers;

        private List<Guest> _guests;
        public Subject subject;

        public GuestRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<Guest>();
            _guests = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Guest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Guest Save(Guest guest)
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
            return _guests.Max(c => c.Id) + 1;
        }

        public void Delete(Guest guest)
        {
            _guests = _serializer.FromCSV(FilePath);
            Guest founded = _guests.Find(c => c.Id == guest.Id);
            _guests.Remove(founded);
            _serializer.ToCSV(FilePath, _guests);
            subject.NotifyObservers();
        }

        public Guest Update(Guest guest)
        {
            _guests = _serializer.FromCSV(FilePath);
            Guest current = _guests.Find(c => c.Id == guest.Id);
            int index = _guests.IndexOf(current);
            _guests.Remove(current);
            _guests.Insert(index, guest);
            _serializer.ToCSV(FilePath, _guests);
            subject.NotifyObservers();
            return guest;
        }

        public string GetFullname(int id)
        {
            _guests = _serializer.FromCSV(FilePath);
            Guest guest = _guests.Find(u => u.Id == id);
            return guest.Name + " " + guest.Surname;
        }
    }
}
