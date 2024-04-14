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
    /// <summary>
    /// Interaction logic for ReviewDetailsPage.xaml
    /// </summary>
    public partial class ReviewDetailsPage : Page
    {


        private TourScheduleRepository _tourScheduleRepository;
        private TourRepository _tourRepository;
        private CheckpointRepository _checkpointRepository;
        private TourReviewRepository _tourReviewRepository;
        private ImageRepository _imageRepository;
        private UserRepository _userRepository;
        private int tourScheduleId;

        public static ObservableCollection<TourReviewDTO> Reviews { get; set; }


        public ReviewDetailsPage(int tourScheduleId)
        {
            InitializeComponent();
            DataContext = this;

            _tourScheduleRepository = new TourScheduleRepository();
            _tourRepository = new TourRepository();
            _checkpointRepository = new CheckpointRepository();
            _imageRepository = new ImageRepository();
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
               


              // Reviews.Add(new TourReviewDTO(tourScheduleId, review.GuideLanguageGrade, review.GuideKnowledgeGrade, review.TourAttractionsGrade, review.Impression, image.Path, username, checkpoint);
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
    }
}
