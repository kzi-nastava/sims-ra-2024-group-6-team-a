using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;


namespace BookingApp.Model
{
    public class OwnerReview : ISerializable
    {
        public int ReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int Correctness { get; set; }
        public string AditionalComment { get; set; }

        public OwnerReview()
        {

        }
        public OwnerReview(int reservationId, int cleanliness, int correctness, string comment)
        {
            ReservationId = reservationId;
            Cleanliness = cleanliness;
            Correctness = correctness;
            AditionalComment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), Cleanliness.ToString(), Correctness.ToString(), AditionalComment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            Cleanliness = Convert.ToInt32(values[1]);
            Correctness = Convert.ToInt32(values[2]);
            AditionalComment = values[3];
        }
    }
}
