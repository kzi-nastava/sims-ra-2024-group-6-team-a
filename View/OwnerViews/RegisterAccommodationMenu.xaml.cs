using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BookingApp.Repository;
using System.Runtime.CompilerServices;
using BookingApp.Resources;
using Microsoft.Win32;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationMenu.xaml
    /// </summary>
    public partial class RegisterAccommodationMenu : Window,INotifyPropertyChanged
    {
        public Accommodation accommodation;
        public AccommodationRepository accommodationRepository;
        public LocationRepository locationRepository;
        public ImageRepository imageRepository;
        List<String> _imageRelativePath = new List<String>();
        public int userId;

        public event PropertyChangedEventHandler? PropertyChanged;
        public RegisterAccommodationMenu(AccommodationRepository accommodationRepository,LocationRepository locationRepository,ImageRepository imageRepository, int userId)
        {
            InitializeComponent();
            DataContext = this;

            this.locationRepository = locationRepository;
            this.accommodationRepository = accommodationRepository;
            this.imageRepository = imageRepository;

            apt.IsChecked = true;
            this.userId = userId; 
        }

        

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            int guestsParsed;
            int reservationParsed;
            int cancelParsed;

            if (State.Text != "" && City.Text != "" && Name.Text != "" && int.TryParse(MaxGuests.Text,out guestsParsed) && int.TryParse(MinReservation.Text,out reservationParsed) && int.TryParse(CancelDays.Text,out cancelParsed))
             {
                Enums.AccommodationType type = GetType();

                Location location = new Location(State.Text, City.Text);
                location = locationRepository.Save(location);
                //we could add a check if location exists


                accommodation = new Accommodation(Name.Text, type, int.Parse(MaxGuests.Text), int.Parse(MinReservation.Text), int.Parse(CancelDays.Text), location.Id, userId);
                SaveImages(accommodationRepository.Save(accommodation).Id);

                Close();
            }
            else
            {
                MessageBox.Show("Please enter every field correctly.","Information Missing",MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SaveImages(int accommodationId)
        {
            foreach (String relativePath in _imageRelativePath)
            {
                imageRepository.Save(new Model.Image(relativePath, accommodationId, Enums.ImageType.Accommodation));
            }
        }

        private Enums.AccommodationType GetType()
        {
            if (apt.IsChecked == true)
            {
                return Enums.AccommodationType.Apartment;
            }
            else if (cottage.IsChecked == true)
            {
                return Enums.AccommodationType.Cottage;
            }
            else
            {
                return Enums.AccommodationType.House;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void AddImageClick(object sender, RoutedEventArgs e)
        {
            List<String> _imagePath = new List<String>();
            _imageRelativePath.Clear();
            //so it doesnt duplicate
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Multiselect = true;

            if(openFileDialog.ShowDialog() == true) 
            {
                foreach(String imgPath in openFileDialog.FileNames)
                { 
                    _imagePath.Add(imgPath);
                }
            }

            if(_imagePath.Count == 1) 
            {
                ImageCountText.Text = "Added 1 image";
            }
            else
            {
                ImageCountText.Text = "Added " + _imagePath.Count + " images";
            }

            ConvertImagePath(_imagePath);

               
        }

        private void ConvertImagePath(List<String> _imagePath)
        {
            foreach (String imgPath in _imagePath)
            {
                int relativePathStartIndex = imgPath.IndexOf("\\Resources");
                String relativePath = imgPath.Substring(relativePathStartIndex);
                _imageRelativePath.Add(relativePath);
            }
        }
    }
}
