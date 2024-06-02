using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public String Text { get; set; }
        public String Username { get; set; }

        public int Ratio { get; set; }

        

        public String Status { get; set; }

        public Comment() { }

        public Comment(int blogId, String text, String user, int ratio,String status)
        {   
            BlogId = blogId;
            Text = text;
            Username = user;
            Ratio = ratio;
           
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), BlogId.ToString(), Text, Username,Ratio.ToString(),Status };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            BlogId = Convert.ToInt32(values[1]);
            Text = values[2];
            Username = values[3];
            Ratio = Convert.ToInt32(values[4]);
           
            Status = values[5];

        }
    }
}
