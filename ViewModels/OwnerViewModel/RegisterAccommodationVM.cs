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



        public List<String> _imageRelativePath = new List<String>();

        public ObservableCollection<ImageDTO> AddedImages { get; set; }
        public List<String> locations {  get; set; }
        public int userId;

        public event PropertyChangedEventHandler? PropertyChanged;


        public RegisterAccommodationVM( int userId)
        {


            this.userId = userId;
            this.locations = new List<String>();
            this.AddedImages = new ObservableCollection<ImageDTO>();
            AddLocations();
        }

        private void AddLocations()
        {
            foreach (Location location in LocationService.GetInstance().GetAll()) 
            {
                locations.Add(location.City + " , " + location.State);
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
            foreach(string imgPath in _imageRelativePath)
            {
                ImageDTO image = new ImageDTO(imgPath);
                AddedImages.Add(image);
            }
        }


        public void Register(bool? aptChecked, bool? cottageChecked,int locationId,string name,string maxguests,string minres,string canceldays)
        {


            Enums.AccommodationType type = AccommodationService.GetInstance().GetType(aptChecked,cottageChecked);

            Location location = LocationService.GetInstance().GetById(locationId);


            accommodation = new Accommodation(name, type, int.Parse(maxguests), int.Parse(minres), int.Parse(canceldays), location.Id, userId);
            ImageService.GetInstance().SaveImages(AccommodationService.GetInstance().Save(accommodation).Id,_imageRelativePath);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
