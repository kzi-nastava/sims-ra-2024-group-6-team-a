using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;
using BookingApp.Resources;

namespace BookingApp.Domain.Model
{
    public class Guide : User,ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public int NumberOfTours { get; set; }  
        public Enums.GuideRank Rank { get; set; }

        public int UserId { get; set; }

        public string Language { get; set; }

        public double Grade { get; set; }


        public Guide() { }
        public Guide(int id, string name, string surname, int age,int numberOfTours, Enums.GuideRank rank ,int userId, string language, double grade)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
            NumberOfTours = numberOfTours;
            Rank = rank;
            Language = language;
            Grade = grade;
        }
        public Guide(string name, string surname, int age,int numberOfTours,Enums.GuideRank rank ,int userId, string language, double grade)
        {
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
            NumberOfTours = numberOfTours;
            Rank = rank;
            Language = language;
            Grade = grade;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Age = Convert.ToInt32(values[3]);
            NumberOfTours = Convert.ToInt32(values[4]);
            Rank = (Enums.GuideRank)Enum.Parse(typeof(Enums.GuideRank), values[5]);
            UserId = Convert.ToInt32(values[6]);
            Language = values[7];
            Grade = Convert.ToDouble(values[8]);

        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Name, Surname, Age.ToString(),NumberOfTours.ToString(), Rank.ToString(),UserId.ToString(),Language, Grade.ToString()};
            return csvvalues;
        }
    }
}
