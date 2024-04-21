using BookingApp.ApplicationServices;
using BookingApp.DTOs;
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
        public ScheduleRenovation(AccommodationOwnerDTO Accommodation,List<ReservationOwnerDTO> Reservations)
        {
            this.Accommodation = Accommodation;
            this.Reservations = Reservations;
            DataContext = this;
            InitializeComponent();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            String str = StartDatePicker.SelectedDate.ToString() + " " + EndDatePicker.SelectedDate.ToString() + " " + DurationPicker.Text;
            MessageBox.Show(str);
            List<DateOnly?> availableDates = RenovationService.GetInstance().GetAvailableDatesForRenovation(Reservations, DateOnly.FromDateTime((DateTime)StartDatePicker.SelectedDate), DateOnly.FromDateTime((DateTime)EndDatePicker.SelectedDate), Convert.ToInt32(DurationPicker.Text));
            if (availableDates[0] != null)
            {
                HelpBox.Text = "There is an available period between " + ((DateOnly)availableDates[0]).ToString("dd MMMM,yyyy") + " and " +((DateOnly)availableDates[1]).ToString("dd MMMM,yyyy");
            }
            else
            {
                HelpBox.Text = "There is no available period between selected dates for the duration.";
            }
        }
    }
}
