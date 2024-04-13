using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
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
using System.Windows.Shapes;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Window
    {
        public static ObservableCollection<TourScheduleDTO> Tours {  get; set; }
        public User LoggedUser { get; set; }

        private TourRepository _tourRepository;
        private TourScheduleRepository _scheduleRepository;
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private TourReviewRepository _reviewRepository;

        public TourScheduleDTO SelectedTourSchedule { get; set; }
        public FinishedTours(User user)
        {
            InitializeComponent();
            DataContext = this;

            LoggedUser = user;
            _tourRepository = new TourRepository();
            _scheduleRepository = new TourScheduleRepository();
            _locationRepository = new LocationRepository();
            _imageRepository = new ImageRepository();
            _reviewRepository = new TourReviewRepository();
            Tours = new ObservableCollection<TourScheduleDTO>();

            Update();
        }

        private void Update()
        {
            Tours.Clear();
            foreach (TourSchedule tour in _tourRepository.GetAllFinishedTours(LoggedUser))
            {
                Tours.Add(new TourScheduleDTO(tour));
            }

        }

        private void RateTour_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTourSchedule != null)
            {
                TourRating rating = new TourRating(SelectedTourSchedule, _imageRepository, LoggedUser);
                rating.ShowDialog();
            }
           
        }

    }
}
