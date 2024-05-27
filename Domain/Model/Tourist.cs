﻿using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class Tourist : User, ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }

        public Tourist() { }
        public Tourist(int id, string name, string surname, int age, int userId, int points)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
            Points = points;
        }
        public Tourist(string name, string surname, int age, int userId, int points)
        {
            Name = name;
            Surname = surname;
            Age = age;
            UserId = userId;
            Points = points;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Age = Convert.ToInt32(values[3]);
            UserId = Convert.ToInt32(values[4]);
            Points = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues = {Id.ToString(), Name, Surname, Age.ToString(), UserId.ToString(), Points.ToString()};
            return csvvalues;
        }

    }
}
