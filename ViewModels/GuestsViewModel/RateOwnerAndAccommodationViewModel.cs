using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuestViews;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class RateOwnerAndAccommodationViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<ImageItemDTO> ImagesCollectionn { get; set; }

        public String SelectedImageUrl { get; set; }
        public RelayCommand SendReviewCommand { get; set; }
        public RelayCommand SelectImageCommand { get; set; }
        public RelayCommand RemoveImageCommand { get; set; }
        public ReservationGuestDTO Reservation { get; set; }
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public RateOwnerAndAccommodationView ReservationView { get; set; }
        public string OwnerName;
        public RateOwnerAndAccommodationViewModel(Guest guest, ReservationGuestDTO SelectedReservation, RateOwnerAndAccommodationView reservationView, NavigationService navigation)
        {
            Guest = guest;
            NavService = navigation;
            Reservation = SelectedReservation;
            ReservationView = reservationView;
            ImagesCollectionn = new ObservableCollection<ImageItemDTO>();
            SelectImageCommand = new RelayCommand(Execute_SelectImageCommand);
            RemoveImageCommand = new RelayCommand(Execute_RemoveImageCommand);

            OwnerName = OwnerService.GetInstance().GetByOwnersId(AccommodationService.GetInstance().GetByReservationId(SelectedReservation.AccommodationId).OwnerId).Name;
            SendReviewCommand = new RelayCommand(Execute_SendReviewCommand);
        }
       
        public string CheckUrgency() {
            string urgencyLevel ="";
            if((bool)ReservationView.Zero.IsChecked ) urgencyLevel="Level0";
            if((bool)ReservationView.One.IsChecked ) urgencyLevel="Level1";
            if((bool)ReservationView.Two.IsChecked ) urgencyLevel="Level2";
            if((bool)ReservationView.Three.IsChecked ) urgencyLevel="Level3";
            if((bool)ReservationView.Four.IsChecked ) urgencyLevel="Level4";
            if((bool)ReservationView.Five.IsChecked ) urgencyLevel="Level5";
            return urgencyLevel;
        }
        public void Execute_SendReviewCommand(object obj)
        {
            string SelectedUrgency = CheckUrgency();
            int cleanliness = (int) ReservationView.CleanlinessSlider.Value;
            int correctness = (int) ReservationView.CorrectnessSlider.Value;
            string AdditionalComment = ReservationView.AdditionalComment.Text;
            string StateComment = ReservationView.StateComment.Text;
            if (AdditionalComment != "") { 
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to send this review?\nCleanliness  : " + cleanliness.ToString() + "\n" + "Correctness : " + correctness + "\nComment : " + AdditionalComment, "Rate " + OwnerName + " and " + Reservation.AccommodationName, MessageBoxButton.YesNo);
                switch (odgovor) { 
                    case MessageBoxResult.Yes:
                        OwnerReview ownerReview = new OwnerReview(Reservation.Id, cleanliness, correctness, AdditionalComment, SelectedUrgency, StateComment);
                         foreach (ImageItemDTO imageItem in ImagesCollectionn)
                            ImageService.GetInstance().Save(new Model.Image(imageItem.ImagePath, Reservation.Id, Enums.ImageType.OwnersReviews));
                        OwnerReviewService.GetInstance().Save(ownerReview);
                        NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
                        break;
                    default:
                        break;
                }
            } else MessageBox.Show("The comment are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private string numberOfPhoto;
        public string NumberOfPhoto
        {
            get => numberOfPhoto;
            set { 
                if (value != numberOfPhoto) { 
                    numberOfPhoto = value;
                    OnPropertyChanged("NumberOfPhoto");
                }
            }
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
                    ImagesCollectionn.Add(new ImageItemDTO(relativePath, imageSource));
                }
            }
            if (ImagesCollectionn.Count == 1)
            {
                NumberOfPhoto = "Added " + ImagesCollectionn.Count + " image";
            }
            else if (ImagesCollectionn.Count > 1) {
                NumberOfPhoto = "Added " + ImagesCollectionn.Count + " images";
            }
        }
        private void Execute_RemoveImageCommand(object param)
        {
            var imageToRemove = param as ImageItemDTO;
            ImagesCollectionn.Remove(imageToRemove);
            if (ImagesCollectionn.Count == 0) {
                NumberOfPhoto = "";
            }
            else if (ImagesCollectionn.Count == 1)
            {
                NumberOfPhoto = "Added " + ImagesCollectionn.Count + " image";
            } else NumberOfPhoto = "Added " + ImagesCollectionn.Count + " images";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
