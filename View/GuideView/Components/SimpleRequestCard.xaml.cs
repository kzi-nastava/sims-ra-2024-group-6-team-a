using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
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
    /// Interaction logic for SimpleRequestCard.xaml
    /// </summary>
    public partial class SimpleRequestCard : UserControl
    {
        public SimpleRequestCard()
        {
            InitializeComponent();
        }

        public void AcceptRequestClick(object sender, RoutedEventArgs e)
        {
            RequestedTourCreation tourCreation = new RequestedTourCreation(Convert.ToInt32(txtRequestId.Text), Convert.ToInt32(txtUserId.Text));

            (Window.GetWindow(this) as GuideViewMenu).MainFrame.Content = tourCreation;
        }
    }
}
