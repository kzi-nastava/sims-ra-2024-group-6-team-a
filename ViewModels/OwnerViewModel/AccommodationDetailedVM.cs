using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.ViewModels
{
    public class AccommodationDetailedVM : IObserver
    {

        public static ObservableCollection<ImageDTO> Images { get; set; }
        public ObservableCollection<ReservationOwnerDTO> Reservations { get; set; }
        public ObservableCollection<AccommodationStatisticDTO> Statistics { get; set; }
        public ObservableCollection<AccommodationRenovation> Renovations { get; set; }

        private string mainImage;

        public string MainImage
        {
            get { return mainImage; }

            set
            {
                if (mainImage != value)
                {
                    mainImage = value;
                }
            }
        }


        private int bestYear;

        public int BestYear
        {
            get { return bestYear; }
            set
            {
                if (bestYear != value)
                {
                    bestYear = value;
                }

            }
        }
       
        private double ratingNum;

        public String guestRating;
        public String GuestRating
        {
            get { return guestRating; }

            set
            {
                if(guestRating != value) 
                {
                guestRating = value;
                }
            }
        }
        public String ratingColor;
        public String RatingColor
        {
            get { return ratingColor; }

            set
            {
                if (guestRating != value)
                {
                    ratingColor = value;
                }
            }
        }


        public AccommodationOwnerDTO Accommodation { get; set; }
        public static List<Model.Image> imageModels { get; set; }

        public AccommodationRenovation SelectedRenovation { get; set; }
        public ReservationOwnerDTO SelectedReservation { get; set; }
        public AccommodationStatisticDTO SelectedStatistic { get; set; }

        public AccommodationDetailedVM(List<Model.Image> images, ObservableCollection<ReservationOwnerDTO> Reservations,AccommodationOwnerDTO accommodation) 
        {
            this.Accommodation = accommodation;
            this.MainImage = images[0].Path;
            Images = new ObservableCollection<ImageDTO>();
            Statistics = new ObservableCollection<AccommodationStatisticDTO>();

            imageModels = images;
            this.Reservations = Reservations;
          

            Renovations = new ObservableCollection<AccommodationRenovation>(RenovationService.GetInstance().GetAllByAccommodation(Accommodation.Id));
            this.bestYear = AccommodationService.GetInstance().GetMostPopularYear(Accommodation.Id);


            Update();
        }

        public void GatherAllYearsAllStats()
        {
            List<int> years = AccommodationService.GetInstance().GatherAllReservationYears(Reservations.ToList());

            foreach (int year in years)
            {

                AccommodationStatisticDTO stats = new AccommodationStatisticDTO(year,
                    AccommodationService.GetInstance().GetReservationCountForAccommodation(Accommodation.Id, year),
                    AccommodationService.GetInstance().GetChangesCountForAccommodation(Accommodation.Id, year),
                    AccommodationService.GetInstance().GetCancelationCountForAccommodation(Accommodation.Id,year),
                    0);
                Statistics.Add(stats);

            }
        }

        public void Update()
        {
            GetRatingAndColor();
            Images.Clear();
            Statistics.Clear();


            for(int i = 0;i < imageModels.Count;i = i +2)
            {
                 ImageDTO image = new ImageDTO(imageModels[i]);
                image.LeftPath = imageModels[i].Path;
                if(i+1 < imageModels.Count) 
                {
                    image.RightPath = imageModels[i + 1].Path;
                }
                

                Images.Add(image);
            }

            GatherAllYearsAllStats();


        }

        public void CalculateRating()
        {
            double sum = 0;
            double count = 0;
            foreach(OwnerReview review in OwnerReviewService.GetInstance().GetAll())
            {
                foreach(ReservationOwnerDTO reservation in Reservations)
                {
                    if(reservation.Id == review.ReservationId)
                    {
                        sum = sum + ((review.Correctness + review.Cleanliness) / 2);
                        count++;
                    }
                }
            }

            ratingNum = sum / count;
        }

        public void GetRatingAndColor()
        {
            CalculateRating();
            if (ratingNum >= 0.5 && ratingNum < 1)
            {
                GuestRating = "Very Poor";
                RatingColor = "#FF6347";
            }
            else if (ratingNum >= 1 && ratingNum < 1.5)
            {
                GuestRating = "Poor";
                RatingColor = "#FF6347";
            }
            else if (ratingNum >= 1.5 && ratingNum < 2)
            {
                GuestRating = "Fair";
                RatingColor = "#FFA500";
            }
            else if (ratingNum >= 2 && ratingNum < 2.5)
            {
                GuestRating = "Average";
                RatingColor = "#FFA500";
            }
            else if (ratingNum >= 2.5 && ratingNum < 3)
            {
                GuestRating = "Decent";
                RatingColor = "#FFD700";
            }
            else if (ratingNum >= 3 && ratingNum < 3.5)
            {
                GuestRating = "Good";
                RatingColor = "#FFFF00";
            }
            else if (ratingNum >= 3.5 && ratingNum < 4)
            {
                GuestRating = "Very Good";
                RatingColor = "#9ACD32";
            }
            else if (ratingNum >= 4 && ratingNum < 4.5)
            {
                GuestRating = "Great";
                RatingColor = "#00FF00";
            }
            else if (ratingNum >= 4.5 && ratingNum <= 5)
            {
                GuestRating = "Excellent";
                RatingColor = "#6495ED";
            }

        }

        public void SelectFirstReservation(ListBox ReservationsList)
        {
            if (SelectedReservation == null)
            {
                SelectedStatistic = null;
                SelectedRenovation = null;

                SelectedReservation = Reservations.First();
                ReservationsList.SelectedIndex = 0;
                ReservationsList.UpdateLayout();
                ReservationsList.Focus();

            }
        }

        internal void SelectFirstStatistic(ListBox statisticsList)
        {
            if(SelectedStatistic == null)
            {
                SelectedReservation = null;
                SelectedRenovation = null;

                SelectedStatistic = Statistics.First();
                statisticsList.SelectedIndex = 0;
                statisticsList.UpdateLayout();
                statisticsList.Focus();


            }
        }

        internal void SelectFirstRenovation(ListBox renovationsList)
        {
            if(SelectedRenovation == null)
            {
                SelectedReservation = null;
                SelectedStatistic = null;

                SelectedRenovation = Renovations.First();
                renovationsList.SelectedIndex = 0;
                renovationsList.UpdateLayout();
                renovationsList.Focus();

            }
        }
        internal void OpenMonthStatistic()
        {
            DetailedStatisticsView detailedStatisticsView = new DetailedStatisticsView(AccommodationService.GetInstance().GetMonthlyStatistics(Accommodation.Id,SelectedStatistic.Year),SelectedStatistic.Year);
            detailedStatisticsView.ShowDialog();
        }

        internal void ScheduleRenovation()
        {
            ScheduleRenovation scheduleRenovation = new ScheduleRenovation(Accommodation,Reservations.ToList());
            scheduleRenovation.ShowDialog();
        }
    }
}
