using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class TourReviewService
    {
        private ITourReviewRepository _reviewRepository;

        public TourReviewService()
        {
            _reviewRepository = new TourReviewRepository();
        }
        public List<TourReview> GetAll()
        {
            return _reviewRepository.GetAll();  
        }

        public TourReview Save(TourReview review)
        {
            return _reviewRepository.Save(review);
        }


        public void MakeReview(TourReviewDTO tourReviewDTO)
        {
            foreach (TourReview review in GetAll())
            {
                if (review.ScheduleId == tourReviewDTO.ScheduleId && review.TouristId == tourReviewDTO.TouristId)
                {
                    return;
                }
            }
            TourReview tourReview = new TourReview(tourReviewDTO.ScheduleId, tourReviewDTO.GuideKnowledgeGrade, tourReviewDTO.GuideLanguageGrade, tourReviewDTO.TourAttractionsGrade, tourReviewDTO.Impression, tourReviewDTO.TouristId);
            Save(tourReview);
        }
    }
}
