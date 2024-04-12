using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Checkpoint : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }
        
        public bool IsReached {  get; set; }

        public int TourScheduleId {  get; set; }


        public Checkpoint() { }

        public Checkpoint(string name, int tourId, bool isReached, int tourScheduleId)
        {
            Name = name;
            TourId = tourId;
            IsReached = isReached;
            TourScheduleId = tourScheduleId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, TourId.ToString(), IsReached.ToString(), TourScheduleId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            TourId = Convert.ToInt32(values[2]);
            IsReached = bool.Parse(values[3]);
            TourScheduleId = Convert.ToInt32(values[4]);
        }

    }

}
