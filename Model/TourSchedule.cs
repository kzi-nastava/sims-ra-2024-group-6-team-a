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

        public int CurrentGuestNumber { get; set; }

        public int TourId { get; set; }

        public Enums.TourActivity TourActivity { get; set; }

        public TourSchedule() { }

        public TourSchedule(DateTime start, int tourId, int currentGuestNumber)
        {
            Start = start;
            TourId = tourId;
            CurrentGuestNumber = currentGuestNumber;
        }
        public TourSchedule(DateTime start, int tourId, int currentGuestNumber, Enums.TourActivity tourActivity)
        {
            Start = start;
            TourId = tourId;
            CurrentGuestNumber = currentGuestNumber;
            TourActivity = tourActivity;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Start.ToString(), TourId.ToString() , CurrentGuestNumber.ToString(), TourActivity.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Start = DateTime.Parse(values[1]);
            TourId = Convert.ToInt32(values[2]);
            CurrentGuestNumber = Convert.ToInt32(values[3]);
            TourActivity = (Enums.TourActivity)Enum.Parse(typeof(Enums.TourActivity), values[4]);

        }

    }
}
