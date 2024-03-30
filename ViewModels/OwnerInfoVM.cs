using BookingApp.DTOs;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class OwnerInfoVM : IObserver
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public int Reservations { get; set; }
        public int Accommodations { get; set; }
        public double Grade { get; set; }
        public String Status { get; set; }
        public int Ranking { get; set; }

        

        public OwnerInfoVM(OwnerInfoDTO ownerInfoDTO)
        {
            Name = ownerInfoDTO.Name;
            Surname = ownerInfoDTO.Surname;
            Reservations = ownerInfoDTO.Reservations;
            Accommodations = ownerInfoDTO.Accommodations;
            Grade = ownerInfoDTO.Grade;
            Status = ownerInfoDTO.Status;
            Ranking = ownerInfoDTO.Ranking;

            Update();     
        }

        public void Update()
        {


        }

    }
}
