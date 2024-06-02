using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookingApp.ViewModels
{
    public class RegisterAccommodationVM : INotifyPropertyChanged
    {
        private string name;
        private bool? isApartment;
        private bool? isHouse;
        private bool? isCottage;
        private string maxGuests;
        private string minReservation;
        private string cancelationDays;
        private int selectedLocationIndex;
        private ImageDTO selectedImage;

        public Accommodation accommodation;
        public List<String> _imageRelativePath = new List<String>();
        public ObservableCollection<ImageDTO> AddedImages { get; set; }
        public List<String> Locations { get; set; }
        public int UserId { get; }


        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public bool? IsApartment
        {
            get => isApartment;
            set
            {
                isApartment = value;
                OnPropertyChanged();
            }
        }

        public bool? IsHouse
        {
            get => isHouse;
            set
            {
                isHouse = value;
                OnPropertyChanged();
            }
        }

        public bool? IsCottage
        {
            get => isCottage;
            set
            {
                isCottage = value;
                OnPropertyChanged();
            }
        }


        public string MaxGuests
        {
            get => maxGuests;
            set
            {
                maxGuests = value;
                OnPropertyChanged();
            }
        }



        public string MinReservation
        {
            get => minReservation;
            set
            {
                minReservation = value;
                OnPropertyChanged();
            }
        }



        public string CancelationDays
        {
            get => cancelationDays;
            set
            {
                cancelationDays = value;
                OnPropertyChanged();
            }
        }

        public int SelectedLocationIndex
        {
            get => selectedLocationIndex;
            set
            {
                selectedLocationIndex = value;
                OnPropertyChanged();
            }
        }

        public ImageDTO SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand AddImagesCommand { get; }
        public ICommand RemoveImageCommand { get; }
        public ICommand PickAccommodationCommand {  get; }
        public ICommand PickHouseCommand { get; }
        public ICommand PickCottageCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;



        public RegisterAccommodationVM(int userId)
        {

            IsApartment = true;
            this.UserId = userId;
            this.Locations = new List<String>();
            this.AddedImages = new ObservableCollection<ImageDTO>();
            CloseCommand = new RelayCommand(CloseWindow);
            RegisterCommand = new RelayCommand(Register);
            AddImagesCommand = new RelayCommand(AddImages);
            RemoveImageCommand = new RelayCommand(RemoveImage, CanRemoveImage);
            PickAccommodationCommand = new RelayCommand(PickAccommodation);
            PickCottageCommand = new RelayCommand(PickCottage);
            PickHouseCommand = new RelayCommand(PickHouse);

            AddLocations();
        }

        private void PickCottage(object parameter)
        {
            IsCottage = true;
        }

        private void PickHouse(object parameter) 
        {
            IsHouse = true;
        }
        private void PickAccommodation(object parameter)
        {
            IsApartment = true;
        }
        private void AddLocations()
        {
            foreach (Location location in LocationService.GetInstance().GetAll())
            {
                Locations.Add(location.City + " , " + location.State);
            }
        }


        public void ConvertImagePath(List<String> _imagePath)
        {
            foreach (String imgPath in _imagePath)
            {
                int relativePathStartIndex = imgPath.IndexOf("\\Resources");
                String relativePath = imgPath.Substring(relativePathStartIndex);
                _imageRelativePath.Add(relativePath);
            }
        }

        public void AddConvertedImages()
        {
            foreach (string imgPath in _imageRelativePath)
            {
                ImageDTO image = new ImageDTO(imgPath);
                AddedImages.Add(image);
            }
        }

        private void AddImages(object parameter)
        {
            List<String> _imagePath = new List<String>();
            _imageRelativePath.Clear();
            AddedImages.Clear();
            //so it doesnt duplicate

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (String imgPath in openFileDialog.FileNames)
                {
                    _imagePath.Add(imgPath);
                }
            }


            ConvertImagePath(_imagePath);
            AddConvertedImages();


        }


        public void Register(object parameter)
        {
            int intMaxGuests;
            int intMinReservation;
            int intCancelationDays;

            if (Name == null)
                Name = "Name";


            if (MinReservation == null || !int.TryParse(MinReservation, out intMinReservation))
            {
                MinReservation = "5";
                intMinReservation = 5;
            }

            if (MaxGuests == null || !int.TryParse(MaxGuests, out intMaxGuests))
            {
                MaxGuests = "5";
                intMaxGuests = 5;
            }

            if (CancelationDays == null || !int.TryParse(CancelationDays,out intCancelationDays))
            {
                CancelationDays = "5";
                intCancelationDays = 5;
            }

            
            
                if (MessageBox.Show("Confirm registration?", "Register", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    Enums.AccommodationType type = AccommodationService.GetInstance().GetType(IsApartment, IsCottage);
                    
                    Location location = LocationService.GetInstance().GetById(SelectedLocationIndex+1);


                    accommodation = new Accommodation(Name, type, intMaxGuests,intMinReservation,intCancelationDays, location.Id, UserId);
                    ImageService.GetInstance().SaveImages(AccommodationService.GetInstance().Save(accommodation).Id, _imageRelativePath);

                    CloseWindow(Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));

                }
            

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        internal void RemoveImage(object parameter)
        {

            ImageDTO image = parameter as ImageDTO;
            AddedImages.Remove(image);
            _imageRelativePath.Clear();
            foreach (ImageDTO img in AddedImages)
            {
                _imageRelativePath.Add(img.LeftPath);
            }
        }

        private bool CanRemoveImage(object parameter)
        {
            return SelectedImage != null;
        }

        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }

    }
}
