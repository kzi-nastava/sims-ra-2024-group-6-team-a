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
            Enums.AccommodationType type;
            if(apt.IsChecked == true)
            {
                type = Enums.AccommodationType.Apartment;
            }
            else if(cottage.IsChecked == true)
            {
                type = Enums.AccommodationType.Cottage; 
            }
            else
            {
                type = Enums.AccommodationType.House;
            }

            Location location = new Location(State.Text,City.Text);
            location = locationRepository.Save(location);
            accommodation = new Accommodation(Name.Text,type,int.Parse(MaxGuests.Text),int.Parse(MinReservation.Text),int.Parse(CancelDays.Text),location.Id,userId);
            int accommodationId = accommodationRepository.Save(accommodation).Id;
            foreach (String relativePath in _imageRelativePath)
            {
                imageRepository.Save(new Model.Image(relativePath,accommodationId, Enums.ImageType.Accommodation));
            }
            Close();



        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void AddImageClick(object sender, RoutedEventArgs e)
        {
            List<String> _imagePath = new List<String>();
            _imageRelativePath.Clear();
            
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

            foreach (String imgPath in _imagePath)
            {
                int relativePathStartIndex = imgPath.IndexOf("\\Resources");
                String relativePath = imgPath.Substring(relativePathStartIndex);
                _imageRelativePath.Add(relativePath);
            }
               
        }
    }
}
