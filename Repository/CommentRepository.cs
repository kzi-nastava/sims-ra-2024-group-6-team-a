using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private const string FilePath = "../../../Resources/Data/comments.csv";

        private readonly Serializer<Comment> _serializer;
        private readonly List<IObserver> _observers;

        public Subject subject;

        private List<Comment> _comments;

        public CommentRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<Comment>();
            _comments = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Comment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Comment Save(Comment comment)
        {
            comment.Id = NextId();
            _comments = _serializer.FromCSV(FilePath);
            _comments.Add(comment);
            _serializer.ToCSV(FilePath, _comments);
            subject.NotifyObservers();
            return comment;
        }

        public int NextId()
        {
            _comments = _serializer.FromCSV(FilePath);
            if (_comments.Count < 1)
            {
                return 1;
            }
            return _comments.Max(c => c.Id) + 1;
        }

        public void Delete(Comment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            Comment founded = _comments.Find(c => c.Id == comment.Id);
            _comments.Remove(founded);
            _serializer.ToCSV(FilePath, _comments);
            subject.NotifyObservers();
        }

        public Comment Update(Comment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            Comment current = _comments.Find(c => c.Id == comment.Id);
            int index = _comments.IndexOf(current);
            _comments.Remove(current);
            _comments.Insert(index, comment);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _comments);
            subject.NotifyObservers();
            return comment;
        }

        public List<Comment> GetByBlog(AccommodationBlog blog)
        {
            _comments = _serializer.FromCSV(FilePath);
            return _comments.FindAll(c => c.BlogId == blog.Id);
        }
    }
}
