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


    }

}
