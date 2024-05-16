using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class MyRatingsViewModel
    {
        public ObservableCollection<GuestRatingsDTO> Ratings { get; set; }
        public Guest Guest { get; set; }
        public double AverageGrade { get; set; }
        public NavigationService NavService { get; set; }

        public MyRatingsViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;
            Ratings = new ObservableCollection<GuestRatingsDTO>();
            NavService = navigation;
            AverageGrade = GuestReviewService.GetInstance().GetAverageGradeByGuest(Guest);
            Update();
        }
        public void Update()
        {
            Ratings.Clear();
            foreach (GuestReview guestReviw in GuestReviewService.GetInstance().GetLegalReviews(Guest.Id))
            {
                AccommodationReservation reservation = AccommodationReservationService.GetInstance().GetByReservationId(guestReviw.ReservationId);
                Accommodation accommodation = AccommodationService.GetInstance().GetByReservationId(reservation.AccommodationId);
                Model.Image image = new Model.Image();
                foreach (Model.Image i in ImageService.GetInstance().GetByEntity(reservation.AccommodationId, Enums.ImageType.Accommodation))
                {
                    image = i;
                    break;
                }
                Ratings.Add(new GuestRatingsDTO(Guest, reservation, guestReviw, accommodation, image.Path));
            }
        }




    }
}
