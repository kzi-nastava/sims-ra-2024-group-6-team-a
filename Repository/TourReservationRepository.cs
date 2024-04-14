using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class TourReservationRepository : ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _tourReservations;

        public Subject subject;



        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = _serializer.FromCSV(FilePath);
            subject = new Subject();

        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourReservation Save(TourReservation reservation)
        {
            reservation.Id = NextId();
            _tourReservations = _serializer.FromCSV(FilePath);
            _tourReservations.Add(reservation);
            _serializer.ToCSV(FilePath, _tourReservations);

            subject.NotifyObservers();
            return reservation;
        }

        public int NextId()
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(x => x.Id) + 1;
        }

        public void Delete(TourReservation reservation)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            TourReservation found = _tourReservations.Find(x => x.Id == reservation.Id);
            _tourReservations.Remove(found);
            _serializer.ToCSV(FilePath, _tourReservations);
            subject.NotifyObservers();
        }

        public List<TourReservation> GetAllByRealisationId(int tourRealisationId)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            return _tourReservations.Where(t => t.TourRealisationId == tourRealisationId).ToList();
        }

        public TourReservation Update(TourReservation reservation)
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            TourReservation current = _tourReservations.Find(x => x.Id == reservation.Id);
            int index = _tourReservations.IndexOf(current);
            _tourReservations.Remove(current);
            _tourReservations.Insert(index, reservation);
            _serializer.ToCSV(FilePath, _tourReservations);
            subject.NotifyObservers();
            return reservation;
        }

        public List<TourReservation> GetAllByUser(User user)  //pronasla sam sve rezervacije nekog turiste
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            return _tourReservations.FindAll(x => x.TouristId == user.Id);
  
        }


        public TourReservation GetById(int id)
        {
            return _tourReservations.Find(c => c.Id == id);

        }

        public TourReservation GetBySchedule(int schdeuleId)
        {
            return _tourReservations.Find(c => c.TourRealisationId == schdeuleId);
        }

    }
}