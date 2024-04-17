using BookingApp.ApplicationServices;
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
       
        private int reservationId;
        public ReviewGuest(int reservationId)
        {
            DataContext = this;
            InitializeComponent();

            InitializeComboBoxes();

           
            this.reservationId = reservationId;
            CommentBox.MaxLength = 60;
        }

        private void InitializeComboBoxes()
        {
            for (int i = 1; i < 6; i++)
            {
                CleanGradeCombo.Items.Add(i);
                RespectGradeCombo.Items.Add(i);
            }

        }


        private void SaveGrade(object sender, RoutedEventArgs e)
        {
            if (CleanGradeCombo.SelectedItem != null && RespectGradeCombo.SelectedItem != null)
            {
                if (MessageBox.Show("Confirm review?", "Grade the guest", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    
                    GuestReviewService.GetInstance().Save(new Model.GuestReview(reservationId, CleanGradeCombo.SelectedIndex + 1, RespectGradeCombo.SelectedIndex + 1, CommentBox.Text));
                    Close();
                }

            }
            else
            {
                MessageBox.Show("Must fill all fields.","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }


}
