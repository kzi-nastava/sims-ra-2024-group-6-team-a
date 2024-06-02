using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace BookingApp.ApplicationServices
{
    public class TourReviewService
    {
        private ITourReviewRepository _reviewRepository;

        public TourReviewService(ITourReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public TourReviewService() 
        {
            _reviewRepository = new TourReviewRepository(); 
        }
       


        public static TourReviewService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TourReviewService>();
        }


        public TourReview GetById(int id)
        {
            return _reviewRepository.GetById(id);
        }

        public TourReview Update(TourReview review)
        {
           return _reviewRepository.Update(review);
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
            TourReview tourReview = new TourReview(tourReviewDTO.ScheduleId, tourReviewDTO.GuideKnowledgeGrade, tourReviewDTO.GuideLanguageGrade, tourReviewDTO.TourAttractionsGrade, tourReviewDTO.Impression, tourReviewDTO.TouristId, tourReviewDTO.IsValid);
            Save(tourReview);
        }

        public List<TourReview> GetAllReviewsByScheduleId(int tourScheduleId)
        {
            return _reviewRepository.GetAllReviewsByScheduleId(tourScheduleId);

        }
        public List<TourReview> GetAllReviewsByUser(User user)
        {
            List<TourSchedule> schedules = TourScheduleService.GetInstance().GetAllByUser(user);
            List<TourReview> reviews = new List<TourReview>();
            foreach (TourSchedule schedule in schedules) 
            { 

                foreach(TourReview review in GetAllReviewsByScheduleId(schedule.Id))
                {
                    reviews.Add(review);
                }
            }

            return reviews;
        }

        public List<TourReview> GetAllReviewsByPrimaryLanguage(User user, Language language) 
        {
            List<TourReview> reviews = GetAllReviewsByUser(user);
            List<TourReview> primaryLanguageReviews = new List<TourReview>();
            foreach (TourReview review in reviews)
            {
                TourSchedule tourSchedule = TourScheduleService.GetInstance().GetById(review.ScheduleId);
                Tour tour = TourService.GetInstance().GetById(tourSchedule.TourId);
                if (tour.LanguageId == language.Id)
                {
                    primaryLanguageReviews.Add(review);
                }
            }
            return primaryLanguageReviews;  
        }
       
    }
}
