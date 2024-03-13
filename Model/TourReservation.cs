using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }

        public int TouristId { get; set; } //Id turiste koji pravi rezervaciju ture

        public int TourId { get; set; } 
        
        public int GuestNumber { get; set; }

        public List<TourGuests> TourGuests {  get; set; } //List of guests that will be assigned to a tour by 'main' tourist

        public int ReservedTourTime { get; set; }
        public TourReservation() 
        {
            TourGuests = new List<TourGuests>();
        }

        public TourReservation(int id, int touristId, int tourId, int reservedTourTime, int guestNumber)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            ReservedTourTime = reservedTourTime;
            GuestNumber = guestNumber;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), TourId.ToString(), ReservedTourTime.ToString(), GuestNumber.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            ReservedTourTime = Convert.ToInt32(values[3]);
            GuestNumber = Convert.ToInt32(values[4]);

        }
    }
}
