using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuideView.Pages
{
    
    public partial class ReviewDetailsPage : Page
    {


        private TourScheduleRepository _tourScheduleRepository;
        private TourRepository _tourRepository;
        private CheckpointRepository _checkpointRepository;
        private TourReviewRepository _tourReviewRepository;
        private ImageRepository _imageRepository;
        private UserRepository _userRepository;
        private TourGuestRepository _tourGuestRepository;
        private TourReservationRepository _tourReservationRepository;
        private int tourScheduleId;
   
        public static ObservableCollection<TourReviewDTO> Reviews { get; set; }


        public ReviewDetailsPage(int tourScheduleId)
        {
            InitializeComponent();
            DataContext = this;

            _tourReviewRepository = new TourReviewRepository();
            _tourScheduleRepository = new TourScheduleRepository();
            _tourRepository = new TourRepository();
            _checkpointRepository = new CheckpointRepository();
            _imageRepository = new ImageRepository();
            _tourGuestRepository = new TourGuestRepository();
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            Reviews = new ObservableCollection<TourReviewDTO>();

            this.tourScheduleId = tourScheduleId;
            Update();

        }

        private void Update()
        {
            Reviews.Clear();
            foreach(TourReview review in _tourReviewRepository.GetAllReviewsByScheduleId(tourScheduleId))
            {
                Model.Image image = GetFirstTourImage(review.ScheduleId);
                string username = GetUsername(review.TouristId);
                int checkpointId = GetFirstUsersCheckpointId(tourScheduleId, review.TouristId);
                Checkpoint checkpoint = GetCheckpointById(checkpointId);

             Reviews.Add(new TourReviewDTO(tourScheduleId, review.GuideLanguageGrade, review.GuideKnowledgeGrade, review.TourAttractionsGrade, review.Impression, image.Path,review.TouristId, username, checkpoint.Name));
            }
        }

        private string GetUsername(int userId)
        {
            return _userRepository.GetUsername(userId);

        }
        private Model.Image GetFirstTourImage(int tourShceduleId)
        {
            return _imageRepository.GetByEntity(tourShceduleId, Enums.ImageType.TourReview).First();
        }

        private int GetFirstUsersCheckpointId(int tourShceduleId, int userId)
        {
            List <TourGuests> tourGuests = _tourGuestRepository.GetAllByTourId(tourShceduleId);
            List<TourReservation> reservations = _tourReservationRepository.GetAllByRealisationId(tourScheduleId);
            foreach(TourGuests tourGuest in tourGuests)
            {
                
                foreach(TourReservation reservation in reservations)
                {
                    if(reservation.Id == tourGuest.ReservationId && tourGuest.UserTypeId == userId && tourGuest.IsPresent == true)
                    {
                        return tourGuest.CheckpointId;
                    }

                }
            }

            return 0;
        }
        private Checkpoint GetCheckpointById(int checkpointId) 
        { 
            return _checkpointRepository.GetById(checkpointId);
        }
    }
}
