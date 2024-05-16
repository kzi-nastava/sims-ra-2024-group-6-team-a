using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class AccommodationRenovation : ISerializable
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int AccommodationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set;}

        public AccommodationRenovation() { }

        public AccommodationRenovation(int id, string title, int accommodationId, DateOnly startDate, DateOnly endDate)
        {
            Id = id;
            Title = title;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV()
        {

            string[] csvValues = { Id.ToString(), Title,AccommodationId.ToString(),StartDate.ToString("dd.MM.yyyy"),EndDate.ToString("dd.MM.yyyy") };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Title = values[1];
            AccommodationId = Convert.ToInt32(values[2]);
            StartDate = DateOnly.ParseExact(values[3], "dd.MM.yyyy");
            EndDate = DateOnly.ParseExact(values[4], "dd.MM.yyyy");



        }


    }
}
