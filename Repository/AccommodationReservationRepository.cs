using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Resources;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static BookingApp.Resources.Enums;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_reservations.csv";
        private readonly Serializer<AccommodationReservation> _serializer;
        private readonly List<IObserver> _observers;
        private List<AccommodationReservation> _accommodationReservations;
        public Subject subject;
        public AccommodationReservationRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }
        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public AccommodationReservation Save(AccommodationReservation AccommodationReservation)
        {
            AccommodationReservation.Id = NextId();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            _accommodationReservations.Add(AccommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            subject.NotifyObservers();
            return AccommodationReservation;
        }
        public int NextId()
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            if (_accommodationReservations.Count < 1)
            {
                return 1;
            }
            return _accommodationReservations.Max(c => c.Id) + 1;
        }
        public void Delete(AccommodationReservation AccommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation found = _accommodationReservations.Find(c => c.Id == AccommodationReservation.Id);
            if (found != null)
            {
                _accommodationReservations.Remove(found);
            }
            _serializer.ToCSV(FilePath, _accommodationReservations);
            subject.NotifyObservers();
        }
        public AccommodationReservation Update(AccommodationReservation AccommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation current = _accommodationReservations.Find(c => c.Id == AccommodationReservation.Id);
            int index = _accommodationReservations.IndexOf(current);
            _accommodationReservations.Remove(current);
            _accommodationReservations.Insert(index, AccommodationReservation);       
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return AccommodationReservation;
        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }
    }
}
