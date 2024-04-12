using BookingApp.Serializer;
using System;
using BookingApp.Resources;

namespace BookingApp.Model
{

    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Enums.UserType Type { get; set; }


        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password,Type.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Type = (Enums.UserType)Enum.Parse(typeof(Enums.UserType), values[3]);
        }
    }
}
