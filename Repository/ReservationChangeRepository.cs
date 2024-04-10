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
    public class ReservationChangeRepository
    {
        private const string FilePath = "../../../Resources/Data/reservation_changes.csv";

        private readonly Serializer<ReservationChanges> _serializer;
        

        private List<ReservationChanges> _changes;
        public Subject subject;

        public ReservationChangeRepository()
        {
            
            _serializer = new Serializer<ReservationChanges>();
            _changes= _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<ReservationChanges> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ReservationChanges Save(ReservationChanges ReservationChanges)
        {
            //ReservationChanges.ReservationId = NextId();          OVO DAJE NOVI ID A TEBI TREBA ID OD RESERVACIJE
            _changes = _serializer.FromCSV(FilePath);
            _changes.Add(ReservationChanges);
            _serializer.ToCSV(FilePath, _changes);
            subject.NotifyObservers();
            return ReservationChanges;

        }

        public int NextId()
        {
            _changes= _serializer.FromCSV(FilePath);
            if (_changes.Count < 1)
            {
                return 1;
            }
            return _changes.Max(c => c.ReservationId) + 1;
        }

        public void Delete(ReservationChanges ReservationChanges)
        {
            _changes= _serializer.FromCSV(FilePath);
            ReservationChanges found = _changes.Find(c => c.ReservationId == ReservationChanges.ReservationId);
            if (found != null)
            {
                _changes.Remove(found);
            }

            _serializer.ToCSV(FilePath, _changes);
            subject.NotifyObservers();
        }
        public List<ReservationChanges> GetAllByGuest(int guestId)
        {
            List<ReservationChanges> reservationsChanges = new List<ReservationChanges>();
            _changes = _serializer.FromCSV(FilePath);
            foreach (ReservationChanges change in _changes)
            {
                //if (change.ReservationId.GuestId == guestId)
                //{
                    reservationsChanges.Add(change);
                //}
            }
            return reservationsChanges;
        }



        public ReservationChanges Update(ReservationChanges ReservationChanges)
        {
            _changes = _serializer.FromCSV(FilePath);
            ReservationChanges current = _changes.Find(c => c.ReservationId == ReservationChanges.ReservationId);
            int index = _changes.IndexOf(current);
            _changes.Remove(current);
            _changes.Insert(index, ReservationChanges);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _changes);
            return ReservationChanges;
        }

        public ReservationChanges Get(int id)
        {
            _changes = _serializer.FromCSV(FilePath);
            return _changes.Find(c => c.ReservationId == id);
        }

    }
}
