using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView.Components;
using BookingApp.ViewModels.GuideViewModel;
namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for LiveToursPage.xaml
    /// </summary>
    public partial class LiveToursPage : Page 
    { 
        public LiveToursPageViewModel LiveViewModel { get; set; }

        public LiveToursPage(User user)
        {
            InitializeComponent();
            LiveViewModel = new LiveToursPageViewModel(user);
            DataContext = LiveViewModel;    
        }

        private void LoadTodaysTours(object sender, RoutedEventArgs e)
        {
            LiveViewModel.LoadTodaysTours();
        }

    }
}
