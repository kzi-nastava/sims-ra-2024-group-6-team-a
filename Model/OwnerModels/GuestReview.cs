using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Resources;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class GuestReview : ISerializable
    {
        public int ReservationId { get; set; }
        public int CleanlinessGrade { get; set; }
        public int RespectGrade { get; set; }
        public string Comment { get; set; }

        public GuestReview()
        {

        }

        public GuestReview(int reservationId, int cleanlinessGrade, int respectGrade, string comment)
        {
            ReservationId = reservationId;
            CleanlinessGrade = cleanlinessGrade;
            RespectGrade = respectGrade;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), CleanlinessGrade.ToString(),RespectGrade.ToString(),Comment};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            CleanlinessGrade = Convert.ToInt32(values[1]);
            RespectGrade = Convert.ToInt32(values[2]);
            Comment = values[3];
        }
    }
}
