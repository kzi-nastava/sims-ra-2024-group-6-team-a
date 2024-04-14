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

        TourScheduleRepository _tourScheduleRepository;
        public event EventHandler TourEndedCard;

        public TourCard()
        {
            InitializeComponent();
            _tourScheduleRepository = new TourScheduleRepository();
        }



        private void BeginTourMouseDown(object sender, MouseEventArgs e)
        {

            TourSchedule tourSchedule = _tourScheduleRepository.GetById(Convert.ToInt32(textBoxId.Text));
            tourSchedule.TourActivity = Enums.TourActivity.Ongoing;
            _tourScheduleRepository.Update(tourSchedule);
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
