using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for ScheduleRenovation.xaml
    /// </summary>
    public partial class ScheduleRenovation : Window
    {
        ScheduleRenovationVM ViewModel { get; set; }

        public ScheduleRenovation(AccommodationOwnerDTO Accommodation,List<ReservationOwnerDTO> Reservations)
        {
            ViewModel = new ScheduleRenovationVM(Accommodation, Reservations);
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            HelpBox.Text = ViewModel.FindDates(StartDatePicker.SelectedDate,EndDatePicker.SelectedDate,DurationPicker.Text);

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int duration;
            if(ViewModel.isPeriodFound && StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null && int.TryParse(DurationPicker.Text, out duration))
            {
                if(MessageBox.Show("Confirm renovation?", "Schedule", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    ViewModel.ConfirmSchedule(TitleText.Text);
                    Close();
                }
            }
            else
            {
                HelpBox.Text = "Please find a period first using dates and a duration.";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                if(StartDatePicker.IsDropDownOpen)
                {
                    StartDatePicker.IsDropDownOpen = false;
                }
                else if(EndDatePicker.IsDropDownOpen)
                {
                    EndDatePicker.IsDropDownOpen= false;
                }
                else
                {
                    Close();
                }
                
            }
            else if(e.Key == Key.S && !DurationPicker.IsKeyboardFocused && !TitleText.IsKeyboardFocused)
            {
                EndDatePicker.IsDropDownOpen = false;
                StartDatePicker.IsDropDownOpen = true;
                
            }
            else if(e.Key == Key.E && !DurationPicker.IsKeyboardFocused && !TitleText.IsKeyboardFocused)
            {
                StartDatePicker.IsDropDownOpen = false;
                EndDatePicker.IsDropDownOpen = true;

            }
            else if(e.Key == Key.F && !DurationPicker.IsKeyboardFocused && !TitleText.IsKeyboardFocused) 
            {
                Find_Click(sender, e);  
            }
            else if(e.Key == Key.C && !DurationPicker.IsKeyboardFocused && !TitleText.IsKeyboardFocused )
            {
                Confirm_Click(sender, e);
            }
        }
    }
}
