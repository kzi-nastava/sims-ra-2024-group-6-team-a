using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ForumsCommentRepository : IForumsCommentRepository
    {
        private const string FilePath = "../../../Resources/Data/forumsComments.csv";
        private readonly Serializer<ForumsComment> _serializer;
        private List<ForumsComment> _forums;
        public Subject subject;

        public ForumsCommentRepository()
        {
            _serializer = new Serializer<ForumsComment>();
            _forums = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

      
        public ForumsComment Save(ForumsComment forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            subject.NotifyObservers();
            return forum;
        }
        public void Delete(ForumsComment forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            ForumsComment found = _forums.Find(c => c.ForumId == forum.ForumId);
            if (found != null)
            {
                _forums.Remove(found);
            }

            _serializer.ToCSV(FilePath, _forums);
            subject.NotifyObservers();
        }
        public ForumsComment Update(ForumsComment forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            ForumsComment current = _forums.Find(c => c.ForumId == forum.ForumId);
            int index = _forums.IndexOf(current);
            _forums.Remove(current);
            _forums.Insert(index, forum);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        } 
        public ForumsComment Get(int ForumId)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.Find(c => c.ForumId == ForumId);
        }
        public List<ForumsComment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
