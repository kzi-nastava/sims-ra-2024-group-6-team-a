using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using BookingApp.ViewModels.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class ReviewDetailsPage : Page
    {


        
        public ReviewDetailsViewModel ReviewDetViewModel { get; set; }

        public ReviewDetailsPage(int tourScheduleId)
        {
            InitializeComponent();
            ReviewDetViewModel = new ReviewDetailsViewModel(this,tourScheduleId);
            DataContext = ReviewDetViewModel;
            Update();
        }

        private void Update()
        {
            ReviewDetViewModel.Update();
        }

        private string GetUsername(int userId)
        {
            return ReviewDetViewModel.GetUsername(userId);

        }
        private List<Model.Image> GetAllImages(int tourShceduleId)
        {
            return ReviewDetViewModel.GetAllImages(tourShceduleId);
        }

        private int GetFirstUsersCheckpointId(int tourShceduleId, int userId)
        {
            return ReviewDetViewModel.GetFirstUsersCheckpointId(tourShceduleId, userId);
        }
        private Checkpoint GetCheckpointById(int checkpointId) 
        { 
            return ReviewDetViewModel.GetCheckpointById(checkpointId);
        }

        private void HandleFakeReport(object sender, EventArgs e)
        {
            Update();
        }

        private void GoBackClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
