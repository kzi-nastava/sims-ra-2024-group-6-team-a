using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
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

        public TourReservationForm(User user, TourTouristDTO selectedTour, TourReservationRepository _tourReservationRepository, TourScheduleRepository _tourScheduleRepository)
        {
            InitializeComponent();

            LoggedUser = user;

            this._tourReservationRepository = _tourReservationRepository;
            this._tourScheduleRepository = _tourScheduleRepository;
           // this._userRepository = _userRepository;

            tourSchedules = new ObservableCollection<TourScheduleDTO>();

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


        }

        private void SaveReservationClick(object sender, RoutedEventArgs e)
        {
            List<TourGuestDTO> guestList = new List<TourGuestDTO>();

            for(int i = 0; i < int.Parse(txtGuestNumber.Text); i++)
            {
                WrapPanel wrapPanel = (WrapPanel)GuestNamesPanel.Children[i];
                TourGuestDTO guestDTO  = new TourGuestDTO(((TextBox)wrapPanel.Children[0]).Text, ((TextBox)wrapPanel.Children[1]).Text, Convert.ToInt32(((TextBox)wrapPanel.Children[2]).Text));
                guestList.Add(guestDTO);
            }
            _tourReservationRepository.MakeReservation(TourSchedule, TourReservationDTO, TourTouristDTO, LoggedUser, guestList);
            Close();

        }

        private void NumberOfGuestsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numberOfGuests;
            if (int.TryParse(txtGuestNumber.Text, out numberOfGuests))
            {
                GuestNamesPanel.Children.Clear(); // Clear existing textboxes
                
                for (int i = 0; i < numberOfGuests; i++)
                {
                    WrapPanel wrapPanel = new WrapPanel();
                    for (int j = 0; j < 3; j++)
                    {
                        TextBox guestNameTextBox = new TextBox();
                        guestNameTextBox.Name = "guestNameTextBox" + i;
                        guestNameTextBox.Margin = new Thickness(0, 5, 0, 0); // Add some margin between textboxes
                        guestNameTextBox.HorizontalAlignment = HorizontalAlignment.Right;
                        guestNameTextBox.VerticalAlignment = VerticalAlignment.Center;
                        guestNameTextBox.Width = 200; // Set textbox width as needed
                        guestNameTextBox.Text = "Guest " + (i + 1) + " name"; // Set default text if needed
                        wrapPanel.Children.Add(guestNameTextBox);
                    }
                    GuestNamesPanel.Children.Add(wrapPanel);
                }
            }
        }
    }

}

