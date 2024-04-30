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
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class TourRatingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TourScheduleDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }
        public RelayCommand SelectImageCommand { get; set; }
        public RelayCommand RemoveImageCommand { get; set; }
        public RelayCommand CancleRateCommand { get; set; }
        public RelayCommand SaveRateCommand { get; set; }


        public TourReviewDTO TourReviewDTO { get; set; }
        public ImageItemDTO SelectedImageUrl { get; set; }
        public static ObservableCollection<ImageItemDTO> ImagesCollection { get; set; }
        public TourRatingViewModel(TourScheduleDTO selectedTour, User user) 
        {
            SelectedTour = selectedTour;
            TourReviewDTO = new TourReviewDTO();
            LoggedUser = user;
            SaveRateCommand = new RelayCommand(Execute_SaveRateCommand);
            CancleRateCommand = new RelayCommand(Execute_CancleRateCommand);
            SelectImageCommand = new RelayCommand(Execute_SelectImageCommand);
            RemoveImageCommand = new RelayCommand(Execute_RemoveImageCommand);
            ImagesCollection = new ObservableCollection<ImageItemDTO>();
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
                    BitmapImage imageSource = new BitmapImage(new Uri(imagePath));
                    ImagesCollection.Add(new ImageItemDTO(relativePath, imageSource));
                }
            }
        }
        private void SaveImages()
        {
            foreach (ImageItemDTO imageItem in ImagesCollection)
            {
                ImageService.GetInstance().Save(new Model.Image(imageItem.ImagePath, SelectedTour.Id, Enums.ImageType.TourReview));
            }
        }
        private void Execute_RemoveImageCommand(object param)
        {
            /*   string imageUrl = SelectedImageUrl;
               ImagesCollection.Remove(imageUrl);*/
            var imageToRemove = param as ImageItemDTO;

            ImagesCollection.Remove(imageToRemove);
        }

        private void Execute_SaveRateCommand(object sender)
        {
            TourReviewDTO.ScheduleId = SelectedTour.Id;
            TourReviewDTO.TouristId = LoggedUser.Id;
          
            TourReviewDTO.IsValid = true;
            TourReviewService.GetInstance().MakeReview(TourReviewDTO);

            SaveImages();
            //Window.Close();
        }

        private void Execute_CancleRateCommand(object sender)
        {
            //Window.Close();
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
