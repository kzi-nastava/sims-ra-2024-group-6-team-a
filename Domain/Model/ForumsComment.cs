using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookingApp.Resources;

using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class ForumsComment : ISerializable
    {
        public int ForumId { get; set; }
        public DateOnly CreationTime { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public Enums.UserType UserType { get; set; }

        public ForumsComment() { }
        public ForumsComment(int forumsId, int userId, DateOnly creationTime, string text, Enums.UserType userType, int status)
        {
            ForumId = forumsId;
            CreationTime = creationTime;
            Text = text;
            UserId = userId;
            UserType = userType;
            Status = status;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { ForumId.ToString(),
                UserId.ToString(),
                CreationTime.ToString("dd.MM.yyyy"),
                UserType.ToString(),
                Text,
                 Status.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ForumId = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            CreationTime = DateOnly.ParseExact(values[2], "dd.MM.yyyy");
            UserType = (Enums.UserType)Enum.Parse(typeof(Enums.UserType), values[3]);
            Text = values[4];
            Status = Convert.ToInt32(values[5]);

        }
    }
}
