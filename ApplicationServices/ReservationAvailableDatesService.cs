using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ApplicationServices
{
    public class ReservationAvailableDatesService
    {
        public IAccommodationReservationRepository accommodationReservationRepository;
        private List<DateRanges> _availableDates;
        private List<DateRanges> _bookedDates;
        private DateOnly _firstDate;
        private DateOnly _lastDate;

        public ReservationAvailableDatesService(IAccommodationReservationRepository _accommodationReservationRepository)
        {
            accommodationReservationRepository = _accommodationReservationRepository;
            _availableDates = new List<DateRanges>();
            _bookedDates = new List<DateRanges>();
            _firstDate = new DateOnly();
            _lastDate = new DateOnly();
        }

        public static ReservationAvailableDatesService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ReservationAvailableDatesService>();
        }
        public List<AccommodationReservation> GetAll()
        {
            return accommodationReservationRepository.GetAll();
        }
        public AccommodationReservation Save(AccommodationReservation reservation)
        {
            return accommodationReservationRepository.Save(reservation);
        }
        public List<DateRanges> GetAvailableDates(DateOnly firstDate, DateOnly lastDate, int daysNumber, int accommodationId)
        {
            _availableDates.Clear();
            _firstDate = firstDate;
            _lastDate = lastDate;
            GetAllDates(daysNumber);
            GetBookedDates(accommodationId);
            foreach (DateRanges bookedDate in _bookedDates)
            {
                FindRangeCase(bookedDate);
            }
            IsAvailableDatesEmpty(daysNumber);
            return _availableDates;
        }
        public void IsAvailableDatesEmpty(int daysNumber)
        {
            if (_availableDates.Count == 0)
            {
                MessageBox.Show("There are no available dates in the given period, but here are few suggested dates!", "Suggested dates", MessageBoxButton.OK, MessageBoxImage.Information);
                FindSuggestedDates(daysNumber);
            }
        }
        public void FindSuggestedDates(int daysNumber)
        {
            DateOnly startDateBefore = _firstDate.AddDays(-1);
            DateOnly endDateBefore = startDateBefore.AddDays(daysNumber);
            DateOnly endDateAfter = _lastDate.AddDays(1);
            DateOnly startDateAfter = endDateAfter.AddDays(-daysNumber);
            while (_availableDates.Count < 3)
            {
                AddSuggestedDate(startDateBefore, endDateBefore);
                startDateBefore = startDateBefore.AddDays(-1);
                endDateBefore = endDateBefore.AddDays(-1);
                AddSuggestedDate(startDateAfter, endDateAfter);
                startDateAfter = startDateAfter.AddDays(1);
                endDateAfter = endDateAfter.AddDays(1);
            }
        }
        public void FindRangeCase(DateRanges bookedDate)
        {
            if (IsStartBorderCase(bookedDate, _firstDate, _lastDate))
                RemoveStartBorderCase(bookedDate);
            if (IsEndBorderCase(bookedDate, _firstDate, _lastDate))
                RemoveEndBorderCase(bookedDate);
            if (IsInRangeCase(bookedDate, _firstDate, _lastDate))
                RemoveInRangeCase(bookedDate);
            if (IsSubsetRangeCase(bookedDate, _firstDate, _lastDate))
                RemoveInSubsetCase(bookedDate);
        }
        public void RemoveStartBorderCase(DateRanges bookedDate)
        {
            _availableDates = _availableDates.Where(availableDate => bookedDate.CheckOut <= availableDate.CheckIn).ToList();
        }
        public void RemoveInRangeCase(DateRanges bookedDate)
        {
            _availableDates.RemoveAll(availableDate => (bookedDate.CheckIn < availableDate.CheckOut && bookedDate.CheckOut > availableDate.CheckIn));

        }
        public void RemoveInSubsetCase(DateRanges bookedDate)
        {
            _availableDates.Clear();
        }

        public void RemoveEndBorderCase(DateRanges bookedDate)
        {
            _availableDates.RemoveAll(availableDate => bookedDate.CheckIn < availableDate.CheckOut);
        }
        public bool IsInRangeCase(DateRanges bookedDateRange, DateOnly startDate, DateOnly endDate)
        {
            return bookedDateRange.CheckIn > startDate && bookedDateRange.CheckOut < endDate;
        }
        public bool IsEndBorderCase(DateRanges bookedDateRange, DateOnly startDate, DateOnly endDate)
        {
            return bookedDateRange.CheckIn < endDate && bookedDateRange.CheckOut > endDate && bookedDateRange.CheckIn > startDate;
        }
        public bool IsStartBorderCase(DateRanges bookedDateRange, DateOnly startDate, DateOnly endDate)
        {
            return bookedDateRange.CheckOut > startDate && bookedDateRange.CheckIn < startDate && bookedDateRange.CheckOut <= endDate;
        }
        public bool IsSubsetRangeCase(DateRanges bookedDateRange, DateOnly startDate, DateOnly endDate)
        {
            return bookedDateRange.CheckIn <= startDate && bookedDateRange.CheckOut >= endDate;
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
            foreach (AccommodationReservation reservation in GetAll())
            {
                if (reservation.AccommodationId == accommodationId)
                {
                    if (IsBooked(reservation.CheckInDate, reservation.CheckOutDate))
                        _bookedDates.Add(new DateRanges(reservation.CheckInDate, reservation.CheckOutDate));
                }
            }
        }
        public bool IsBooked(DateOnly checkIn, DateOnly checkOut)
        {
            if (checkOut <= _firstDate || checkIn >= _lastDate) return false;
            return true;
        }
        public void AddSuggestedDate(DateOnly startDate, DateOnly endDate)
        {
            if (IsSuggestedDateAvailable(startDate, endDate))
                _availableDates.Add(new DateRanges(startDate, endDate));
        }

        public bool IsSuggestedDateAvailable(DateOnly startDate, DateOnly endDate)
        {
            bool checkInIsInRange, checkOutIsInRange, containsBookedDates;
            foreach (DateRanges bookedDate in _bookedDates)
            {
                checkInIsInRange = IsCheckInInRange(bookedDate, startDate, endDate);
                checkOutIsInRange = IsCheckOutInRange(bookedDate, startDate, endDate);
                containsBookedDates = DoesContainsBookedDates(bookedDate, startDate, endDate);
                if (DiscardSuggestedDate(checkInIsInRange, checkOutIsInRange, containsBookedDates))
                    return false;
            }
            return true;
        }
        public bool IsCheckInInRange(DateRanges bookedDate, DateOnly startDate, DateOnly endDate)
        {
            return bookedDate.CheckIn >= startDate && bookedDate.CheckIn < endDate;
        }
        public bool IsCheckOutInRange(DateRanges bookedDate, DateOnly startDate, DateOnly endDate)
        {
            return bookedDate.CheckOut > startDate && bookedDate.CheckOut <= endDate;
        }
        public bool DoesContainsBookedDates(DateRanges bookedDate, DateOnly startDate, DateOnly endDate)
        {
            return false;
            //return startDate > bookedDate.CheckIn && endDate < bookedDate.CheckOut;
        }
        public bool DiscardSuggestedDate(bool checkInIsInRange, bool checkOutIsInRange, bool containsBookedDates)
        {
            if (checkInIsInRange || checkOutIsInRange || containsBookedDates)
                return true;
            else
                return false;
        }
    }
}
