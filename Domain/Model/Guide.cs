using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class Guide : User,ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public int UserId { get; set; }

        public Guide() { }
        public Guide(int id, string name, string surname, int age, int userId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
        }
        public Guide(string name, string surname, int age, int userId)
        {
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Age = Convert.ToInt32(values[3]);
            UserId = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Name, Surname, Age.ToString(), UserId.ToString() };
            return csvvalues;
        }
    }
}
