using BookingApp.Repository;
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
        private GuestReviewRepository _guestReviewRepository;
        private int reservationId;
        public ReviewGuest(GuestReviewRepository _guestReviewRepository,int reservationId)
        {
            DataContext = this;
            InitializeComponent();
            for(int i = 1;i < 6;i++)
            {
                CleanGradeCombo.Items.Add(i);
                RespectGradeCombo.Items.Add(i);
            }

            this._guestReviewRepository = _guestReviewRepository;
            this.reservationId = reservationId;
            CommentBox.MaxLength = 60;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CleanGradeCombo.SelectedItem != null && RespectGradeCombo != null)
            {
                _guestReviewRepository.Save(new Model.GuestReview(reservationId, CleanGradeCombo.SelectedIndex + 1, RespectGradeCombo.SelectedIndex + 1, CommentBox.Text));
                Close();
            }
            else
            {
                MessageBox.Show("Must fill all fields.","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


}
