using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
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

    }
}
