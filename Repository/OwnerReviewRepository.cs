using System;
using BookingApp.Observer;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.Repository
{
    public class OwnerReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/owner_reviews.csv";

        private readonly Serializer<OwnerReview> _serializer;
        private readonly List<IObserver> _observers;

        private List<OwnerReview> _reviews;
        public Subject subject;


        public OwnerReviewRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<OwnerReview>();
            _reviews = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<OwnerReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public OwnerReview Save(OwnerReview ownerReview)
        {

            _reviews = _serializer.FromCSV(FilePath);
            _reviews.Add(ownerReview);
            _serializer.ToCSV(FilePath, _reviews);
            subject.NotifyObservers();
            return ownerReview;

        }

        public void Delete(OwnerReview ownerReview)
        {
            _reviews = _serializer.FromCSV(FilePath);
            OwnerReview found = _reviews.Find(c => c.ReservationId == ownerReview.ReservationId);
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
            foreach (OwnerReview review in _reviews)
            {
                if (review.ReservationId == reservationId)
                    return true;
            }

            return false;
        }
        public OwnerReview Get(int reservationId)
        {
            _reviews = _serializer.FromCSV(FilePath);
            return _reviews.Find(r => r.ReservationId == reservationId);
        }

    }
}
