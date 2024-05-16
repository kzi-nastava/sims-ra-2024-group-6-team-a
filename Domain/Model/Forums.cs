using BookingApp.Model;
using System;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Resources;

namespace BookingApp.Domain.Model
{
    public class Forums : ISerializable
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateOnly CreationTime { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public Enums.UserType UserType { get; set; }

        public Forums() { }
        public Forums(int locationId,int userId, DateOnly creationTime, string text, Enums.UserType userType)
        {
            LocationId = locationId;
            CreationTime = creationTime;
            Text = text;
            UserId = userId;
            UserType = userType;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),
                LocationId.ToString(),
                UserId.ToString(),
                CreationTime.ToString("dd.MM.yyyy"),
                UserType.ToString(),
                Text};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            CreationTime = DateOnly.ParseExact(values[3], "dd.MM.yyyy");
            UserType = (Enums.UserType)Enum.Parse(typeof(Enums.UserType), values[4]);
            Text = values[5];
        }
    }
}
