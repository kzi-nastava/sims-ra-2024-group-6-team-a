using BookingApp.Domain.Model;
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
    public class GuideRepository : IGuideRepository
    {
        private const string FilePath = "../../../Resources/Data/guides.csv";
        private readonly Serializer<Guide> _serializer;
        private List<Guide> _guides;
        public Subject subject;


        public GuideRepository()
        {
            _serializer = new Serializer<Guide>();
            _guides = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Guide> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Guide Save(Guide guide)
        {
            guide.Id = NextId();
            _guides = _serializer.FromCSV(FilePath);
            _guides.Add(guide);
            _serializer.ToCSV(FilePath, _guides);

            subject.NotifyObservers();
            return guide;
        }

        public int NextId()
        {
            _guides = _serializer.FromCSV(FilePath);
            if (_guides.Count < 1)
            {
                return 1;
            }
            return _guides.Max(x => x.Id) + 1;
        }

        public void Delete(Guide guide)
        {
            _guides = _serializer.FromCSV(FilePath);
            Guide found = _guides.Find(x => x.Id == guide.Id);
            _guides.Remove(found);
            _serializer.ToCSV(FilePath, _guides);
            subject.NotifyObservers();
        }

        public Guide Update(Guide guide)
        {
            _guides = _serializer.FromCSV(FilePath);
            Guide current = _guides.Find(x => x.Id == guide.Id);
            int index = _guides.IndexOf(current);
            _guides.Remove(current);
            _guides.Insert(index, guide);
            _serializer.ToCSV(FilePath, _guides);
            subject.NotifyObservers();
            return guide;
        }

        public Guide GetById(int id)
        {
            return _guides.Find(c => c.Id == id);
        }

        
    }

}
