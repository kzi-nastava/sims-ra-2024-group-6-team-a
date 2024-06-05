using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using BookingApp.ViewModels.GuideViewModel;
using Microsoft.Win32;
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

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for RequestedTourCreation.xaml
    /// </summary>
    public partial class RequestedTourCreation : Page
    {
        public RequestedTourCreationViewModel CreationViewModel {  get; set; }
        public RequestedTourCreation(int requestId, int userId)
        {
            InitializeComponent();
            CreationViewModel = new RequestedTourCreationViewModel(this,requestId,userId);
            DataContext = CreationViewModel;
        }

        private void DurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CreationViewModel.DurationTextBox_PreviewTextInput(sender, e);
        }

        private void AddCheckPointClick(object sender, EventArgs e)
        {

            CreationViewModel.AddCheckPointClick();
        }
        private void SelectImagesClick(object sender, RoutedEventArgs e)
        {
            CreationViewModel.SelectImagesClick();
        }
        private void SelectDatesClick(object sender, RoutedEventArgs e)
        {
            CreationViewModel.SelectDatesClick();
        }

        private void RemoveDateClick(object sender, RoutedEventArgs e)
        {
            CreationViewModel.RemoveDateClick();
        }
        private void CreateTourClick(object sender, RoutedEventArgs e)
        {
            CreationViewModel.CreateTourClick();
        }
        private void SaveTourDatesAndCheckpoints(List<DateTime> dates, List<String> checkpoints)
        {
            CreationViewModel.SaveTourDatesAndCheckpoints(dates, checkpoints);
        }
        private void CheckpointRemoveClick(object sender, EventArgs e)
        {
            CreationViewModel.CheckpointRemoveClick();
        }

        private void ImageRemoveClick(object sender, EventArgs e)
        {
            CreationViewModel.ImageRemoveClick();
        }
        private void CancelCreationClick(object sender, EventArgs e)
        {
                CreationViewModel.CancelCreationClick();
        }
    }
}
