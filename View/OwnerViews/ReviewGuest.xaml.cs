using BookingApp.ApplicationServices;
using BookingApp.Repository;
using BookingApp.ViewModels.OwnerViewModel;
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
    /// Interaction logic for ReviewGuest.xaml
    /// </summary>
    public partial class ReviewGuest : Window
    {

        public ReviewGuestVM ViewModel {  get; set; }
        public ReviewGuest(int reservationId)
        {
            ViewModel = new ReviewGuestVM(reservationId);
            DataContext = ViewModel;
            InitializeComponent();

            CommentBox.MaxLength = 60;
        }
    }


}
