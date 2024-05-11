using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.ViewModels.GuideViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for TourCreationPage.xaml
    /// </summary>
    public partial class TourCreationPage : Page
    {



        public TourCreationViewModel TourCreationViewModel { get; set; }


        public TourCreationPage(User user)
        {
            InitializeComponent();
            TourCreationViewModel = new TourCreationViewModel(this, user);
            DataContext = TourCreationViewModel;

        }
       

        private void AddCheckPointClick(object sender, EventArgs e)
        {

            TourCreationViewModel.AddCheckPointClick();
        }
        
      
        private void SelectImagesClick(object sender, RoutedEventArgs e)
        {
            TourCreationViewModel.SelectImagesClick();
        }

        private void SelectDatesClick(object sender, RoutedEventArgs e)
        {
            TourCreationViewModel.SelectDatesClick();
        }

        private void RemoveDateClick(object sender, RoutedEventArgs e)
        {
            TourCreationViewModel.RemoveDateClick();
        }

        private void CreateTourClick(object sender, RoutedEventArgs e)
        {
            TourCreationViewModel.CreateTourClick();
        }

  
        private void CheckpointRemoveClick(object sender, EventArgs e)
        {
            TourCreationViewModel.CheckpointRemoveClick();
        }

        private void ImageRemoveClick(object sender, EventArgs e)
        {
            TourCreationViewModel.ImageRemoveClick();        
        }

        private void CancelCreationClick(object sender, EventArgs e)
        {
            TourCreationViewModel.CancelCreationClick();        
        }

    }
}

