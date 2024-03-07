using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

using BookingApp.Resources;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int LocationId { get; set; }
        public Enums.AccommodationType Type { get; set; }

        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancelationDays { get; set; }
        List<Image> Images { get; set; }

        public Accommodation()
        {

        }

        public Accommodation(string name,Enums.AccommodationType type,int maxGuests,int minReservationDays,int cancelationDays,int publicId,int ownerId)
        {
            Name = name;
            Type = type;    
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancelationDays = cancelationDays;
            LocationId = LocationId;
            OwnerId = ownerId;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancelationDays.ToString(),LocationId.ToString(),OwnerId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Type = (Enums.AccommodationType)Enum.Parse(typeof(Enums.AccommodationType), values[2]);
            MaxGuests = Convert.ToInt32(values[3]);
            MinReservationDays= Convert.ToInt32(values[4]);
            CancelationDays= Convert.ToInt32(values[5]);
            LocationId = Convert.ToInt32(values[6]);
            OwnerId = Convert.ToInt32(values[7]);
        }


    }
}
