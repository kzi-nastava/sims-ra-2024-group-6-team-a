using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationBlogRepository : IAccommodationBlogRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_blogs.csv";

        private readonly Serializer<AccommodationBlog> _serializer;
        private readonly List<IObserver> _observers;

        private List<AccommodationBlog> _blogs;
        public Subject subject;

        public AccommodationBlogRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<AccommodationBlog>();
            _blogs = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<AccommodationBlog> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationBlog Save(AccommodationBlog AccommodationBlog)
        {
            AccommodationBlog.Id = NextId();
            _blogs = _serializer.FromCSV(FilePath);
            _blogs.Add(AccommodationBlog);
            _serializer.ToCSV(FilePath, _blogs);
            subject.NotifyObservers();
            return AccommodationBlog;

        }

        public int NextId()
        {
            _blogs = _serializer.FromCSV(FilePath);
            if (_blogs.Count < 1)
            {
                return 1;
            }
            return _blogs.Max(c => c.Id) + 1;
        }

        public void Delete(AccommodationBlog AccommodationBlog)
        {
            _blogs = _serializer.FromCSV(FilePath);
            AccommodationBlog found = _blogs.Find(c => c.Id == AccommodationBlog.Id);
            if (found != null)
            {
                _blogs.Remove(found);
            }

            _serializer.ToCSV(FilePath, _blogs);
            subject.NotifyObservers();
        }

        public void Delete(int id)
        {
            _blogs = _serializer.FromCSV(FilePath);
            AccommodationBlog founded = _blogs.Find(c => c.Id == id);
            _blogs.Remove(founded);
            _serializer.ToCSV(FilePath, _blogs);
            subject.NotifyObservers();
        }

        public AccommodationBlog GetById(int id)
        {
            _blogs = _serializer.FromCSV(FilePath);
            return _blogs.Find(c => c.Id == id);
        }
    }
}
