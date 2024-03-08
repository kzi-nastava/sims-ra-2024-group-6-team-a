using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Resources;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ImageRepository
    {
        private const string FilePath = "../../../Resources/Data/images.csv";

        private readonly Serializer<Image> _serializer;

        private List<Image> _images;
        public Subject subject;

        public ImageRepository()
        {
            _serializer = new Serializer<Image>();
            _images = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Image> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Image Save(Image Image)
        {
            Image.Id = NextId();
            _images = _serializer.FromCSV(FilePath);
            _images.Add(Image);
            _serializer.ToCSV(FilePath, _images);
            subject.NotifyObservers();
            return Image;

        }

        public int NextId()
        {
            _images = _serializer.FromCSV(FilePath);
            if (_images.Count < 1)
            {
                return 1;
            }
            return _images.Max(c => c.Id) + 1;
        }

        public void Delete(Image Image)
        {
            _images = _serializer.FromCSV(FilePath);
            Image found = _images.Find(c => c.Id == Image.Id);
            if (found != null)
            {
                _images.Remove(found);
            }

            _serializer.ToCSV(FilePath, _images);
            subject.NotifyObservers();
        }

        public List<Image> GetByEntity(int id,Enums.ImageType type)
        {
            _images = _serializer.FromCSV(FilePath);
            return _images.FindAll(c => c.EntityId == id && c.Type == type);
        }

    }
}
