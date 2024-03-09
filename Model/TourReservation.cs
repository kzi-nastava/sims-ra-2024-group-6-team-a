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

        public int TouristId { get; set; }

        public int TourId { get; set; }

        public List<TourGuests> TourGuests {  get; set; } //List of guests that will be assigned to a tour by 'main' tourist
        public TourReservation() 
        {
            TourGuests = new List<TourGuests>();
        }

        public TourReservation(int id, int touristId, int tourId, List<TourGuests> tourGuests)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            TourGuests = tourGuests;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), TourId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);

        }
    }
}
