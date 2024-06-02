using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels
{
    public class AccommodationMenuVM
    {
        public  ObservableCollection<AccommodationOwnerDTO> Accommodations { get; set; }
        public  ObservableCollection<GuestReviewDTO> GuestReviews { get; set; }
        public  ObservableCollection<ReservationOwnerDTO> Reservations { get; set; }
        public ObservableCollection<ReservationChangeDTO> ReservationChanges { get; set; }

        public AccommodationOwnerDTO SelectedAccommodation { get; set; }
        public GuestReviewDTO SelectedGuestReview { get; set; }
        public ReservationOwnerDTO SelectedReservation { get; set; }
        public ReservationChangeDTO SelectedChange { get; set; }

        public OwnerInfoDTO OwnerInfo { get; set; }

        public Owner Owner { get; set; }
  


        bool existsNotReviewed = false;
        bool existsCanceled = false;
        bool existsBlog = false;

        public AccommodationMenuVM(Owner owner)
        {
            

            Owner = owner;


            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            GuestReviews = new ObservableCollection<GuestReviewDTO>();
            Reservations = new ObservableCollection<ReservationOwnerDTO>();
            ReservationChanges = new ObservableCollection<ReservationChangeDTO>();

            
            Update();
            EntryNotification();
 
        }

        public void EntryNotification() 
        {
            if(existsCanceled && existsNotReviewed && existsBlog)
            {
                MessageBox.Show("You have cancelled reservations,unreviewed guests and new blogs!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (existsNotReviewed && existsBlog)
            {
                MessageBox.Show("You have unreviewed guests and new blogs!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (existsCanceled && existsBlog)
            {
                MessageBox.Show("You have cancelled reservations and new blogs!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(existsCanceled && existsNotReviewed) 
            {
                MessageBox.Show("You have cancelled reservations and unreviewed guests!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if( existsCanceled) 
            {
                MessageBox.Show("You have cancelled reservations!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else if(existsBlog)
            {
                MessageBox.Show("You have new blogs!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else if(existsNotReviewed)
            {
                MessageBox.Show("You have unreviewed guests!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        public void Register()
        {
            RegisterAccommodationMenu registerAccommodationMenu = new RegisterAccommodationMenu(Owner.Id);
            registerAccommodationMenu.ShowDialog();
            Update();
        }

        public void Update()
        {
            ClearOldData();

            foreach (Accommodation a in AccommodationService.GetInstance().GetByOwnerId(Owner.Id))
            {
                foreach (AccommodationReservation r in AccommodationReservationService.GetInstance().GetByAccommodation(a))
                {
                    if (r.Status == Enums.ReservationStatus.Canceled)
                    {
                        existsCanceled = true;
 
                    }
                    UpdateReservationsOwnerAndReviews(a, r);
                }

                string imagePath = ImageService.GetInstance().AddMainAccommodationImage(a);

                AddChangedReservations(a);

                Accommodations.Add(new AccommodationOwnerDTO(a, LocationService.GetInstance().GetByAccommodation(a), imagePath));

                if(!existsBlog)
                    existsBlog = AccommodationBlogService.GetInstance().ExistsBlogWithinAccommodation(a);
            }

            OwnerService.GetInstance().UpdateOwnerStatus(Owner);

        }

        public void ClearOldData()
        {
            Accommodations.Clear(); //we must clear so it doesnt duplicate
            GuestReviews.Clear();
            Reservations.Clear();
            ReservationChanges.Clear();
            Owner.AverageGrade = 0;
            Owner.GradeCount = 0;
        }

        public void UpdateReservationsOwnerAndReviews(Accommodation a,AccommodationReservation r)
        {
            if(r.Status != Enums.ReservationStatus.Changed)
                 AddReservations(r, a);
            CheckGuestReview(a, r);
            OwnerService.GetInstance().UpdateOwner(r,Owner);
        }



        public void AddChangedReservations(Accommodation accommodation)
        {
            foreach (ReservationChanges reservationChange in ReservationChangeService.GetInstance().GetAll())
            {
                if (reservationChange.AccommodationId == accommodation.Id && reservationChange.Status == Enums.ReservationChangeStatus.Pending)
                {

                    String userName = GuestService.GetInstance().GetFullname(AccommodationReservationService.GetInstance().GetAll().Find(r => r.Id == reservationChange.ReservationId).GuestId);
                    String oldDate = reservationChange.OldCheckIn.ToString("dd MMMM yyyy") + "   ->   " + reservationChange.OldCheckOut.ToString("dd MMMM yyyy");
                    String newDate = reservationChange.NewCheckIn.ToString("dd MMMM yyyy") + "   ->   " + reservationChange.NewCheckOut.ToString("dd MMMM yyyy");
                    String bookedStatus = "No";
                    if (AccommodationService.GetInstance().CheckIfAlreadyBooked(reservationChange, accommodation))
                        bookedStatus = "Yes";

                    String imagePath = ImageService.GetInstance().AddMainAccommodationImage(accommodation);

                    ReservationChangeDTO newChange = new ReservationChangeDTO(reservationChange.ReservationId, userName, accommodation.Name, oldDate, newDate, bookedStatus,imagePath);

                    ReservationChanges.Add(newChange);
                }

            }


        }
        public void AddReservations(AccommodationReservation reservation, Accommodation accommodation)
        {

            String userName = GuestService.GetInstance().GetFullname(reservation.GuestId);
            String imagePath = ImageService.GetInstance().AddMainAccommodationImage(accommodation);

            ReservationOwnerDTO newReservation = new ReservationOwnerDTO(userName, reservation, accommodation.Name, LocationService.GetInstance().GetByAccommodation(accommodation), imagePath);

            Reservations.Add(newReservation);
        }

        public void CheckGuestReview(Accommodation accommodation, AccommodationReservation reservation)
        {
            DateTime GuestCheckout = reservation.CheckOutDate.ToDateTime(TimeOnly.MinValue);
            double DaysPassedForReview = (DateTime.Today - GuestCheckout).TotalDays;

            if (DaysPassedForReview >= 0)
            {
                GuestReview review = GuestReviewService.GetInstance().Get(reservation.Id);

                if (review == null && DaysPassedForReview < 5)
                {
                    AddGuestReview(accommodation, reservation);
                    existsNotReviewed = true;
                }
                else if (review != null)
                {
                    AddGuestReview(accommodation, reservation, review.CleanlinessGrade, review.RespectGrade, review.Comment);
                }

            }
        }

        public void AddGuestReview(Accommodation accommodation, AccommodationReservation reservation, int cleanlinessGrade = 0, int respectGrade = 0, string comment = "")
        {
            string GuestOccupationPeriod = reservation.CheckInDate.ToString("dd.MM.yyyy") + " - " + reservation.CheckOutDate.ToString("dd.MM.yyyy");
            GuestReviews.Add(new GuestReviewDTO(accommodation.Name, GuestService.GetInstance().GetFullname(reservation.GuestId), cleanlinessGrade, respectGrade, comment, GuestOccupationPeriod, reservation.Id));
        }

        public void DetailedAccommodationView()
        {

            AccommodationDetailedMenu AccommodationDetailedMenu = new AccommodationDetailedMenu(ImageService.GetInstance().GetImagesForAccommodaton(SelectedAccommodation.Id), 
                AccommodationService.GetInstance().GetReservationsForAccommodation(SelectedAccommodation), SelectedAccommodation);
            AccommodationDetailedMenu.ShowDialog();

        }

        public void GradeEmptyReview()
        {

            if (SelectedGuestReview.respectGrade == 0)
            {
                ReviewGuest reviewGuest = new ReviewGuest(SelectedGuestReview.ReservationId);
                reviewGuest.ShowDialog();
            }
        }

        public OwnerReview FindLinkedReview(GuestReviewDTO review)
        {
            return OwnerReviewService.GetInstance().Get(review.reservationId);
        }

        public void ShowGuestsReview()
        {
            OwnerReview ownerReview = FindLinkedReview(SelectedGuestReview);
            if (ownerReview != null)
            {
                GuestsReviewOfAccommodation guestsReview = new GuestsReviewOfAccommodation(SelectedGuestReview, FindLinkedReview(SelectedGuestReview));
                guestsReview.ShowDialog();
            }
            else
                MessageBox.Show("This guest hasn't reviewed your accommodation yet.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void AllowReservationChange()
        {
            AllowReservationChange allowReservationChange = new AllowReservationChange(SelectedChange);
            allowReservationChange.ShowDialog();

        }

        public void EnterOwnerInfo()
        {
            Math.Round(Owner.AverageGrade, 3);   
            OwnerInfo = new OwnerInfoDTO(Owner, AccommodationService.GetInstance().GetTotalReservationCount(Owner.Id), AccommodationService.GetInstance().GetTotalAccommodationCount(Owner.Id));
            OwnerInfo ownerInfo = new OwnerInfo(OwnerInfo);
            ownerInfo.ShowDialog();

        }

    }
}
