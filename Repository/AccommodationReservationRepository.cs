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
    public class AccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_reservations.csv";

        private readonly Serializer<AccommodationReservation> _serializer;
        private readonly List<IObserver> _observers;

        private List<AccommodationReservation> _accommodationReservations;
        private List<DateRanges> _availableDates;
        private List<DateRanges> _unavailableDates;
        private List<DateRanges> _candidatesForDeletion;
        private DateOnly _firstDate;
        private DateOnly _lastDate;
        public Subject subject;

        public AccommodationReservationRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            _availableDates = new List<DateRanges>();
            _unavailableDates = new List<DateRanges>();
            _candidatesForDeletion = new List<DateRanges>();
            _firstDate = new DateOnly();
            _lastDate = new DateOnly();
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

        public List<AccommodationReservation> GetByAccommodation(Accommodation accommodation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(c => c.AccommodationId == accommodation.Id);
        }
        public List<DateRanges> GetAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
            _availableDates.Clear();

            _firstDate = firstDate;
            _lastDate = lastDate;

            GetAllDates(daysNumber);
            GetBookedDates(accommodationId);

            bool isFirstBoundaryCase, isInRangeCase, isLastBoundaryCase;
            foreach (DateRanges unavailableDate in _unavailableDates)
            {
                // Date range for search: 15.03-22.03, checkIn = 14.03, checkOut = 17.03
                isFirstBoundaryCase = !IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                // Date range for search: 15.03-22.03, checkIn = 15.03, checkOut = 17.03
                isInRangeCase = IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                // Date range for search: 15.03-22.03, checkIn = 20.03, checkOut = 25.03
                isLastBoundaryCase = IsInRange(unavailableDate.CheckIn, _firstDate, _lastDate) && !IsInRange(unavailableDate.CheckOut, _firstDate, _lastDate);

                if (isFirstBoundaryCase)
                {
                    RemoveStartBorderCase(unavailableDate);
                }
                else if (isInRangeCase)
                {
                    RemoveInRangeCase(unavailableDate);
                }
                else if (isLastBoundaryCase)
                {
                    RemoveEndBorderCase(unavailableDate);
                }
            }
            

            return _availableDates;
        }
        public void RemoveStartBorderCase(DateRanges unavailableDate)
        {
            _availableDates = _availableDates
            .Where(availableDate => unavailableDate.CheckOut <= availableDate.CheckIn)
            .ToList();
        }
        public void RemoveInRangeCase(DateRanges unavailableDate)
        {
            _availableDates = _availableDates
         .Where(availableDate => !(unavailableDate.CheckOut > availableDate.CheckIn && unavailableDate.CheckIn < availableDate.CheckOut))
         .ToList();
        }
        public void RemoveEndBorderCase(DateRanges unavailableDate)
        {
            _availableDates.RemoveAll(availableDate => unavailableDate.CheckIn < availableDate.CheckOut);

        }
        public bool IsInRange(DateOnly date, DateOnly startDate, DateOnly endDate)
        {
            return date >= startDate && date <= endDate;
        }
        public void GetAllDates(int daysNumber)
        {
            DateOnly startDate = _firstDate;
            DateOnly endDate = _firstDate.AddDays(daysNumber);

            while (endDate < _lastDate)
            {
                _availableDates.Add(new DateRanges(startDate, endDate));
                startDate = startDate.AddDays(1);
                endDate = endDate.AddDays(1);
            }
        }
        public void GetBookedDates(int accommodationId)
        {
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                if (reservation.AccommodationId == accommodationId)
                {
                    if (IsUnavailable(reservation.CheckInDate, reservation.CheckOutDate))
                    {
                        _unavailableDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                    }
                }
            }
        }
        public bool IsUnavailable(DateOnly checkIn, DateOnly checkOut)
        {
            if (checkOut<= _firstDate || checkIn>= _lastDate) return false;

            return true;

        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

    }
}
