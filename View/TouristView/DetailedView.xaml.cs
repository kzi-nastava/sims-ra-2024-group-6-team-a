﻿using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.ViewModels.TouristViewModel;
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
using System.Windows.Shapes;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for DetailedView.xaml
    /// </summary>
    public partial class DetailedView : Window
    {
        public DetailsViewModel DetailsWindow { get; set; }

        public DetailedView(TourTouristDTO selectedTour)
        {
            InitializeComponent();
            DetailsWindow = new DetailsViewModel(selectedTour);
            DataContext = DetailsWindow;
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is DetailsViewModel viewModel)
            {
                viewModel.SetScrollViewer(scrollViewer);
            }
        }
    }
}
