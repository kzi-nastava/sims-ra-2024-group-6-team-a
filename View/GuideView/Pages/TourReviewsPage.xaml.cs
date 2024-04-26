using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.ViewModels.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for TourReviewsPage.xaml
    /// </summary>
    


    public partial class TourReviewsPage : Page
    {
       

        public TourReviewsViewModel ReviewViewModel {  get; set; }

        public TourReviewsPage(Frame mainFrame, User user)
        {
            InitializeComponent();
            ReviewViewModel = new TourReviewsViewModel(this, mainFrame, user);
            DataContext = ReviewViewModel;
            Update();
        }


        public void Update()
        {
            ReviewViewModel.Update();

        }

        public double CalculateAvgGrade(int tourScheduleId) 
        {
           return ReviewViewModel.CalculateAvgGrade(tourScheduleId);
        }

        public Model.Image GetFirstTourImage(int tourId)
        {
            return ReviewViewModel.GetFirstTourImage(tourId);
        }
    }
}
