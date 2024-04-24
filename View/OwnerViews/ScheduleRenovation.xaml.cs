using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
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
    public partial class ScheduleRenovation : Window,INotifyPropertyChanged
    {

        List<DateOnly?> availableDates = new List<DateOnly?> ();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public AccommodationOwnerDTO Accommodation {  get; set; }
        public List<ReservationOwnerDTO> Reservations { get; set; }
        private bool isPeriodFound;

        public ScheduleRenovation(AccommodationOwnerDTO Accommodation,List<ReservationOwnerDTO> Reservations)
        {
            this.Accommodation = Accommodation;
            this.Reservations = Reservations;
            isPeriodFound = false;
            DataContext = this;
            InitializeComponent();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            int duration;
            if (StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null && int.TryParse(DurationPicker.Text, out duration))
            {
                availableDates = RenovationService.GetInstance().GetAvailableDatesForRenovation(Reservations, DateOnly.FromDateTime((DateTime)StartDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)EndDatePicker.SelectedDate), duration);
                if (availableDates[0] != null)
                {
                    HelpBox.Text = "There is an available period between " + ((DateOnly)availableDates[0]).ToString("dd MMMM,yyyy") + " and " + ((DateOnly)availableDates[1]).ToString("dd MMMM,yyyy");
                    isPeriodFound = true;
                }
                else
                {
                    HelpBox.Text = "There is no available period between selected dates for the duration.";
                    isPeriodFound = false;
                }
            }
            else
            {
                HelpBox.Text = "Please make sure you have selected dates and entered a number for the duration.";
                isPeriodFound = false;
            }

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int duration;
            if(isPeriodFound && StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null && int.TryParse(DurationPicker.Text, out duration))
            {
                if(MessageBox.Show("Confirm renovation?", "Schedule", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    
                    AccommodationRenovation renovation = new AccommodationRenovation(RenovationService.GetInstance().NextId(),TitleText.Text,Accommodation.Id, (DateOnly)availableDates[0], (DateOnly)availableDates[1]);
                    RenovationService.GetInstance().Save(renovation);
                    Close();
                }
            }
            else
            {
                HelpBox.Text = "Please find a period first using dates and a duration.";
            }
        }
    }
}
