using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class GuestReviewService
    {
        private IGuestReviewRepository guestReviewRepository;

        public GuestReviewService(IGuestReviewRepository guestReviewRepository)
        {
            this.guestReviewRepository = guestReviewRepository;
        }

        public static GuestReviewService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<GuestReviewService>();
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
