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
    public class ForumsRepository: IForumsRepository
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";
        private readonly Serializer<Forums> _serializer;
        private List<Forums> _forums;
        public Subject subject;

        public ForumsRepository()
        {
            _serializer = new Serializer<Forums>();
            _forums = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public int NextId()
        {
            _forums = _serializer.FromCSV(FilePath);
            if (_forums.Count < 1)
            {
                return 1;
            }
            return _forums.Max(c => c.Id) + 1;
        }
        public Forums Save(Forums forum)
        {
            forum.Id = NextId();  
            _forums = _serializer.FromCSV(FilePath);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            subject.NotifyObservers();
            return forum;
        }
        public void Delete(Forums forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forums found = _forums.Find(c => c.Id == forum.Id);
            if (found != null)
            {
                _forums.Remove(found);
            }

            _serializer.ToCSV(FilePath, _forums);
            subject.NotifyObservers();
        }
        public Forums Update(Forums forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forums current = _forums.Find(c => c.Id == forum.Id);
            int index = _forums.IndexOf(current);
            _forums.Remove(current);
            _forums.Insert(index, forum);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }
        public Forums Get(int id)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.Find(c => c.Id == id);
        }
        public List<Forums> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
