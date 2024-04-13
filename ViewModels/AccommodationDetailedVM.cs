using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public OwnerReviewRepository _reviews {  get; set; }
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

        public ReservationOwnerDTO SelectedReservation { get; set; }

        public AccommodationDetailedVM(List<Model.Image> images, ObservableCollection<ReservationOwnerDTO> Reservations,AccommodationOwnerDTO accommodation,OwnerReviewRepository _reviews) 
        {
            Images = new ObservableCollection<ImageDTO>();

            imageModels = images;
            this.Reservations = Reservations;
            this.Accommodation = accommodation;
            this._reviews = _reviews;
            

            Update();
        }

        public void Update()
        {
            GetRatingAndColor();
            Images.Clear();


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


        }

        public void CalculateRating()
        {
            double sum = 0;
            double count = 0;
            foreach(OwnerReview review in _reviews.GetAll())
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
                SelectedReservation = Reservations.First();
                ReservationsList.SelectedIndex = 0;
                ReservationsList.UpdateLayout();
                ReservationsList.Focus();

            }
        }


    }
}
