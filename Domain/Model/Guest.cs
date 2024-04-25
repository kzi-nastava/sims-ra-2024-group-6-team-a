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
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateOnly Birthday { get; set; }
        public DateOnly StartSuperGuestDate { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int BonusPoints { get; set; }
        public bool IsSuperGuest { get; set; }

        public Guest() { }
        public Guest(int id, string username, string name, string surname, string phone, string email, DateOnly birthday, int userId, bool isSuperGuest, DateOnly startSuperGuest, int bonusPoints)
        {
            Id = id;
            Username = username;
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
            Birthday = birthday;
            UserId = userId;
            IsSuperGuest = isSuperGuest;
            StartSuperGuestDate = startSuperGuest;
            BonusPoints = bonusPoints;
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
            IsSuperGuest = Convert.ToBoolean(values[7]);
            Username = values[8];
            BonusPoints = Convert.ToInt32(values[9]);
            StartSuperGuestDate = DateOnly.ParseExact(values[10], "dd.MM.yyyy");

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
                IsSuperGuest.ToString(),
                Username,
                BonusPoints.ToString(),
                StartSuperGuestDate.ToString("dd.MM.yyyy"),

            };
            return csvvalues;
        }
    }
}
