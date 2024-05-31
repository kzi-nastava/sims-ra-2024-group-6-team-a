using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestsReviewOfAccommodation.xaml
    /// </summary>
    public partial class GuestsReviewOfAccommodation : Window
    {
        private GuestsReviewVM vm;
        public GuestsReviewOfAccommodation(GuestReviewDTO selectedGuest,OwnerReview ownerReview)
        {
            vm = new GuestsReviewVM(ownerReview);
            DataContext = vm;
            InitializeComponent();
            Title = selectedGuest.GuestName + "'s review.";
           
        }

    }
}
