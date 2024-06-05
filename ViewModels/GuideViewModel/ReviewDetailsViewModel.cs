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
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class ReviewDetailsViewModel
    {
       
        private int tourScheduleId;

        public static ObservableCollection<TourReviewDTO> Reviews { get; set; }

        public List<Model.Image> CurrentReviewImages { get; set; }

        public TourSchedule TourSchedule { get; set; }

        public Tour Tour {  get; set; }

        public string Location {  get; set; }

        public string Language { get; set; }

        public string BigImagePath {  get; set; }

        public ReviewDetailsPage Window {  get; set; }

        public ICommand GoBackCommand { get; set; }

        public ReviewDetailsViewModel(ReviewDetailsPage window,int tourScheduleId)
        {

            GoBackCommand = new RelayCommand(GoBack);
            this.Window = window;
            Reviews = new ObservableCollection<TourReviewDTO>();
            this.tourScheduleId = tourScheduleId;

            LoadTourData(tourScheduleId);
           
            Update();
        }
        public void GoBack(object obj)
        {
            Window.NavigationService.GoBack();
        }   

        public void LoadTourData(int tourScheduleId)
        {
            TourSchedule = TourScheduleService.GetInstance().GetById(tourScheduleId);
            Tour = TourService.GetInstance().GetById(TourSchedule.TourId);
            Location location = LocationService.GetInstance().GetById(Tour.LocationId);
            Location = location.City + ", " + location.State;
            Language language = LanguageService.GetInstance().GetById(Tour.LanguageId);
            BigImagePath = GetFirstImagePath(Tour.Id);
            Language = language.Name;
        }

        public string GetFirstImagePath(int tourId) 
        {
            List<Model.Image> image = ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour);

            return image.First().Path;
        }

        public void Update()
        {
            Reviews.Clear();
            foreach (TourReview review in TourReviewService.GetInstance().GetAllReviewsByScheduleId(tourScheduleId))
            {
                CurrentReviewImages = GetAllImages(review.ScheduleId);
                Tourist tourist = TouristService.GetInstance().GetByTouristId(review.TouristId);
                int checkpointId = GetFirstUsersCheckpointId(tourScheduleId, review.TouristId);
                Checkpoint checkpoint = GetCheckpointById(checkpointId);

                Reviews.Add(new TourReviewDTO(review.Id,tourScheduleId, review.GuideLanguageGrade, review.GuideKnowledgeGrade, review.TourAttractionsGrade, review.Impression, CurrentReviewImages, review.TouristId,tourist, checkpoint.Name,review.IsValid));
            }
        }

        public string GetUsername(int userId)
        {
            return UserService.GetInstance().GetUsername(userId);

        }
        public List <Model.Image> GetAllImages(int tourShceduleId)
        {
            return ImageService.GetInstance().GetByEntity(tourShceduleId, Enums.ImageType.TourReview);
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
