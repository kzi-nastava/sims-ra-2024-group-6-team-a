using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.View.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Security.RightsManagement;
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

        public ObservableCollection<AccommodationBlog> Blogs {  get; set; }

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

        public String suggestion;

        public String Suggestion
        {
            get { return suggestion; }

            set
            {
                if(suggestion != value)
                {
                    suggestion = value;
                }
            }
        }


        public AccommodationOwnerDTO Accommodation { get; set; }
        public static List<Model.Image> imageModels { get; set; }

        public AccommodationRenovation SelectedRenovation { get; set; }
        public ReservationOwnerDTO SelectedReservation { get; set; }
        public AccommodationStatisticDTO SelectedStatistic { get; set; }
        public AccommodationBlog SelectedBlog { get; set; }

        public AccommodationDetailedVM(List<Model.Image> images, ObservableCollection<ReservationOwnerDTO> Reservations,AccommodationOwnerDTO accommodation) 
        {
            this.Accommodation = accommodation;
            this.MainImage = images[0].Path;
            Images = new ObservableCollection<ImageDTO>();
            Statistics = new ObservableCollection<AccommodationStatisticDTO>();
            Blogs = new ObservableCollection<AccommodationBlog>();

            imageModels = images;
            this.Reservations = Reservations;
            Renovations = new ObservableCollection<AccommodationRenovation>();
            Suggestion = AccommodationService.GetInstance().GetSuggestionForAccommodation(accommodation.Id);



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
                    AccommodationService.GetInstance().GetRenovationCountForAccommodation(Accommodation.Id,year));
                Statistics.Add(stats);

            }
        }

        public void Update()
        {
            GetRatingAndColor();
            Images.Clear();
            Statistics.Clear();
            Renovations.Clear();
            Blogs.Clear();

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

            foreach (AccommodationRenovation ren in RenovationService.GetInstance().GetAll())
            {
                if (ren.AccommodationId == Accommodation.Id)
                    Renovations.Add(ren);
            }

            foreach (AccommodationBlog blog in AccommodationBlogService.GetInstance().GetAll())
            {
                if(blog.AccommodationId == Accommodation.Id)
                    Blogs.Add(blog);
            }



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


        internal void OpenMonthStatistic()
        {
            DetailedStatisticsView detailedStatisticsView = new DetailedStatisticsView(AccommodationService.GetInstance().GetMonthlyStatistics(Accommodation.Id,SelectedStatistic.Year),SelectedStatistic.Year);
            detailedStatisticsView.ShowDialog();
            
            Update();
        }

        internal void ScheduleRenovation()
        {
            ScheduleRenovation scheduleRenovation = new ScheduleRenovation(Accommodation,Reservations.ToList());
            scheduleRenovation.ShowDialog();
            Update();
        }

        internal void RemoveRenovation()
        {
            AccommodationRenovation renovation = RenovationService.GetInstance().GetAll().Find(r => r.Id == SelectedRenovation.Id);
            RenovationService.GetInstance().Delete(renovation);
            Update();
        }

        internal void CloseAccommodation()
        {
            foreach(AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetByAccommodationId(Accommodation.Id)) 
            {
                AccommodationReservationService.GetInstance().Delete(reservation);
                GuestReviewService.GetInstance().Delete(GuestReviewService.GetInstance().GetByReservationId(reservation.Id));
            }

            foreach(ReservationChanges change in ReservationChangeService.GetInstance().GetAll())
            {
                if(change.AccommodationId == Accommodation.Id) 
                {
                    ReservationChangeService.GetInstance().Delete(change); 
                }
            }

            foreach(AccommodationRenovation renovation in RenovationService.GetInstance().GetAllByAccommodation(Accommodation.Id))
            {
                RenovationService.GetInstance().Delete(renovation);
            }

            foreach(Model.Image image in ImageService.GetInstance().GetImagesForAccommodaton(Accommodation.Id))
            {
                ImageService.GetInstance().Delete(image);
            }

            AccommodationService.GetInstance().Delete(Accommodation.Id);
        }

        internal void OpenBlog()
        {
            BlogView blogView = new BlogView(SelectedBlog,mainImage,OwnerService.GetInstance().GetOwnerNameByAccommodation(Accommodation.Id));
            blogView.ShowDialog();
            Update();
        }
    }
}
