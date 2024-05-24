using BookingApp.Resources;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }
        public Enums.RequestStatus Status { get; set; }
        public int TouristId { get; set; }

        public ComplexTourRequest() { }

        public ComplexTourRequest(Enums.RequestStatus status, int touristId)
        {
            Status = status;
            TouristId = touristId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Status.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Status = (Enums.RequestStatus)Enum.Parse(typeof(Enums.RequestStatus), values[1]);
        }

    }
}
