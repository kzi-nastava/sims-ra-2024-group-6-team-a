using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
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

        public event EventHandler TourEndedCard;

        public TourCard()
        {
            InitializeComponent();
        }



        private void BeginTourMouseDown(object sender, MouseEventArgs e)
        {

            TourSchedule tourSchedule = TourScheduleService.GetInstance().GetById(Convert.ToInt32(textBoxId.Text));
            tourSchedule.TourActivity = Enums.TourActivity.Ongoing;
            TourScheduleService.GetInstance().Update(tourSchedule);
            LiveTour l = new LiveTour(Convert.ToInt32(textBoxId.Text));
            l.TourEnded += HandleTourEndedEvent;
            (Window.GetWindow(this) as GuideViewMenu).MainFrame.Content = l;

        }

         public void HandleTourEndedEvent(object sender, EventArgs e)
        {
            TourEndedCard?.Invoke(this, e);
        }

    }
}
