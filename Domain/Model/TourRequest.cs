using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public int LocationId {  get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TouristId { get; set; }
        public Enums.RequestStatus Status { get; set; }

        
        public TourRequest() { }

        public TourRequest(int locationId, int languageId, string description, DateOnly startDate, DateOnly endDate, int touristId, Enums.RequestStatus status)
        {
            LocationId = locationId;
            LanguageId = languageId;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            TouristId = touristId;
            Status = status;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), LocationId.ToString(), LanguageId.ToString(), Description, StartDate.ToString(), EndDate.ToString(), TouristId.ToString(), Status.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
            LanguageId = Convert.ToInt32(values[2]);
            Description = values[3];
            StartDate = DateOnly.Parse(values[4]);
            EndDate = DateOnly.Parse(values[5]);
            TouristId = Convert.ToInt32(values[6]);
            Status = (Enums.RequestStatus)Enum.Parse(typeof(Enums.RequestStatus), values[7]);
        }
    }
}
