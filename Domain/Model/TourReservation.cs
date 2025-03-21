﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }

        public int TouristId { get; set; }

        public int TourId { get; set; } 
        
        public int GuestNumber { get; set; }

        public List<TourGuests> TourGuests {  get; set; } 

        public int TourRealisationId { get; set; }
        public TourReservation() 
        {
            TourGuests = new List<TourGuests>();
        }

        public TourReservation(int id, int touristId, int tourId, int tourRealisationId, int guestNumber)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            TourRealisationId = tourRealisationId;
            GuestNumber = guestNumber;
        }

        public TourReservation(int guestNumber, int tourRealisationId, int tourId, int loggedUserId)
        {
            GuestNumber = guestNumber;
            TourRealisationId = tourRealisationId;
            TourId = tourId;
            TouristId = loggedUserId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), TourId.ToString(), TourRealisationId.ToString(), GuestNumber.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            TourRealisationId = Convert.ToInt32(values[3]);
            GuestNumber = Convert.ToInt32(values[4]);

        }
    }
}
