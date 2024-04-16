using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        public List<TourReview> GetAll();

        public TourReview GetById(int id);

        public TourReview Save(TourReview review);

        public int NextId();

        public void Delete(TourReview review);
        public TourReview Update(TourReview review);

        public void MakeReview(TourReviewDTO tourReviewDTO);


        public List<TourReview> GetAllReviewsByScheduleId(int tourScheduleId);
    }
}
