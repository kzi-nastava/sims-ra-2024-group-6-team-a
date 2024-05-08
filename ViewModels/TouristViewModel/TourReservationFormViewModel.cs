using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.TouristView;
using BookingApp.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using BookingApp.Domain.Model;
using System;

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

        public TourTouristDTO TourTouristDTO { get; set; }
        public User LoggedUser { get; set; }
        public TourScheduleDTO TourSchedule { get; set; }
        public VouchersDTO Voucher { get; set; }

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
        public RelayCommand AddTouristInfoCommand { get; set; }
        public RelayCommand RemoveTouristCommand { get; set; }
        public RelayCommand SaveReservationCommand { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }
        public RelayCommand UseVoucherCommand { get; set; }
        public Action CloseAction { get; set; }

        public TourReservationFormViewModel(User user, TourTouristDTO selectedTour)
        {
            LoggedUser = user;
           
            AddTouristInfoCommand = new RelayCommand(Execute_AddTouristInfoCommand);
            RemoveTouristCommand = new RelayCommand(RemoveTourist);
            SaveReservationCommand = new RelayCommand(Execute_SaveReservationCommand);
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand);
            CancelReservationCommand = new RelayCommand(Execute_CancelReservationCommand);

            TourSchedules = new ObservableCollection<TourScheduleDTO>();
            TourGuests = new ObservableCollection<TourGuestDTO>();

            foreach (var tourSchedule in TourScheduleService.GetInstance().GetAll())
            {
                if (tourSchedule.TourId == selectedTour.Id)
                {
                    TourSchedules.Add(new TourScheduleDTO(tourSchedule));
                }
            }

            TourTouristDTO = selectedTour;
            TourReservationDTO = new TourReservationDTO();

            AddUserInfo();
        }

        private void AddUserInfo()
        {
            TourGuests.Clear();
            Tourist tourist = TouristService.GetInstance().GetByTouristId(LoggedUser.Id);
            TourGuests.Add(new TourGuestDTO(tourist.Name, tourist.Age, tourist.Surname, tourist.UserId));
        }

        private void AvailableSpaceMessage()
        {
            string message = "Not enough space left. There are " + TourSchedule.CurrentFreeSpace + " spaces left. If you want to cancle the reservation say yes. If you want to change people number say no.";
            MessageBoxResult result = MessageBox.Show(message, "Error", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

        }
        private void Execute_SaveReservationCommand(object sender)
        {
            Voucher voucher = new Voucher();
            
            if (TourReservationService.GetInstance().IsFullyBooked(TourSchedule.Id))
            {
                SameLocationToursWindow sameLocationTours = new SameLocationToursWindow(TourSchedule, LoggedUser);
                sameLocationTours.Owner = Application.Current.MainWindow;
                sameLocationTours.ShowDialog();
               
                return;
            }

            if (TourSchedule.CurrentFreeSpace >= GuestNumber && TourSchedule.Activity != Resources.Enums.TourActivity.Finished)
            {
                TourReservationService.GetInstance().MakeReservation(TourSchedule, LoggedUser, TourGuests.ToList());
                if (Voucher != null)
                {
                    voucher.Id = Voucher.Id;
                    VoucherService.GetInstance().Delete(voucher);
                }
                CloseAction();
                return;
            }

            AvailableSpaceMessage();

        }

        private void Execute_CancelReservationCommand(object sender)
        {
            CloseAction();
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
        private void RemoveTourist(object parameter)
        {
            var touristToRemove = parameter as TourGuestDTO;
            TourGuests.Remove(touristToRemove);
        }

        private void Execute_UseVoucherCommand(object sender)
        {
            VoucherUsage voucherUsage = new VoucherUsage(LoggedUser, this);
            voucherUsage.Owner = Application.Current.MainWindow;
            voucherUsage.ShowDialog();
        }
    }
}

