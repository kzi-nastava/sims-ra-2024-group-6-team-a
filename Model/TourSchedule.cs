using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourSchedule
    {
        public int Id { get; set; }
        public DateTime Start {  get; set; }
        public int TourId { get; set; }

        public TourSchedule() { }

        public TourSchedule(int id, DateTime start, int tourId)
        {
            Id = id;
            Start = start;
            TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Start.ToString(), TourId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Start = DateTime.Parse(values[1]);
            TourId = Convert.ToInt32(values[2]);
        }


    }
}
