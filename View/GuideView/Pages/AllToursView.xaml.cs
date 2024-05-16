using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
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
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page
    {
      

        public AllToursViewModel AllToursWindow { get; set; }
        public AllToursPage(User user)
        {
            InitializeComponent();
            AllToursWindow = new AllToursViewModel(user);
            DataContext = AllToursWindow;
            Update();
        }



        public void Update()
        {
            AllToursWindow.Update();

        }
        public bool CheckUpdateConditions(TourSchedule tourSchedule, Tour tour)
        {
            return AllToursWindow.CheckUpdateConditions(tourSchedule, tour);
        }
        public Model.Image GetFirstTourImage(int tourId)
        {
           return AllToursWindow.GetFirstTourImage(tourId);
        }

        public void LoadAllTours(object sender, RoutedEventArgs e)
        {
            AllToursWindow.LoadAllTours();
        }
    }
}
