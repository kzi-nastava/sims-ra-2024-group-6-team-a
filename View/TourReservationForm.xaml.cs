using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourReservationForm.xaml
    /// </summary>
    public partial class TourReservationForm : Window
        
    {
        public TourReservationDTO TourReservationDTO { get; set; }
        public TourTouristDTO TourTouristDTO { get; set; }

        public TourGuestDTO TourGuestDTO { get; set; }
        public User LoggedUser { get; set; }
        public TourReservation SelectedReservation { get; set; }
        public TourScheduleDTO TourSchedule { get; set; }

        private TourReservationRepository _tourReservationRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private UserRepository _userRepository;

        public ObservableCollection<TourScheduleDTO> tourSchedules { get; set; }
        public ObservableCollection<TourGuestDTO> tourGuests { get; set; }

        public TourReservationForm(User user, TourTouristDTO selectedTour, TourReservationRepository _tourReservationRepository, TourScheduleRepository _tourScheduleRepository)
        {
            InitializeComponent();

            LoggedUser = user;

            this._tourReservationRepository = _tourReservationRepository;
            this._tourScheduleRepository = _tourScheduleRepository;
           // this._userRepository = _userRepository;

            tourSchedules = new ObservableCollection<TourScheduleDTO>();
            tourGuests = new ObservableCollection<TourGuestDTO>();

            foreach(var tourSchedule in _tourScheduleRepository.GetAll())
            {
                if(tourSchedule.TourId == selectedTour.Id)
                {
                    tourSchedules.Add(new TourScheduleDTO(tourSchedule));
                }
            }

            DataContext = this;

            TourTouristDTO = selectedTour;
            TourReservationDTO = new TourReservationDTO();
            //TourGuestDTO = new TourGuestDTO();


        }

        private void SaveReservationClick(object sender, RoutedEventArgs e)
        {
            List<TourGuestDTO> guestList = new List<TourGuestDTO>();

            int numberOfInterestedPeople;
            if (!int.TryParse(txtGuestNumber.Text, out numberOfInterestedPeople))
            {
                MessageBox.Show("Please enter a valid number.");
                return;
            }

            if (TouristsDataGrid.Items.Count != numberOfInterestedPeople)
            {
                MessageBox.Show("Please enter tourist information for all the people as specified in the 'Number of interested people' field.");
                return;
            }

            if (!_tourReservationRepository.IsFullyBooked(TourSchedule.Id)) //nije u potpunosti popunjena
            {
                if(TourSchedule.CurrentGuestNumber >= TourReservationDTO.GuestNumber) //moguca rezervacija
                {
                    _tourReservationRepository.MakeReservation(TourSchedule, TourReservationDTO, TourTouristDTO, LoggedUser, tourGuests.ToList());
                    return;
                }

                string message = "Not enough places left. There are " + TourSchedule.CurrentGuestNumber + " places left. If you want to cancle the reservation press yes. If you want to change people number press no.";
                MessageBoxResult result = MessageBox.Show(message, "Error", MessageBoxButton.YesNo);

                // Handle the button clicks
                if (result == MessageBoxResult.Yes)
                {
                    // Exit the reservation form
                    Close();
                }
                else
                {
                    // Close the message box
                    return;
                }

            }
            else
            {
                SameLocationToursWindow sameLocationTours = new SameLocationToursWindow(TourSchedule, LoggedUser);
                sameLocationTours.ShowDialog();

            }


            Close();

        }

        private void AddTouristInfoClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(SurnameTextBox.Text) ||
                string.IsNullOrWhiteSpace(AgeTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields before adding.");
                return;
            }

            int age;
            if (!int.TryParse(AgeTextBox.Text, out age))
            {
                MessageBox.Show("Please enter a valid integer for Age.");
                AgeTextBox.BorderBrush = Brushes.Red; 
                return;
            }

            AgeTextBox.BorderBrush = SystemColors.ControlDarkBrush;
            tourGuests.Add(new TourGuestDTO(NameTextBox.Text, SurnameTextBox.Text, Convert.ToInt32(AgeTextBox.Text),0));
            
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            AgeTextBox.Text = "";
        }

        private void DeleteTouristInfoClick(object sender, RoutedEventArgs e)
        {
            if (TouristsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }
            //TouristsDataGrid.Items.Remove(TouristsDataGrid.SelectedItem);
        }

    }



}

