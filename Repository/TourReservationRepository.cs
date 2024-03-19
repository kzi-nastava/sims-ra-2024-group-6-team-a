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

        public List <TourReservation> GetAllByRealisationId(int tourRealisationId)
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
        public int FindAssignedPeopleNumber(int currentTourRealisationId)
        {
            int peopleNumber = 0;

            foreach (TourReservation tourReservation in GetAll())
            {
                if (tourReservation.TourRealisationId == currentTourRealisationId)
                {
                    peopleNumber += tourReservation.TourGuests.Count;
                }
            }

            return peopleNumber;
        }
        public bool IsFullyBooked(int currentTourRealisationId)
        {
            TourScheduleRepository tourScheduleRepository = new TourScheduleRepository();

            //int peopleNumber = FindAssignedPeopleNumber(currentTourRealisationId);

            int currentGuestNumber = tourScheduleRepository.GetById(currentTourRealisationId).CurrentFreeSpace;
            return currentGuestNumber <= 0;
        }
        private void UpdateCurrentGuestNumber(int tourRealisationId, int guestNumber, TourScheduleRepository tourScheduleRepository)
        {
            foreach (TourSchedule tourSchedule in tourScheduleRepository.GetAll())
            {
                if (tourSchedule.Id == tourRealisationId)
                {
                    tourSchedule.CurrentFreeSpace -= guestNumber;
                    tourScheduleRepository.Update(tourSchedule);
                }
            }
        }
        private void SaveTourGuests(int reservationId, List<TourGuestDTO> guests, TourGuestRepository tourGuestRepository)
        {
            foreach (TourGuestDTO guest in guests)
            {
                TourGuests newGuest = new TourGuests(guest, reservationId);
                tourGuestRepository.Save(newGuest);
            }
        }

        public void MakeReservation(TourScheduleDTO tourScheduleDTO, User loggedUser, List<TourGuestDTO> guests)
        {
            TourGuestRepository tourGuestRepository = new TourGuestRepository();
            TourScheduleRepository tourScheduleRepository = new TourScheduleRepository();

            TourReservation reservation = new TourReservation(guests.Count(), tourScheduleDTO.Id, tourScheduleDTO.TourId, loggedUser.Id);

            UpdateCurrentGuestNumber(tourScheduleDTO.Id, reservation.GuestNumber, tourScheduleRepository);
            Save(reservation);


            SaveTourGuests(reservation.Id, guests, tourGuestRepository);
        }
    }
}
