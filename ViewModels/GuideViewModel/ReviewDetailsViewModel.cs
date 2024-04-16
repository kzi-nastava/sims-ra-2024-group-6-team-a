using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class ReviewDetailsViewModel
    {
       
        private int tourScheduleId;

        public static ObservableCollection<TourReviewDTO> Reviews { get; set; }

        public ReviewDetailsPage Window {  get; set; }

        public ReviewDetailsViewModel(ReviewDetailsPage window,int tourScheduleId)
        {
           

           
            Reviews = new ObservableCollection<TourReviewDTO>();

            this.tourScheduleId = tourScheduleId;
            Update();

        }

        public void Update()
        {
            Reviews.Clear();
            foreach (TourReview review in TourReviewService.GetInstance().GetAllReviewsByScheduleId(tourScheduleId))
            {
                Model.Image image = GetFirstTourImage(review.ScheduleId);
                string username = GetUsername(review.TouristId);
                int checkpointId = GetFirstUsersCheckpointId(tourScheduleId, review.TouristId);
                Checkpoint checkpoint = GetCheckpointById(checkpointId);

                Reviews.Add(new TourReviewDTO(review.Id,tourScheduleId, review.GuideLanguageGrade, review.GuideKnowledgeGrade, review.TourAttractionsGrade, review.Impression, image.Path, review.TouristId, username, checkpoint.Name,review.IsValid));
            }
        }

        public string GetUsername(int userId)
        {
            return UserService.GetInstance().GetUsername(userId);

        }
        public Model.Image GetFirstTourImage(int tourShceduleId)
        {
            return ImageService.GetInstance().GetByEntity(tourShceduleId, Enums.ImageType.TourReview).First();
        }

        public int GetFirstUsersCheckpointId(int tourShceduleId, int userId)
        {
            List<TourGuests> tourGuests = TourGuestService.GetInstance().GetAllByTourId(tourShceduleId);
            List<TourReservation> reservations = TourReservationService.GetInstance().GetAllByRealisationId(tourScheduleId);
            foreach (TourGuests tourGuest in tourGuests)
            {

                foreach (TourReservation reservation in reservations)
                {
                    if (IsMatchingGuestAndReservation(tourGuest, reservation, userId))
                    {
                        return tourGuest.CheckpointId;
                    }

                }
            }

            return 0;
        }

        private bool IsMatchingGuestAndReservation(TourGuests tourGuest, TourReservation reservation, int userId)
        {
            return reservation.Id == tourGuest.ReservationId &&
                   tourGuest.UserTypeId == userId &&
                   tourGuest.IsPresent;
        }


        public Checkpoint GetCheckpointById(int checkpointId)
        {
            return CheckpointService.GetInstance().GetById(checkpointId);
        }
    }
}
