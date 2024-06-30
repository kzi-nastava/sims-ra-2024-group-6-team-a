using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookingApp.ApplicationServices
{
    public class ImageService
    {
        private IImageRepository _imageRepository;

        public ImageService()
        {
            _imageRepository = new ImageRepository();
        }
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public static ImageService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ImageService>();
        }

        

        public string AddMainAccommodationImage(Accommodation a)
        {
            Model.Image image = new Model.Image();
            foreach (Model.Image i in _imageRepository.GetByEntity(a.Id, Enums.ImageType.Accommodation))
            {
                image = i;
                return image.Path;
            }
            return null;
        }

        public List<Model.Image> GetImagesForAccommodaton(int id)
        {
            List<Model.Image> images = new List<Model.Image>();

            foreach (Model.Image i in _imageRepository.GetByEntity(id, Enums.ImageType.Accommodation))
            {
                images.Add(i);
            }

            return images;
        }

        public void SaveImages(int accommodationId,List<String> _imageRelativePath)
        {
            foreach (String relativePath in _imageRelativePath)
            {
                _imageRepository.Save(new Model.Image(relativePath, accommodationId, Enums.ImageType.Accommodation));
            }
        }

        public List<String> GetAllImages(int tourId)
        {
            List<string> images = new List<string>();
            List<Model.Image> imagePaths = ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour);

            foreach (Model.Image imagePath in imagePaths)
            {
                images.Add(imagePath.Path);
            }

            return images;
        }

        public Image Save(Image Image)
        {
           return _imageRepository.Save(Image); 
        }

        public List<Image> GetByEntity(int id, Enums.ImageType type)
        {
            return _imageRepository.GetByEntity(id, type);
        }

        public List<Image> GetAll()
        {
            return _imageRepository.GetAll();
        }
        public void Delete(Image Image)
        {
            _imageRepository.Delete(Image);
        }

        public void SaveAllImages(ObservableCollection<ImageItemDTO> images,int tourId) 
        {
            foreach (ImageItemDTO imageItem in images)
            {
                ImageService.GetInstance().Save(new Model.Image(imageItem.ImagePath, tourId, Enums.ImageType.Tour));
            }
        }
    }
}
