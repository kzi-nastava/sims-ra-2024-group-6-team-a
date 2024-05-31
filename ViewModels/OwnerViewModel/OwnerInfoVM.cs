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
using System.Windows.Input;

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

        public int CurrentYear {  get; set; }

        public SeriesCollection Reservations { get; set; }
        public SeriesCollection Cancelations { get; set; }
        public SeriesCollection ReviewsChart { get; set; }
        public ICommand CloseWindowCommand { get; }


        public List<String> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        public OwnerInfoVM(OwnerInfoDTO ownerInfoDTO)
        {
            CloseWindowCommand = new RelayCommand(CloseWindow);

            Id = ownerInfoDTO.Id;
            Name = ownerInfoDTO.Name;
            Surname = ownerInfoDTO.Surname;
            ReservationCount = ownerInfoDTO.Reservations;
            Accommodations = ownerInfoDTO.Accommodations;
            Grade = ownerInfoDTO.Grade;
            Status = ownerInfoDTO.Status;
            Ranking = ownerInfoDTO.Ranking;
            Labels = new List<String>();

            CurrentYear = DateTime.Today.Year;

            Reservations = new SeriesCollection { };

            Cancelations = new SeriesCollection { };

            ReviewsChart = new SeriesCollection { };

            foreach(Accommodation acc in AccommodationService.GetInstance().GetAll())
            {
                if(acc.OwnerId == Id)
                {
                    Reservations.Add(new ColumnSeries
                    {
                        Title = acc.Name,
                        Values = new ChartValues<int>(YearsAndReservations(acc.Id))
                    });

                    Cancelations.Add(new ColumnSeries
                    {
                        Title = acc.Name,
                        Values = new ChartValues<int>(YearsAndCancelations(acc.Id))
                    });
                }

            }

            //adding series will update and animate the chart automatically

            //also adding values updates and animates the chart automatically

            foreach(int y in GetAllYears())
            {
                Labels.Add(y.ToString());
            }


            for(int i = 1;i <= 5;i++)
            {
                ReviewsChart.Add(new PieSeries
                {
                    Title = i.ToString(),
                    Values = new ChartValues<int> { GetCurrentYearReviews()[i] }
                });

            }


        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        public List<int> YearsAndReservations(int accid)
        {
            List<int> reservations = new List<int>();
            foreach(int year in GetAllYears()) 
            {
                reservations.Add(AccommodationService.GetInstance().GetReservationCountForAccommodation(accid, year));
            }


            return reservations;
        }



        public List<int> YearsAndCancelations(int accid)
        {
            List<int> reservations = new List<int>();
            foreach (int year in GetAllYears())
            {
                reservations.Add(AccommodationService.GetInstance().GetCancelationCountForAccommodation(accid, year));
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

        public Dictionary<int,int> GetCurrentYearReviews()
        {

            Dictionary<int, int> reviews = new Dictionary<int, int>();
            reviews[1] = 0;
            reviews[2] = 0;
            reviews[3] = 0;
            reviews[4] = 0;
            reviews[5] = 0;
            foreach (OwnerReview review in OwnerReviewService.GetInstance().GetAll()) 
            {
                AccommodationReservation res = AccommodationReservationService.GetInstance().GetByReservationId(review.ReservationId);
                    reviews[review.Cleanliness]++;
                    reviews[review.Correctness]++;
   
            }

            return reviews;
        }

        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }




    }
}
