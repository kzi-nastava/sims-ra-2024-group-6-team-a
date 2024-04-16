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
using System.Windows.Navigation;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class RateOwnerAndAccommodationViewModel : INotifyPropertyChanged
    {
        public String SelectedImageUrl { get; set; }
        public static ObservableCollection<String> ImagesCollection { get; set; }
        public RelayCommand AddPhotoComand { get; set; }
        public RelayCommand SendReviewCommand { get; set; }
        public RelayCommand RemovePhotoComand { get; set; }
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
            ImagesCollection = new ObservableCollection<string>();
            OwnerName = OwnerService.GetInstance().GetByOwnersId(AccommodationService.GetInstance().GetByReservationId(SelectedReservation.AccommodationId).OwnerId).Name;
            SendReviewCommand = new RelayCommand(Execute_SendReviewCommand);
            AddPhotoComand = new RelayCommand(Execute_AddPhotoCommand);
            RemovePhotoComand = new RelayCommand(Execute_RemovePhotoComand);
            HasntPhotoVisibility = Visibility.Visible;
            HasPhotoVisibility = Visibility.Collapsed;
        }
        public void Execute_SendReviewCommand(object obj)
        {
            int cleanliness = (int) ReservationView.CleanlinessSlider.Value;
            int correctness = (int) ReservationView.CorrectnessSlider.Value;
            string comment = ReservationView.AdditionalComment.Text;
            if (comment != "") { 
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to send this review?\nCleanliness  : " + cleanliness.ToString() + "\n" + "Correctness : " + correctness + "\nComment : " + comment, "Rate " + OwnerName + " and " + Reservation.AccommodationName, MessageBoxButton.YesNo);
                switch (odgovor) { 
                    case MessageBoxResult.Yes:
                        OwnerReview ownerReview = new OwnerReview(Reservation.Id, cleanliness, correctness, comment);
                        foreach (String relativePath in ImagesCollection)
                            ImageService.GetInstance().Save(new Model.Image(relativePath, Reservation.Id, Enums.ImageType.OwnersReviews));
                        OwnerReviewService.GetInstance().Save(ownerReview);
                        NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
                        break;
                    default:
                        break;
                }
            } else MessageBox.Show("The comment are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        public void Execute_RemovePhotoComand(object obj)
        {
            if (SelectedImageUrl != null) { 
                ImagesCollection.Remove(SelectedImageUrl);
                CheckPhotoNumber();
                if (ImagesCollection.Count == 0) NumberOfPhoto = "";
                else NumberOfPhoto = "Added " + ImagesCollection.Count + " images";
            } else MessageBox.Show("You must select a photo!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
        private Visibility hasntPhotoVisibiliti;
        public Visibility HasntPhotoVisibility
        {
            get { return hasntPhotoVisibiliti; }
            set { 
                if (hasntPhotoVisibiliti != value) { 
                    hasntPhotoVisibiliti = value;
                    OnPropertyChanged();
                }
            }
        }
        private Visibility hasPhotoVisibiliti;
        public Visibility HasPhotoVisibility
        {
            get { return hasPhotoVisibiliti; }
            set { 
                if (hasPhotoVisibiliti != value) { 
                    hasPhotoVisibiliti = value;
                    OnPropertyChanged();
                }
            }
        }
        private void CheckPhotoNumber()
        {
            if (ImagesCollection.Count == 0) { 
                HasntPhotoVisibility = Visibility.Visible;
                HasPhotoVisibility = Visibility.Collapsed;
            }else{
                HasntPhotoVisibility = Visibility.Collapsed;
                HasPhotoVisibility = Visibility.Visible;
            }
        }
        public void Execute_AddPhotoCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true) { 
                foreach (String imgPath in openFileDialog.FileNames) { 
                    int relativePathStartIndex = imgPath.IndexOf("\\Resources");
                    String relativePath = imgPath.Substring(relativePathStartIndex);
                    ImagesCollection.Add(relativePath);
                }
            }
            CheckPhotoNumber();
            if (ImagesCollection.Count != 0) NumberOfPhoto = "Added " + ImagesCollection.Count + " images";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
