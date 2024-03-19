using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Resources;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourSchedule : ISerializable
    {
        public int Id { get; set; }
        public DateTime Start {  get; set; }

        public int CurrentFreeSpace { get; set; }

        public int TourId { get; set; }

        public Enums.TourActivity TourActivity { get; set; }

        public TourSchedule() { }

        public TourSchedule(DateTime start, int tourId, int currentFreeSpace)
        {
            Start = start;
            TourId = tourId;
            CurrentFreeSpace = currentFreeSpace;
        }
        public TourSchedule(DateTime start, int tourId, int currentFreeSpace, Enums.TourActivity tourActivity)
        {
            Start = start;
            TourId = tourId;
            CurrentFreeSpace = currentFreeSpace;
            TourActivity = tourActivity;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Start.ToString(), TourId.ToString() , CurrentFreeSpace.ToString(), TourActivity.ToString()};

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            
            TourId = Convert.ToInt32(values[2]);
            CurrentFreeSpace = Convert.ToInt32(values[3]);
            TourActivity = (Enums.TourActivity)Enum.Parse(typeof(Enums.TourActivity), values[4]);
        }
    }
}
