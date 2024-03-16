using BookingApp.DTOs;
using BookingApp.Model;
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
    /// Interaction logic for TourCard.xaml
    /// </summary>
    public partial class TourCard : UserControl
    {



        public TourCard()
        {
            InitializeComponent();
        }

        private void BeginTourMouseDown(object sender, MouseEventArgs e)
        {
            (Window.GetWindow(this) as GuideViewMenu).mainFrame.Content = new LiveTour(Convert.ToInt32(textBoxId.Text));
        }

    }
}
