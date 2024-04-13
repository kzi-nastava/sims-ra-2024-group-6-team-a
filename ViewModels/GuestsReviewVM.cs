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
        public GuestsReviewVM(OwnerReview ownerReview) 
        {
            this.Cleanliness = ownerReview.Cleanliness;
            this.Correctness = ownerReview.Correctness;
            this.AdditionalComment = ownerReview.AditionalComment;
        }

    }
}
