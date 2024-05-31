﻿using BookingApp.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BookingApp.Repository;
using System.Runtime.CompilerServices;
using BookingApp.Resources;
using Microsoft.Win32;
using BookingApp.ViewModels;
using BookingApp.ApplicationServices;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationMenu.xaml
    /// </summary>
    /// 
    

    public partial class RegisterAccommodationMenu : Window
    {

        public RegisterAccommodationVM registerAccommodationVM;

        public RegisterAccommodationMenu( int userId)
        {
            InitializeComponent();
            apt.IsChecked = true;
            registerAccommodationVM = new RegisterAccommodationVM(userId);
            DataContext = registerAccommodationVM;

           
            
        }


        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            int guestsParsed;
            int reservationParsed;
            int cancelParsed;

            if (LocationCombo.SelectedItem != null &&  Name.Text != "" && int.TryParse(MaxGuests.Text,out guestsParsed) && int.TryParse(MinReservation.Text,out reservationParsed) && int.TryParse(CancelDays.Text,out cancelParsed))
             {
                if (MessageBox.Show("Confirm registration?", "Register", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    registerAccommodationVM.Register(apt.IsChecked, cottage.IsChecked,LocationCombo.SelectedIndex+1,Name.Text,MaxGuests.Text, MinReservation.Text,CancelDays.Text);

                    Close();
                }

            }
            else
            {
                MessageBox.Show("Please enter every field correctly.","Information Missing",MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void AddImageClick(object sender, RoutedEventArgs e)
        {
            List<String> _imagePath = new List<String>();
            registerAccommodationVM._imageRelativePath.Clear();
            registerAccommodationVM.AddedImages.Clear();
            //so it doesnt duplicate

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (String imgPath in openFileDialog.FileNames)
                {
                    _imagePath.Add(imgPath);
                }
            }


            registerAccommodationVM.ConvertImagePath(_imagePath);
            registerAccommodationVM.AddConvertedImages();


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }

            if(e.Key == Key.Enter && registerAccommodationVM.SelectedImage != null) 
            {
                registerAccommodationVM.RemoveImage();
            }

            if (e.Key == Key.A && !Name.IsKeyboardFocused && !MaxGuests.IsKeyboardFocused && !MinReservation.IsKeyboardFocused && !CancelDays.IsKeyboardFocused && !LocationCombo.IsKeyboardFocused )
            {
                apt.IsChecked = true;
                house.IsChecked = false;
                cottage.IsChecked = false;
            }
            else if (e.Key == Key.H && !Name.IsKeyboardFocused && !MaxGuests.IsKeyboardFocused && !MinReservation.IsKeyboardFocused && !CancelDays.IsKeyboardFocused && !LocationCombo.IsKeyboardFocused)
            {
                apt.IsChecked = false;
                house.IsChecked = true;
                cottage.IsChecked = false;
            }
            else if (e.Key == Key.C && !Name.IsKeyboardFocused && !MaxGuests.IsKeyboardFocused && !MinReservation.IsKeyboardFocused && !CancelDays.IsKeyboardFocused && !LocationCombo.IsKeyboardFocused)
            {
                apt.IsChecked = false;
                house.IsChecked = false;
                cottage.IsChecked = true;
            }


        }

        private void LocationCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LocationCombo.IsDropDownOpen = true;
            }
        }
    }
}
