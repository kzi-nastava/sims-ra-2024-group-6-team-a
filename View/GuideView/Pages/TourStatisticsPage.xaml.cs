using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
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
using BookingApp.Observer;
using System.ComponentModel;
using BookingApp.ViewModels.GuideViewModel;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourStatisticsPage.xaml
    /// </summary>
    public partial class TourStatisticsPage : Page
    {
     
        public TourStatisticsViewModel StatisticsViewModel { get; set; }
        public TourStatisticsPage(User user, LocationRepository locationRepository, ImageRepository imageRepository, TourScheduleRepository tourScheduleRepository, TourRepository tourRepository, TourGuestRepository tourGuestRepository)
        {
            InitializeComponent();
            StatisticsViewModel = new TourStatisticsViewModel(this, user, locationRepository, imageRepository, tourScheduleRepository, tourRepository, tourGuestRepository);
            DataContext = StatisticsViewModel;

       
            Update();
            FindMostVisitedTour(0);
        }


        public void FindMostVisitedTour(int selectedYear)
        {
            StatisticsViewModel.FindMostVisitedTour(selectedYear);
        }

        public TourStatisticsDTO GetMostVisitedTour(int selectedYear)
        {
           return StatisticsViewModel.GetMostVisitedTour(selectedYear);
        }

        public TourStatisticsDTO FindMostVisitedTourByYear(int selectedYear)
        {
           return StatisticsViewModel.FindMostVisitedTourByYear(selectedYear);
        }
        public TourStatisticsDTO FindMostVisitedTourOverall()
        {
           return StatisticsViewModel.FindMostVisitedTourOverall();
        }


     

        //It is assumed that the statistics are done for the sum of all TourSchedules, if so, we go through the list of Tour
        //and take the tour schedule, for each tour we count the number of children/adults/elderly, we also need to make a function for dates Combobox because of distinct value
        public void Update()
        {
            StatisticsViewModel.Update();

        }


        public void CountGuests(TourSchedule schedule, ref int touristCount, ref int childrenCount, ref int adultCount, ref int elderlyCount)
        {
            StatisticsViewModel.CountGuests(schedule, ref touristCount, ref childrenCount, ref adultCount, ref elderlyCount);
        }

        public void AddDatesToComboBox(List <int> dates)
        {
          StatisticsViewModel.AddDatesToComboBox(dates);    
        }

        public Model.Image GetFirstTourImage(int tourId)
        {
            return StatisticsViewModel.GetFirstTourImage(tourId);
        }
    }
}
