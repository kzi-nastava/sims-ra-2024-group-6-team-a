using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces
{
    public interface IGuestReviewRepository
    {
        public List<GuestReview> GetAll();
        public GuestReview Save(GuestReview GuestReview);
        public void Delete(GuestReview GuestReview);
        public bool DoesGradeExist(int reservationId);
        public GuestReview Get(int reservationId);
    }
}
