using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuideView.Components
{
    /// <summary>
    /// Interaction logic for DetailedReviewCard.xaml
    /// </summary>
    public partial class DetailedReviewCard : UserControl 
    {

        public event EventHandler FakeReportEventHandler;

        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }

        public List<Model.Image> ListImages { get; set; }

        public DetailedReviewCard()
        {
            InitializeComponent();
        }

        public void ReportReviewMouseDown(object sender, MouseEventArgs e)
        {
            TourReview review = TourReviewService.GetInstance().GetById(Convert.ToInt32(txtReviewId.Text));
            review.IsValid = false;
            TourReviewService.GetInstance().Update(review);
            HandleFakeReport();
        }


        public void HandleFakeReport()
        {
            FakeReportEventHandler?.Invoke(this, EventArgs.Empty);
        }

        public void NextImage(object sender, RoutedEventArgs e)
        {
            var review = DataContext as TourReviewDTO;
            ListImages = review.ListImages;

            review.NextImage();
        }
        public void PreviousImage(object sender, RoutedEventArgs e)
        {
            var review = DataContext as TourReviewDTO;
            ListImages = review.ListImages;
            review.PreviousImage();
        }

        

    }
}
