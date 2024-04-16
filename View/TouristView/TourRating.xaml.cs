using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using BookingApp.ViewModels.TouristViewModel;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for TourRating.xaml
    /// </summary>
    public partial class TourRating : Window
    {
        public TourRatingViewModel RatingViewModel { get; set; }
        public TourRating(TourScheduleDTO selectedTour, User user)
        {
            InitializeComponent();
            RatingViewModel = new TourRatingViewModel(this, selectedTour,user);
            DataContext = RatingViewModel;
           
        }

    }
}
