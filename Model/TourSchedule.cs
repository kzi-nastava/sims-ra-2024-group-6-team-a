using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;
namespace BookingApp.Model
{
    public class TourSchedule : ISerializable
    {
        public int Id { get; set; }
        public DateTime Start {  get; set; }

        public int TouristCount { get; set; }

        public int TourId { get; set; }

        public TourSchedule() { }

        public TourSchedule(DateTime start, int tourId, int touristCount)
        {
            Start = start;
            TourId = tourId;
            TouristCount = touristCount;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Start.ToString(), TourId.ToString() , TouristCount.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Start = DateTime.Parse(values[1]);
            TourId = Convert.ToInt32(values[2]);
            TouristCount = Convert.ToInt32(values[3]);
        }

    }
}
