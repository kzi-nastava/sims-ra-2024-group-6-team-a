using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using Microsoft.Win32;
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
    /// Interaction logic for TourRating.xaml
    /// </summary>
    public partial class TourRating : Window
    {
        private readonly ImageRepository _imageRepository;
        private readonly TourReviewRepository _tourReviewRepository = new TourReviewRepository();
        public TourScheduleDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }

        public TourReviewDTO TourReviewDTO { get; set; }
        public String SelectedImageUrl { get; set; }
        public static ObservableCollection<String> ImagesCollection { get; set; }
        public TourRating(TourScheduleDTO selectedTour, ImageRepository imageRepository, User user)
        {
            InitializeComponent();
            DataContext = this;

            _imageRepository = imageRepository;
            SelectedTour = selectedTour;
            TourReviewDTO = new TourReviewDTO();
            LoggedUser = user;

            ImagesCollection = new ObservableCollection<String>();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string imagePath in openFileDialog.FileNames)
                {
                    int relativePathStartIndex = imagePath.IndexOf("\\Resources");
                    String relativePath = imagePath.Substring(relativePathStartIndex);
                    ImagesCollection.Add(relativePath);
                }
            }
        }
        private void SaveImages(List<String> images)
        {
            foreach (string relativePath in images)
            {
                _imageRepository.Save(new Model.Image(relativePath, SelectedTour.Id, Enums.ImageType.TourReview));
            }
        }
        private void RemoveImage_Click(object sender, EventArgs e)
        {
            string imageUrl = SelectedImageUrl;
            ImagesCollection.Remove(imageUrl);
        }

        private void SaveRate_Click(object sender, EventArgs e)
        {
            TourReviewDTO.ScheduleId = SelectedTour.Id;
            TourReviewDTO.TouristId = LoggedUser.Id;
            _tourReviewRepository.MakeReview(TourReviewDTO);
            SaveImages(ImagesCollection.ToList());
            this.Close();
        }

        private void CancelRate_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
