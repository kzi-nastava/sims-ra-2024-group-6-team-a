using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuestReviewRepository : IGuestReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/guest_reviews.csv";

        private readonly Serializer<GuestReview> _serializer;
        private readonly List<IObserver> _observers;

        private List<GuestReview> _reviews;
        public Subject subject;

        public GuestReviewRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<GuestReview>();
            _reviews = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<GuestReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuestReview Save(GuestReview GuestReview)
        {
            
            _reviews = _serializer.FromCSV(FilePath);
            _reviews.Add(GuestReview);
            _serializer.ToCSV(FilePath, _reviews);
            subject.NotifyObservers();
            return GuestReview;

        }

  
        public void Delete(GuestReview GuestReview)
        {
            _reviews = _serializer.FromCSV(FilePath);
            GuestReview found = _reviews.Find(c => c.ReservationId == GuestReview.ReservationId);
            if (found != null)
            {
                _reviews.Remove(found);
            }

            _serializer.ToCSV(FilePath, _reviews);
            subject.NotifyObservers();
        }

        
        public bool DoesGradeExist(int reservationId)
        {
            _reviews = _serializer.FromCSV(FilePath);
            foreach(GuestReview g in _reviews) 
            {
                if(g.ReservationId == reservationId)
                    return true;
            }

            return false;
        }
        public GuestReview GetByReservationId(int id)
        {
            _reviews  = _serializer.FromCSV(FilePath);
            return _reviews.Find(c => c.ReservationId == id);
        }

        public GuestReview Get(int reservationId) 
        {
            _reviews = _serializer.FromCSV(FilePath);
            return _reviews.Find(r => r.ReservationId == reservationId);
        }
    }
}
