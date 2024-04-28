using BookingApp.DTOs;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class GuestsReviewVM
    {
        public int Cleanliness { get; set; }
        public int Correctness { get; set; }
        public string AdditionalComment { get; set; }
        public string RenovationRec {  get; set; }
        public GuestsReviewVM(OwnerReview ownerReview) 
        {
            this.Cleanliness = ownerReview.Cleanliness;
            this.Correctness = ownerReview.Correctness;
            this.AdditionalComment = ownerReview.AditionalComment;

            if (ownerReview.Urgency == "Level5" && ownerReview.Urgency == "Level4")
                this.RenovationRec = "The guest thinks that this accommodation must have a renovation urgently.";
            else if (ownerReview.Urgency == "Level3" && ownerReview.Urgency == "Level2")
                this.RenovationRec = "The guest thinks that this accommodation could use a renovation soon.";
            else
                this.RenovationRec = "The guest thinks that this accommodation does not require a renovation.";

        }

    }
}
