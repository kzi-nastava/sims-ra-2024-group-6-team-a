using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class GuestRatingsDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int Id { get; set; }
        public int AccommodationId { get; set; }

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }

            set
            {
                if (value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }
            }

        }
        private string comment;
        public string Comment
        {
            get { return comment; }

            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private DateOnly checkIn;

        public DateOnly CheckIn
        {
            get
            {
                return checkIn;
            }

            set
            {
                if (value != checkIn)
                {
                    checkIn = value;
                    OnPropertyChanged("CheckIn");
                }
            }
        }

        private DateOnly checkOut;
        public DateOnly CheckOut
        {
            get
            { return checkOut; }

            set
            {
                if (value != checkOut)
                {
                    checkOut = value;
                    OnPropertyChanged("CheckOut");
                }
            }
        }
        public String image;

        public String Image
        {
            get
            {
                return image;
            }

            set
            {
                if (value != image)
                {
                    image = value;
                    OnPropertyChanged("image");
                }
            }
        }
        public int followRules;

        public int FollowRules
        {
            get
            {
                return followRules;
            }

            set
            {
                if (value != followRules)
                {
                    followRules = value;
                    OnPropertyChanged("FollowRules");
                }
            }
        }
        public int Cleanliness { get; set; }


        public GuestRatingsDTO(Guest guest,AccommodationReservation reservation, GuestReview guestReview, Accommodation accommodation, String image)
        {
            //dodaj i za guest
            this.CheckOut = reservation.CheckOutDate;
            this.CheckIn = reservation.CheckInDate;
            this.comment = guestReview.Comment;
            this.Id = guestReview.ReservationId;
            this.accommodationName = accommodation.Name;
            this.FollowRules = guestReview.RespectGrade;
            this.Cleanliness = guestReview.CleanlinessGrade;
            this.image = image;
        }

    }
}
