using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class ImageService
    {
        private IImageRepository imageRepository {  get; set; }

        public ImageService()
        {
            imageRepository = new ImageRepository();
        }

        public static ImageService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ImageService>();
        }

        public string AddMainAccommodationImage(Accommodation a)
        {
            Model.Image image = new Model.Image();
            foreach (Model.Image i in imageRepository.GetByEntity(a.Id, Enums.ImageType.Accommodation))
            {
                image = i;
                return image.Path;
            }
            return null;
        }

        public List<Model.Image> GetImagesForAccommodaton(int id)
        {
            List<Model.Image> images = new List<Model.Image>();

            foreach (Model.Image i in imageRepository.GetByEntity(id, Enums.ImageType.Accommodation))
            {
                images.Add(i);
            }

            return images;
        }

        public void SaveImages(int accommodationId,List<String> _imageRelativePath)
        {
            foreach (String relativePath in _imageRelativePath)
            {
                imageRepository.Save(new Model.Image(relativePath, accommodationId, Enums.ImageType.Accommodation));
            }
        }
    }
}
