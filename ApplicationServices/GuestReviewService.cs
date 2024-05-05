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
        public List<GuestReview> GetAllByGuest(int guestId)
        {
            List<GuestReview> reviews = new List<GuestReview>();
            foreach (GuestReview guestReview in GuestReviewService.GetInstance().GetAll()) { 
            AccommodationReservation reservation = AccommodationReservationService.GetInstance().GetByReservationId(guestReview.ReservationId);
                if (reservation.GuestId == guestId) reviews.Add(guestReview);
            }
            return reviews;
        }
        public List<GuestReview> GetLegalReviews(int guestId)
        {
            List<GuestReview> legalReviews = new List<GuestReview>();
            foreach (GuestReview guestReview in GuestReviewService.GetInstance().GetAllByGuest(guestId))
            {
                foreach (OwnerReview ownerReview in OwnerReviewService.GetInstance().GetAll())
                {
                    if (ownerReview.ReservationId == guestReview.ReservationId)
                        legalReviews.Add(guestReview);
                }
            }
            return legalReviews;

        }
        public GuestReview Save(GuestReview GuestReview)
        {
            return guestReviewRepository.Save(GuestReview);
        }

        public bool DoesGradeExist(int reservationId)
        {
            return guestReviewRepository.DoesGradeExist(reservationId);
        }
        public GuestReview GetByReservationId(int id)
        {
            return guestReviewRepository.GetByReservationId(id);
        }
        public GuestReview Get(int reservationId)
        {
            return guestReviewRepository.Get(reservationId);
        }

        public double GetAverageGradeByGuest(Guest guest)
        {
            double averageGrade=0;
            int iterator = 0;
            foreach (GuestReview guestReview in GuestReviewService.GetInstance().GetLegalReviews(guest.Id)) {
                        AccommodationReservation reservation = AccommodationReservationService.GetInstance().GetByReservationId(guestReview.ReservationId);
                        if (guest.Id == reservation.GuestId)
                        {
                            averageGrade += guestReview.RespectGrade + guestReview.CleanlinessGrade;
                            iterator++;
                        }
            }
            if (iterator == 0) iterator = 1;
                return averageGrade/(iterator*2.0);
        }


        public void Delete(GuestReview GuestReview)
        {
            guestReviewRepository.Delete(GuestReview);
        }



    }



}
