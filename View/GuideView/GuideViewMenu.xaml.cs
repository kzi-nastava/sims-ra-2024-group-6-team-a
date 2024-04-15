using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView;
using BookingApp.View.GuideView.Components;
using BookingApp.View.GuideView.Pages;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuideViewMenu.xaml
    /// </summary>
    public partial class GuideViewMenu : Window
    {
       
        public GuideMenuViewModel MainWindowViewModel {  get; set; }

        public GuideViewMenu(User user,LocationRepository locationRepository,ImageRepository imageRepository)
        {
            InitializeComponent();
            DataContext = MainWindowViewModel;
            MainWindowViewModel = new GuideMenuViewModel(this, user);
        }

        private void ShowCreateTourForm(object sender, EventArgs e)
        {
            MainWindowViewModel.ShowCreateTourForm();
        }

        private void LiveToursPageEvent(object sender, EventArgs e)
        {
            MainWindowViewModel.LiveToursPageEvent(sender, e);        
        }

        private void TourStatisticsPageClick(object sender, EventArgs e)
        {
            MainWindowViewModel.TourStatisticsPageClick();
        }

       private void TourReviewsPageClick(object sender, EventArgs e)
        {
            MainWindowViewModel.TourReviewsPageClick();
        }


        private void LiveToursPageClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.LiveToursPageClick();
        }

        private void AllToursPageClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.AllToursPageClick();
        }
    }
}
