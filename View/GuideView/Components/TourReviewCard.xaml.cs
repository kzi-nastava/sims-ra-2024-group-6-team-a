using BookingApp.View.GuideView.Pages;
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
    /// Interaction logic for TourReviewCard.xaml
    /// </summary>
    public partial class TourReviewCard : UserControl
    {
        public TourReviewCard()
        {
            InitializeComponent();
        }

        private void MoreInfoMouseDown(object sender, MouseButtonEventArgs e)
        {
            ReviewDetailsPage r = new ReviewDetailsPage(Convert.ToInt32(textBoxId.Text));
            (Window.GetWindow(this) as GuideViewMenu).mainFrame.Content = r;

        }
    }
}
