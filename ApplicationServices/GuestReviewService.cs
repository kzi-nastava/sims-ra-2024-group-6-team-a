using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class GuestReviewService
    {
        private IGuestReviewRepository guestReviewRepository { get; set; }

        public GuestReviewService()
        {
            guestReviewRepository = new GuestReviewRepository();
        }

        public List<GuestReview> GetAll()
        {
            return guestReviewRepository.GetAll();
        }
        public GuestReview Save(GuestReview GuestReview)
        {
            return guestReviewRepository.Save(GuestReview);
        }

        public bool DoesGradeExist(int reservationId)
        {
            return guestReviewRepository.DoesGradeExist(reservationId);
        }

        public GuestReview Get(int reservationId)
        {
            return guestReviewRepository.Get(reservationId);
        }

    }

}
