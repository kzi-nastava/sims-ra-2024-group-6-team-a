using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class ReservationChanges : ISerializable
    {
      
        public int ReservationId { get; set; }

        public DateOnly OldCheckIn {  get; set; }
        public DateOnly OldCheckOut { get; set; }
        public DateOnly NewCheckIn { get; set; }
        public DateOnly NewCheckOut { get; set; }
        
        public string Comment { get; set; }
        public Enums.ReservationChangeStatus Status { get; set; }

        public ReservationChanges()
        {

        }

        public ReservationChanges(int reservationId, DateOnly oldCheckIn, DateOnly oldCheckOut, DateOnly newCheckIn, DateOnly newCheckOut, string comment, Enums.ReservationChangeStatus status)
        {
            ReservationId = reservationId;
            OldCheckIn = oldCheckIn;
            OldCheckOut = oldCheckOut;
            NewCheckIn = newCheckIn;
            NewCheckOut = newCheckOut;
            Comment = comment;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(),  OldCheckIn.ToString("dd.MM.yyyy"),OldCheckOut.ToString("dd.MM.yyyy"), NewCheckIn.ToString("dd.MM.yyyy"), NewCheckOut.ToString("dd.MM.yyyy") , Status.ToString(),Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            OldCheckIn = DateOnly.ParseExact(values[1], "dd.MM.yyyy");
            OldCheckOut = DateOnly.ParseExact(values[2], "dd.MM.yyyy");
            NewCheckIn = DateOnly.ParseExact(values[3], "dd.MM.yyyy");
            NewCheckOut = DateOnly.ParseExact(values[4], "dd.MM.yyyy");
            
            Status = (Enums.ReservationChangeStatus)Enum.Parse(typeof(Enums.ReservationChangeStatus), values[5]);
            Comment = values[6];

        }
    }
}
