using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_reservations.csv";

        private readonly Serializer<AccommodationReservation> _serializer;
        private readonly List<IObserver> _observers;

        private List<AccommodationReservation> _accommodationReservations;
        private List<DateRanges> _availableDates;
        private List<DateRanges> _bookedDates;
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
            _bookedDates = new List<DateRanges>();
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
            if (lastDate < firstDate.AddDays(daysNumber))
            {
                MessageBox.Show("EROR 404", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return _availableDates;

            }
            _firstDate = firstDate;
            _lastDate = lastDate;

            GetAllDates(daysNumber);
            GetBookedDates(accommodationId);

            bool isFirstBoundaryCase, isInRangeCase, isLastBoundaryCase;
            foreach (DateRanges bookedDate in _bookedDates)
            {
                isFirstBoundaryCase = !IsInRange(bookedDate.CheckIn, _firstDate, _lastDate) && IsInRange(bookedDate.CheckOut, _firstDate, _lastDate);
                isInRangeCase = IsInRange(bookedDate.CheckIn, _firstDate, _lastDate) && IsInRange(bookedDate.CheckOut, _firstDate, _lastDate);
                isLastBoundaryCase = IsInRange(bookedDate.CheckIn, _firstDate, _lastDate) && !IsInRange(bookedDate.CheckOut, _firstDate, _lastDate);

                if (isFirstBoundaryCase)
                {
                    RemoveStartBorderCase(bookedDate);
                }
                else if (isInRangeCase)
                {
                    RemoveInRangeCase(bookedDate);
                }
                else if (isLastBoundaryCase)
                {
                    RemoveEndBorderCase(bookedDate);
                }
            }
            if (_availableDates.Count == 0)
            {
                MessageBox.Show("There are no available dates in the given period, but here are 3 suggested dates!", "Suggested dates", MessageBoxButton.OK);
                FindSuggestedDates(daysNumber);
            }
            return _availableDates;
        }
        public void RemoveStartBorderCase(DateRanges bookedDate)
        {
            _availableDates = _availableDates
            .Where(availableDate => bookedDate.CheckOut <= availableDate.CheckIn)
            .ToList();
        }
        public void RemoveInRangeCase(DateRanges bookedDate)
        {
            _availableDates = _availableDates
         .Where(availableDate => !(bookedDate.CheckOut > availableDate.CheckIn && bookedDate.CheckIn < availableDate.CheckOut))
         .ToList();
        }
        public void RemoveEndBorderCase(DateRanges bookedDate)
        {
            _availableDates.RemoveAll(availableDate => bookedDate.CheckIn < availableDate.CheckOut);

        }
        public bool IsInRange(DateOnly date, DateOnly startDate, DateOnly endDate)
        {
            return date >= startDate && date <= endDate;
        }
        public void GetAllDates(int daysNumber)
        {
            DateOnly startDate = _firstDate;
            DateOnly endDate = _firstDate.AddDays(daysNumber);

            while (endDate <= _lastDate)
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
                    if (IsBooked(reservation.CheckInDate, reservation.CheckOutDate))
                    {
                        _bookedDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                    }
                }
            }
        }
        public bool IsBooked(DateOnly checkIn, DateOnly checkOut)
        {
            if (checkOut<= _firstDate || checkIn>= _lastDate) return false;

            return true;

        }

        public void FindSuggestedDates(int daysNumber)
        {
            DateOnly startDateBefore = _firstDate.AddDays(-1);
            DateOnly endDateBefore = startDateBefore.AddDays(daysNumber);

            DateOnly endDateAfter = _lastDate.AddDays(1);
            DateOnly startDateAfter = endDateAfter.AddDays(-daysNumber);

            while (_availableDates.Count != 3)
            {
                if (IsSuggestedDateAvailable(startDateBefore, endDateBefore))
                {
                    _availableDates.Add(new DateRanges(startDateBefore, endDateBefore));
                }
                startDateBefore = startDateBefore.AddDays(-1);
                endDateBefore = endDateBefore.AddDays(-1);

                if (IsSuggestedDateAvailable(startDateAfter, endDateAfter))
                {
                    _availableDates.Add(new DateRanges(startDateAfter, endDateAfter));
                }
                startDateAfter = startDateAfter.AddDays(1);
                endDateAfter = endDateAfter.AddDays(1);
            }
        }

        public bool IsSuggestedDateAvailable(DateOnly checkIn, DateOnly checkOut)
        {
            bool checkInIsInRange, checkOutIsInRange, containsBookedDates;
            foreach (DateRanges bookedDate in _bookedDates)
            {
                checkInIsInRange = bookedDate.CheckIn >= checkIn && bookedDate.CheckIn < checkOut;
                checkOutIsInRange = bookedDate.CheckOut > checkIn && bookedDate.CheckOut <= checkOut;
                containsBookedDates = checkIn > bookedDate.CheckIn && checkOut < bookedDate.CheckOut;
                if (checkInIsInRange || checkOutIsInRange || containsBookedDates)   return false;
            }
            return true;
        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

    }
}
