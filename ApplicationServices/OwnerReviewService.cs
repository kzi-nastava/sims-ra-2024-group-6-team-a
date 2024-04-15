using BookingApp.Domain.RepositoryInterfaces;
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
    public class OwnerReviewService
    {
        private IOwnerReviewRepository ownerReviewRepository { get; set; }

        public OwnerReviewService()
        {
            ownerReviewRepository = new OwnerReviewRepository();
        }
        public List<OwnerReview> GetAll()
        {
            return ownerReviewRepository.GetAll();
        }
        public OwnerReview Save(OwnerReview ownerReview)
        {
            return ownerReviewRepository.Save(ownerReview);
        }
        public void Delete(OwnerReview ownerReview)
        {
             ownerReviewRepository.Delete(ownerReview);
        }
        public OwnerReview Get(int reservationId)
        {
            return ownerReviewRepository.Get(reservationId);
        }
        public static OwnerReviewService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<OwnerReviewService>();
        }
    }
}
