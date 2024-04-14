using BookingApp.Domain.Model;
using BookingApp.DTOs;
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
    public class TourReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreviews.csv";

        private readonly Serializer<TourReview> _serializer;
        private readonly List<IObserver> _observers;

        private List<TourReview> _reviews;
        public Subject subject;

        public TourReviewRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<TourReview>();
            _reviews = _serializer.FromCSV(FilePath);
            subject = new Subject();

        }

        public List<TourReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourReview Save(TourReview review)
        {
            review.Id = NextId();
            _reviews = _serializer.FromCSV(FilePath);
            _reviews.Add(review);
            _serializer.ToCSV(FilePath, _reviews);
            subject.NotifyObservers();

            return review;
        }

        public int NextId()
        {
            _reviews = _serializer.FromCSV(FilePath);
            if (_reviews.Count < 1)
            {
                return 1;
            }
            return _reviews.Max(x => x.Id) + 1;
        }

        public void Delete(TourReview review)
        {
            _reviews = _serializer.FromCSV(FilePath);
            TourReview found = _reviews.Find(x => x.Id == review.Id);
            _reviews.Remove(found);
            _serializer.ToCSV(FilePath, _reviews);
            subject.NotifyObservers();
        }

        public TourReview Update(TourReview review)
        {
            _reviews = _serializer.FromCSV(FilePath);
            TourReview current = _reviews.Find(x => x.Id == review.Id);
            int index = _reviews.IndexOf(current);
            _reviews.Remove(current);
            _reviews.Insert(index, review);
            _serializer.ToCSV(FilePath, _reviews);
            subject.NotifyObservers();
            return review;
        }

        public void MakeReview(TourReviewDTO tourReviewDTO) //provjeri da li si prethodno ocijenila
        {
            foreach(TourReview review in GetAll())
            {
                if(review.ScheduleId == tourReviewDTO.ScheduleId && review.TouristId == tourReviewDTO.TouristId)
                {
                    return;
                }
            }
            TourReview tourReview = new TourReview(tourReviewDTO.ScheduleId, tourReviewDTO.GuideKnowledgeGrade, tourReviewDTO.GuideLanguageGrade, tourReviewDTO.TourAttractionsGrade, tourReviewDTO.Impression, tourReviewDTO.TouristId);
            Save(tourReview);
        }

        public List <TourReview> GetAllReviewsByScheduleId(int tourScheduleId)
        {
            return _reviews.Where(c => c.ScheduleId == tourScheduleId).ToList();

        }
    }
}
