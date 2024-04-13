using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xaml.Schema;
using BookingApp.DTOs;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourGuests : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool IsPresent { get; set; }

        public int CheckpointId {  get; set; }

        public int UserTypeId { get; set; }

        public TourGuests() { }
        public TourGuests(int id,string name, string surname, int age, int reservationId, int userType)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            ReservationId = reservationId;
            UserTypeId = userType;
        }
       
       public TourGuests(int id, string name, string surname, int age, int reservationId, bool isPresent,int checkpointId, int userType)
       
        {
            Name = name;
            Surname = surname;
            Age = age;
            ReservationId = reservationId;
            IsPresent = isPresent;
            CheckpointId = checkpointId;
            UserTypeId = userType;
        }

        public TourGuests(TourGuestDTO guest, int reservationId)
        {
            Name = guest.Name;
            Surname = guest.Surname;
            Age = guest.Age;
            ReservationId = reservationId;
            IsPresent = false;
            UserTypeId = guest.UserType;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Surname, Age.ToString(), ReservationId.ToString(), IsPresent.ToString(), CheckpointId .ToString(), UserTypeId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Surname = values[2];
            Age = Convert.ToInt32(values[3]);
            ReservationId = Convert.ToInt32(values[4]);
            IsPresent = bool.Parse(values[5]);
            CheckpointId = Convert.ToInt32(values[6]);
            UserTypeId = Convert.ToInt32(values[7]);

        }
    }
}
