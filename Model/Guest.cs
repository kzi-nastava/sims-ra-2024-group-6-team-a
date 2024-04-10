using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Guest : ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateOnly Birthday { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SuperGuestId { get; set; }
        public SuperGuest SuperGuest { get; set; }

        public Guest() { }
        public Guest(int id, string name, string surname, string phone, string email, DateOnly birthday, int userId, int superGuestId, SuperGuest superGuest)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
            Birthday = birthday;
            UserId = userId;
            SuperGuestId = superGuestId;
            SuperGuest = superGuest;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Phone = values[3];
            Birthday = DateOnly.ParseExact(values[4], "dd.MM.yyyy");
            Email = values[5];
            UserId = Convert.ToInt32(values[6]);
            SuperGuestId = Convert.ToInt32(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues =
            {
                Id.ToString(),
                Name,
                Surname,
                Phone,
                Birthday.ToString("dd.MM.yyyy"),
                Email,
                UserId.ToString(),
                SuperGuestId.ToString()
            };
            return csvvalues;
        }
    }
}
