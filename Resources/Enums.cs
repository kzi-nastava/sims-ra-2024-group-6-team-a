using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources
{
    public  class Enums
    {
       public enum ImageType
        {
            Accommodation,
            Tour,

            TourReview,

            OwnersReviews 

        }

        public enum UserType
        {
            Owner,
            Tourist,
            Guide,
            Guest
        }

        public enum AccommodationType
        {
            Apartment,
            House,
            Cottage
        }
        public enum ReservationStatus
        {
            Active,
            Canceled,
            Changed,
            CanceledApproved
            
        }

        public enum ReservationChangeStatus
        {
            Pending,
            Rejected,
            Accepted,
            AcceptedSeen,
            RejectedSeen

        }
        public enum TourActivity
        {
            Ready,
            Ongoing,
            Finished
        }
        public enum RequestStatus
        {
            Onhold, 
            Accepted, 
            Invalid
        }

        public enum NotificationType
        {
            Attendance,
            AcceptedRequest,
            NewTour
        }
        public enum TourType
        {
            Ordinary,
            Requested
        }
    }
}
