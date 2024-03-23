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
    public class ReservationChangeRepository
    {
        private const string FilePath = "../../../Resources/Data/reservation_changes.csv";

        private readonly Serializer<AccommodationReservation> _serializer;
        

        private List<AccommodationReservation> _changes;
        public Subject subject;

        public ReservationChangeRepository()
        {
            
            _serializer = new Serializer<AccommodationReservation>();
            _changes= _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationReservation Save(AccommodationReservation AccommodationReservation)
        {
            AccommodationReservation.Id = NextId();
            _changes = _serializer.FromCSV(FilePath);
            _changes.Add(AccommodationReservation);
            _serializer.ToCSV(FilePath, _changes);
            subject.NotifyObservers();
            return AccommodationReservation;

        }

        public int NextId()
        {
            _changes= _serializer.FromCSV(FilePath);
            if (_changes.Count < 1)
            {
                return 1;
            }
            return _changes.Max(c => c.Id) + 1;
        }

        public void Delete(AccommodationReservation AccommodationReservation)
        {
            _changes= _serializer.FromCSV(FilePath);
            AccommodationReservation found = _changes.Find(c => c.Id == AccommodationReservation.Id);
            if (found != null)
            {
                _changes.Remove(found);
            }

            _serializer.ToCSV(FilePath, _changes);
            subject.NotifyObservers();
        }

        public AccommodationReservation Get(int id)
        {
            _changes = _serializer.FromCSV(FilePath);
            return _changes.Find(c => c.Id == id);
        }

    }
}
