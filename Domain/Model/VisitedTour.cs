using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class VisitedTour : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public DateOnly Date {  get; set; }

        public VisitedTour() { }

        public VisitedTour( int touristId, DateOnly date)
        {
            
            TouristId = touristId;
            Date = date;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), Date.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            Date = DateOnly.Parse(values[2]);
        }
    }
}
