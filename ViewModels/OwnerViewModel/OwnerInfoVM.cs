using BookingApp.DTOs;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using BookingApp.Model;
using BookingApp.ApplicationServices;
using System.Windows;
using Microsoft.VisualBasic;

namespace BookingApp.ViewModels
{
    public class OwnerInfoVM
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public int ReservationCount { get; set; }
        public int Accommodations { get; set; }
        public double Grade { get; set; }
        public String Status { get; set; }
        public int Ranking { get; set; }


        public SeriesCollection Reservations { get; set; }
        public List<String> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        public OwnerInfoVM(OwnerInfoDTO ownerInfoDTO)
        {
            Id = ownerInfoDTO.Id;
            Name = ownerInfoDTO.Name;
            Surname = ownerInfoDTO.Surname;
            ReservationCount = ownerInfoDTO.Reservations;
            Accommodations = ownerInfoDTO.Accommodations;
            Grade = ownerInfoDTO.Grade;
            Status = ownerInfoDTO.Status;
            Ranking = ownerInfoDTO.Ranking;
            Labels = new List<String>();

            Reservations = new SeriesCollection { };

            foreach(Accommodation acc in AccommodationService.GetInstance().GetAll())
            {
                if(acc.OwnerId == Id)
                {
                    Reservations.Add(new ColumnSeries
                    {
                        Title = acc.Name,
                        Values = new ChartValues<int>(YearsAndReservations(acc.Id))
                    }); 
                }

            }

            //adding series will update and animate the chart automatically

            //also adding values updates and animates the chart automatically

            foreach(int y in GetAllYears())
            {
                Labels.Add(y.ToString());
            }
            
            

        }

        public List<int> YearsAndReservations(int accid)
        {
            List<int> reservations = new List<int>();
            foreach(int year in GetAllYears()) 
            {
                reservations.Add(AccommodationService.GetInstance().GetReservationCountForAccommodation(accid, year));
            }


            return reservations;
        }

        public List<int> GetAllYears()
        {
            List<int> years = new List<int>();
            foreach (AccommodationReservation res in AccommodationReservationService.GetInstance().GetAll())
            {
  
                Accommodation acc = AccommodationService.GetInstance().GetByReservationId(res.AccommodationId);
                
                if (acc.OwnerId == Id)
                {
                    if (!years.Contains(res.CheckOutDate.Year))
                    {
                        years.Add(res.CheckOutDate.Year);
                    }
                }
            }

            return years;
        }




    }
}
