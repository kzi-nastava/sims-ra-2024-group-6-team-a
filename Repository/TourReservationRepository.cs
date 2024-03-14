using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class TourReservationRepository
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
        //funkcija koja vraca broj ljudi koji su rezervisali tu odredjenu turu, koristi se kasnije
        public int FindAssignedPeopleNumber(int tourId, int tourDateTimeId)
        {
            int peopleNumber = 0;

            TourRepository tourRepository = new TourRepository();

            foreach (TourReservation tourReservation in GetAll())
            {
                if (tourId == tourReservation.TourId && tourDateTimeId == tourReservation.ReservedTourTime)
                {
                    peopleNumber += tourReservation.TourGuests.Count;
                }
            }

            return peopleNumber;
        }
        //pravim bool funkciju koja za izabranu turu provjerava da li je POTPUNO bukirana
        //ako je tura potpuno popunjena sistem ce ponuditi druge ture na istoj lokaciji

        bool IsFullyBooked(int tourId, int tourDateTime)
        {
            TourRepository tourRepository = new TourRepository();
            int peopleNumber = FindAssignedPeopleNumber(tourId, tourDateTime);

            if (tourRepository.GetById(tourId).Capacity == peopleNumber)
                return true;
            return false;
        }

        //vraca broj preostalih mjesta onda kada korisniku treba ispisati koliko mjesta je ostalo
        public int GetAvailableGuestNumber(int tourId, int tourDateTime)
        {
            int bookedPlaces = FindAssignedPeopleNumber(tourId, tourDateTime);
            TourRepository tourRepository = new TourRepository();
            if (!IsFullyBooked(tourId, tourDateTime))
            {
                int leftPlaces = tourRepository.GetById(tourId).Capacity - bookedPlaces;
                return leftPlaces;
            }
            return 0;

        }

        public void MakeReservation(TourScheduleDTO tourScheduleDTO, TourReservationDTO tourReservationDTO, TourTouristDTO tourTouristDTO, User LoggedUser, List<TourGuestDTO> guests)
        {
            TourGuestRepository tourGuestRepository = new TourGuestRepository();
            TourReservation reservation = new TourReservation();

            Tour tour = new Tour();


            reservation.GuestNumber = tourReservationDTO.GuestNumber;
            reservation.ReservedTourTime = tourScheduleDTO.Id;
            reservation.TourId = tourTouristDTO.Id;
            reservation.TouristId = LoggedUser.Id;

            Save(reservation);

            foreach (TourGuestDTO guest in guests)
            {
                TourGuests newGuest = new TourGuests();
                newGuest.Name = guest.Name;
                newGuest.Surname = guest.Surname;
                newGuest.Age = guest.Age;
                newGuest.ReservationId = reservation.Id;
                tourGuestRepository.Save(newGuest);
            }
        }


    }
}
