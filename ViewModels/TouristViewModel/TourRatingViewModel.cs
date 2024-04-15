using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.TouristView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class TourRatingViewModel
    {
        private readonly ImageRepository _imageRepository;
        private readonly TourReviewService _reviewService = new TourReviewService();
        public TourScheduleDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }
        public RelayCommand SelectImageCommand { get; set; }
        public RelayCommand RemoveImageCommand { get; set; }
        public RelayCommand CancleRateCommand { get; set; }
        public RelayCommand SaveRateCommand { get; set; }


        public TourReviewDTO TourReviewDTO { get; set; }
        public String SelectedImageUrl { get; set; }
        public static ObservableCollection<String> ImagesCollection { get; set; }
        public TourRating Window { get; set; }
        public TourRatingViewModel(TourRating window, TourScheduleDTO selectedTour, ImageRepository imageRepository, User user)
        {
            Window = window;
            _imageRepository = imageRepository;
            SelectedTour = selectedTour;
            TourReviewDTO = new TourReviewDTO();
            LoggedUser = user;

            SaveRateCommand = new RelayCommand(Execute_SaveRateCommand);
            CancleRateCommand = new RelayCommand(Execute_CancleRateCommand);
            SelectImageCommand = new RelayCommand(Execute_SelectImageCommand);
            RemoveImageCommand = new RelayCommand(Execute_RemoveImageCommand);
            ImagesCollection = new ObservableCollection<String>();
        }

        private void Execute_SelectImageCommand(object sender)
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
        public void SaveImages(List<String> images)
        {
            foreach (string relativePath in images)
            {
                _imageRepository.Save(new Model.Image(relativePath, SelectedTour.Id, Enums.ImageType.TourReview));
            }
        }
        private void Execute_RemoveImageCommand(object sender)
        {
            string imageUrl = SelectedImageUrl;
            ImagesCollection.Remove(imageUrl);
        }

        private void Execute_SaveRateCommand(object sender)
        {
            TourReviewDTO.ScheduleId = SelectedTour.Id;
            TourReviewDTO.TouristId = LoggedUser.Id;
            _reviewService.MakeReview(TourReviewDTO);
            SaveImages(ImagesCollection.ToList());
            Window.Close();
        }

        private void Execute_CancleRateCommand(object sender)
        {
            Window.Close();
        }
    }
}
