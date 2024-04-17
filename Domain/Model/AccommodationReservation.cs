using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public int GuestId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int GuestNumber { get; set; }
        public Enums.ReservationStatus Status { get; set; }

        public AccommodationReservation()
        {
        }
        public AccommodationReservation(int accommodationId, int guestId, DateOnly checkInDate, DateOnly checkOutDate, int guestNumber, Enums.ReservationStatus status, Accommodation accommodation)
        {
            AccommodationId = accommodationId;
            GuestId = guestId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            GuestNumber = guestNumber;
            Status = status;
            Accommodation = accommodation;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationId.ToString(), GuestId.ToString(), CheckInDate.ToString("dd.MM.yyyy"), CheckOutDate.ToString("dd.MM.yyyy"), GuestNumber.ToString(), Status.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            GuestId = Convert.ToInt32(values[2]);
            CheckInDate = DateOnly.ParseExact(values[3], "dd.MM.yyyy");
            CheckOutDate = DateOnly.ParseExact(values[4], "dd.MM.yyyy");
            GuestNumber = Convert.ToInt32(values[5]);
            Status = (Enums.ReservationStatus)Enum.Parse(typeof(Enums.ReservationStatus), values[6]);

        }
    }
}
