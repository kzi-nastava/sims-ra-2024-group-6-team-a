using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.TouristView;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using System.Reflection.Metadata;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class TourReservationFormViewModel : INotifyPropertyChanged
    {
        public TourReservationDTO TourReservationDTO { get; set; }
        private int _guestNumber;
        public int GuestNumber
        {
            get { return _guestNumber;}
            set{
                if (value != _guestNumber)
                {
                    _guestNumber = value;
                    OnPropertyChanged("GuestNumber");
                }
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged("Surname"); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }
        private string _personalName;
        public string PersonalName
        {
            get { return _personalName; }
            set { _personalName = value; OnPropertyChanged("PersonalName"); }
        }

        private string _personalSurname;
        public string PersonalSurname
        {
            get { return _personalSurname; }
            set { _personalSurname = value; OnPropertyChanged("PesronalSurname"); }
        }

        private int _personalAge;
        public int PersonalAge
        {
            get { return _personalAge; }
            set { _personalAge = value; OnPropertyChanged("PesronalAge"); }
        }
        public TourTouristDTO TourTouristDTO { get; set; }
        public User LoggedUser { get; set; }
        public TourScheduleDTO TourSchedule { get; set; }
        public VouchersDTO Voucher { get; set; }

        private TourReservationService _reservationService;
        private TourScheduleService _scheduleService;
        public TourReservationForm Window { get; set; }

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
        public RelayCommand AddPersonalInfoCommand { get;  set; }
        public RelayCommand AddTouristInfoCommand { get; set; }
        public RelayCommand DeleteTouristInfoCommand { get; set; }
        public RelayCommand SaveReservationCommand { get; set; }
        public RelayCommand UseVoucherCommand { get; set; }

        public TourReservationFormViewModel(TourReservationForm window,  User user, TourTouristDTO selectedTour, TourReservationService reservationService, TourScheduleService scheduleService)
        {
            Window = window;
            LoggedUser = user;

            AddPersonalInfoCommand = new RelayCommand(Execute_AddPersonalInfoCommand);
            AddTouristInfoCommand = new RelayCommand(Execute_AddTouristInfoCommand);
            DeleteTouristInfoCommand = new RelayCommand(Execute_DeleteTouristInfoCommand);
            SaveReservationCommand = new RelayCommand(Execute_SaveReservationCommand);
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand);

            this._reservationService = reservationService;
            this._scheduleService = scheduleService;

            TourSchedules = new ObservableCollection<TourScheduleDTO>();
            TourGuests = new ObservableCollection<TourGuestDTO>();

            foreach (var tourSchedule in _scheduleService.GetAll())
            {
                if (tourSchedule.TourId == selectedTour.Id)
                {
                    TourSchedules.Add(new TourScheduleDTO(tourSchedule));
                }
            }

            TourTouristDTO = selectedTour;
            TourReservationDTO = new TourReservationDTO();
        }
        private void AvailableSpaceMessage()
        {
            string message = "Not enough space left. There are " + TourSchedule.CurrentFreeSpace + " spaces left. If you want to cancle the reservation say yes. If you want to change people number say no.";
            MessageBoxResult result = MessageBox.Show(message, "Error", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

            Window.Close();
        }
        private void Execute_SaveReservationCommand(object sender)
        {
            Voucher voucher = new Voucher();
            
            if (_reservationService.IsFullyBooked(TourSchedule.Id))
            {
                SameLocationToursWindow sameLocationTours = new SameLocationToursWindow(TourSchedule, LoggedUser);
                sameLocationTours.ShowDialog();
                Window.Close();
                return;
            }

            if (TourSchedule.CurrentFreeSpace >= GuestNumber && TourSchedule.Activity != Resources.Enums.TourActivity.Finished)
            {
                _reservationService.MakeReservation(TourSchedule, LoggedUser, TourGuests.ToList());
                if (Voucher != null)
                {
                    voucher.Id = Voucher.Id;
                    VoucherService.GetInstance().Delete(voucher);
                }

                Window.Close();
                return;
            }

            AvailableSpaceMessage();
        }
        private void Execute_AddPersonalInfoCommand(object sender)
        {
            TourGuests.Add(new TourGuestDTO(PersonalName, PersonalAge, PersonalSurname, LoggedUser.Id));
            ClearInputFields();
        }
        private void Execute_AddTouristInfoCommand(object sender)
        {
            TourGuests.Add(new TourGuestDTO(Name, Age, Surname, -1));
            ClearInputFields();
        }
        private void ClearInputFields()
        {
            Name = "";
            Surname = "";
            Age = 0;
        }
        private void Execute_DeleteTouristInfoCommand(object parameter)
        {

            if (parameter is TourGuestDTO tourist)
                TourGuests.Remove(tourist);  
        }
        private void Execute_UseVoucherCommand(object sender)
        {
            VoucherUsage voucherUsage = new VoucherUsage(LoggedUser, this);
            voucherUsage.ShowDialog();
        }
    }
}

