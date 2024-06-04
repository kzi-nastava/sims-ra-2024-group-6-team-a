using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using BookingApp.ViewModels.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for AlreadyStartedTour.xaml
    /// </summary>
    public partial class AlreadyStartedTour : Page
    {


        AlreadyStartedTourViewModel ViewModel { get; set; }


        public AlreadyStartedTour(int tourScheduleId, User user)
        {
            InitializeComponent();

            ViewModel = new AlreadyStartedTourViewModel(this, user, tourScheduleId);
            DataContext = ViewModel;
        }
    }
}
