using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.ViewModels
{
    public class RegisterAccommodationVM : INotifyPropertyChanged
    {

        public Accommodation accommodation;
        public AccommodationRepository accommodationRepository;
        public LocationRepository locationRepository;
        public ImageRepository imageRepository;
        public List<String> _imageRelativePath = new List<String>();
        public List<String> locations {  get; set; }
        public int userId;

        public event PropertyChangedEventHandler? PropertyChanged;


        public RegisterAccommodationVM(AccommodationRepository accommodationRepository, LocationRepository locationRepository, ImageRepository imageRepository, int userId)
        {

            this.locationRepository = locationRepository;
            this.accommodationRepository = accommodationRepository;
            this.imageRepository = imageRepository;
            this.userId = userId;
            this.locations = new List<String>();
            AddLocations();
        }

        private void AddLocations()
        {
            foreach (Location location in this.locationRepository.GetAll()) 
            {
                locations.Add(location.City + " , " + location.State);
            }
        }

        private void SaveImages(int accommodationId)
        {
            foreach (String relativePath in _imageRelativePath)
            {
                imageRepository.Save(new Model.Image(relativePath, accommodationId, Enums.ImageType.Accommodation));
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


       


        public void Register(bool? aptChecked, bool? cottageChecked,int locationId,string name,string maxguests,string minres,string canceldays)
        {


            Enums.AccommodationType type = GetType(aptChecked,cottageChecked);

            Location location = locationRepository.GetById(locationId);


            accommodation = new Accommodation(name, type, int.Parse(maxguests), int.Parse(minres), int.Parse(canceldays), location.Id, userId);
            SaveImages(accommodationRepository.Save(accommodation).Id);
        }

        private Enums.AccommodationType GetType(bool? aptChecked,bool? cottageChecked)
        {
            if (aptChecked == true)
            {
                return Enums.AccommodationType.Apartment;
            }
            else if (cottageChecked == true)
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
    }
}
