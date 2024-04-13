using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class TourReservationForm : Window, INotifyPropertyChanged
        
    {
        public TourReservationDTO TourReservationDTO { get; set; }
        private int _guestNumber;
        public int GuestNumber
        {
            get
            {
                return _guestNumber;
            }
            set
            {
                if (value != _guestNumber)
                {
                    _guestNumber = value;
                    OnPropertyChanged("GuestNumber");

                }
            }

        }
        public TourTouristDTO TourTouristDTO { get; set; }
        public User LoggedUser { get; set; }
        public TourScheduleDTO TourSchedule { get; set; }
        public VouchersDTO Voucher { get; set; }

        private TourReservationRepository _tourReservationRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private UserRepository _userRepository;
        private VoucherRepository _voucherRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<TourScheduleDTO> TourSchedules { get; set; }
        public ObservableCollection<TourGuestDTO> TourGuests { get; set; }

        public TourReservationForm(User user, TourTouristDTO selectedTour, TourReservationRepository tourReservationRepository, TourScheduleRepository tourScheduleRepository)
        {
            InitializeComponent();

            LoggedUser = user;

            this._tourReservationRepository = tourReservationRepository;
            this._tourScheduleRepository = tourScheduleRepository;
            _voucherRepository = new VoucherRepository();

            TourSchedules = new ObservableCollection<TourScheduleDTO>();
            TourGuests = new ObservableCollection<TourGuestDTO>();

            foreach(var tourSchedule in _tourScheduleRepository.GetAll())
            {
                if(tourSchedule.TourId == selectedTour.Id)
                {
                    TourSchedules.Add(new TourScheduleDTO(tourSchedule));
                }
            }

            DataContext = this;

            TourTouristDTO = selectedTour;
            TourReservationDTO = new TourReservationDTO();
        }

        private bool IsReservationValid()
        {
            if (!AreAllFieldsFilled() || GuestNumber == 0)
            {
                MessageBox.Show("Please fill in all data.");
                return false;
            }
            if (TouristsDataGrid.Items.Count != GuestNumber)
            {
                MessageBox.Show("Please enter tourist information for all the people as specified in the 'Number of interested people' field.");
                return false;
            }
            return true;
        }

        private void AvailableSpaceMessage()
        {
            string message = "Not enough space left. There are " + TourSchedule.CurrentFreeSpace + " spaces left. If you want to cancle the reservation say yes. If you want to change people number say no.";
            MessageBoxResult result = MessageBox.Show(message, "Error", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            this.Close();
        }

        private void SaveReservationClick(object sender, RoutedEventArgs e)
        {
            Voucher voucher = new Voucher();
            if (!IsReservationValid())
                return;

            if(_tourReservationRepository.IsFullyBooked(TourSchedule.Id))
            {
                SameLocationToursWindow sameLocationTours = new SameLocationToursWindow(TourSchedule, LoggedUser);
                sameLocationTours.ShowDialog();
                this.Close();
                return;
            }

                if(TourSchedule.CurrentFreeSpace >= GuestNumber)
                { 
                    _tourReservationRepository.MakeReservation(TourSchedule, LoggedUser, TourGuests.ToList());
                    if (Voucher != null)
                    {
                        voucher.Id = Voucher.Id;
                        _voucherRepository.Delete(voucher);
                    }
                    
                    this.Close(); 
                    return;
                }

                AvailableSpaceMessage();
        }
        private bool AreAllFieldsFilled()
        {
            return !string.IsNullOrWhiteSpace(txtGuestNumber.Text) &&
                   ComboBox.SelectedIndex >= 0;
        }

        private void AddPersonalInfoClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PersonalNameTextBox.Text) || string.IsNullOrWhiteSpace(PersonalSurnameTextBox.Text) || string.IsNullOrWhiteSpace(PersonalAgeTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields before adding.");
                return;
            }


            TourGuests.Add(new TourGuestDTO(PersonalNameTextBox.Text,  Convert.ToInt32(PersonalAgeTextBox.Text), PersonalSurnameTextBox.Text, LoggedUser.Id));

            ClearInputFields();
        }

        private void AddTouristInfoClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(SurnameTextBox.Text) || string.IsNullOrWhiteSpace(AgeTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields before adding.");
                return;
            }

            if (!IsAgeFormatOK())
                return;

            TourGuests.Add(new TourGuestDTO(NameTextBox.Text, Convert.ToInt32(AgeTextBox.Text), SurnameTextBox.Text, - 1));

            ClearInputFields();
        }
        private bool IsAgeFormatOK()
        {
            if (!int.TryParse(AgeTextBox.Text, out int age))
            {
                MessageBox.Show("Please enter valid integer for Age.");
                return false;
            }
            return true;
        }
      
      private void ClearInputFields()
        {
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
            TourGuests.Remove((TourGuestDTO)TouristsDataGrid.SelectedItem);
           
        }

        private void DeletePesronalInfoClick(object sender, RoutedEventArgs e)
        {
            if (TouristsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }
            TourGuests.Remove((TourGuestDTO)TouristsDataGrid.SelectedItem);

        }

        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {
            VoucherUsage voucherUsage = new VoucherUsage(LoggedUser, this);
            voucherUsage.ShowDialog();
        }

    }



}

