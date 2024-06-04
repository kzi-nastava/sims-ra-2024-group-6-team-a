using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.ApplicationServices;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class AccommodationBlog : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }

        public String Username { get; set; }
        public DateOnly CheckIn {  get; set; }
        public DateOnly CheckOut { get; set; }

        public String Title { get; set; }
        public String Description { get; set; }

        public bool Reported { get; set; }
        public string Popular { get; set; }

        public AccommodationBlog()
        {

        }

        public AccommodationBlog(int id, int accommodationId, string username, DateOnly checkIn, DateOnly checkOut, string title, string description, bool reported)
        {
            Id = id;
            AccommodationId = accommodationId;
            Username = username;
            CheckIn = checkIn;
            CheckOut = checkOut;
            Title = title;
            Description = description;
            Reported = reported;
        }

        public string[] ToCSV() 
        {
            string[] csvValues = {Id.ToString(), AccommodationId.ToString(),Username,CheckIn.ToString("dd.MM.yyyy"),CheckOut.ToString("dd.MM.yyyy"),Title,Description,Reported.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            Username = values[2];
            CheckIn = DateOnly.ParseExact(values[3], "dd.MM.yyyy");
            CheckOut = DateOnly.ParseExact(values[4], "dd.MM.yyyy");
            Title = values[5];
            Description = values[6];
            Reported = Boolean.Parse(values[7]);
            Popular = "";
            if(CommentService.GetInstance().IsBlogPopular(AccommodationId)) 
            {
                Popular = "This blog is popular!";
            }


        }

    }
}
