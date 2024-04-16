using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IOwnerReviewRepository
    {
        public List<OwnerReview> GetAll();
        public OwnerReview Save(OwnerReview ownerReview);
        public void Delete(OwnerReview ownerReview);
        public OwnerReview Get(int reservationId);

    }
}
