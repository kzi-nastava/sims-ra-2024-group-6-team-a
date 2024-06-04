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
using BookingApp.Validation;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class TourReservationFormViewModel : ValidationBase, INotifyPropertyChanged
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

        private VouchersDTO? _voucher;
        public VouchersDTO? Voucher { 
            get
            {
                return _voucher;
            }

            set
            {
                if(_voucher != value)
                {
                    _voucher = value;
                    OnPropertyChanged("Voucher");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private TourScheduleDTO _tourSchedule;
        public TourScheduleDTO TourSchedule
        {
            get => _tourSchedule;
            set
            {
                if (_tourSchedule != value)
                {
                    _tourSchedule = value;
                    OnPropertyChanged(nameof(TourSchedule));
                    OnPropertyChanged(nameof(IsButtonEnabled));
                }
            }
        }
        private bool _isScheduleSelected;

        public bool IsScheduleSelected
        {
            get => _isScheduleSelected;
            set
            {
                if (_isScheduleSelected != value)
                {
                    _isScheduleSelected = value;
                    OnPropertyChanged(nameof(IsScheduleSelected));
                }
            }
        }


        private bool _isScheduleValid;

        private bool _isTouristInfoValid;

        public bool IsButtonEnabled => IsScheduleSelected;
        public ObservableCollection<TourScheduleDTO> TourSchedules { get; set; } = new ObservableCollection<TourScheduleDTO>();
        public ObservableCollection<TourGuestDTO> TourGuests { get; set; } = new ObservableCollection<TourGuestDTO>();
        public RelayCommand AddTouristInfoCommand { get; set; }
        public RelayCommand RemoveTouristCommand { get; set; }
        public RelayCommand SaveReservationCommand { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }
        public RelayCommand UseVoucherCommand { get; set; }
        public RelayCommand RemoveVoucherCommand { get; set; }
        public Action CloseAction { get; set; }

        public TourReservationFormViewModel(User user, TourTouristDTO selectedTour)
        {
            LoggedUser = user;
           
            AddTouristInfoCommand = new RelayCommand(Execute_AddTouristInfoCommand);
            RemoveTouristCommand = new RelayCommand(RemoveTourist);
            SaveReservationCommand = new RelayCommand(Execute_SaveReservationCommand/*, Save_canExecute*/);
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand);
            RemoveVoucherCommand = new RelayCommand(Execute_RemoveVoucherCommand);
            CancelReservationCommand = new RelayCommand(Execute_CancelReservationCommand);

            foreach (var tourSchedule in TourScheduleService.GetInstance().GetAll())
            {
                if (tourSchedule.TourId == selectedTour.Id)
                {
                    TourSchedules.Add(new TourScheduleDTO(tourSchedule));
                }
            }
            TourTouristDTO = selectedTour;
            TourReservationDTO = new TourReservationDTO();
            _isScheduleValid = false;
            _isTouristInfoValid = false;
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
        private void Execute_RemoveVoucherCommand(object parameter)
        {
            Voucher = null;
        }
        private bool Save_canExecute()
        {
            return TourSchedule != null; 
        }
        private void Execute_SaveReservationCommand(object sender)
        {
            Voucher voucher = new Voucher();
            _isScheduleValid = true;
            _isTouristInfoValid = false;
            Validate();
            if (IsValid)
            {
                if (TourReservationService.GetInstance().IsFullyBooked(TourSchedule.Id))
                {
                    SameLocationToursWindow sameLocationTours = new SameLocationToursWindow(TourSchedule, LoggedUser);
                    sameLocationTours.Owner = Application.Current.MainWindow;
                    sameLocationTours.ShowDialog();

                    return;
                }

                if (TourSchedule.CurrentFreeSpace >= GuestNumber && TourSchedule.Activity != Resources.Enums.TourActivity.Finished)
                {
                    Tourist tourist = TouristService.GetInstance().GetByTouristId(LoggedUser.Id);
                    tourist.Points++;
                    TouristService.GetInstance().Update(tourist);

                    TourReservationService.GetInstance().MakeReservation(TourSchedule, LoggedUser, TourGuests.ToList());

                    if (Voucher != null)
                    {
                        voucher.Id = Voucher.Id;
                        VoucherService.GetInstance().Delete(voucher);
                    }

                    CustomMessageBox.Show("Tour Booked successfully!");
                    CloseAction();
                    return;
                }

                AvailableSpaceMessage();
            }
        }

        protected override void ValidateSelf()
        {
            ValidationErrors.Clear();

            if (_isScheduleValid)
            {
                if (TourSchedule == null)
                    ValidationErrors["Date"] = "Date selecting is required.";
            }

            
            if (_isTouristInfoValid)
            {
                if (string.IsNullOrWhiteSpace(Name))
                    ValidationErrors[nameof(Name)] = "Name is required.";

                if (string.IsNullOrWhiteSpace(Surname))
                    ValidationErrors[nameof(Surname)] = "Surname is required.";

                if (Age <= 0)
                    ValidationErrors[nameof(Age)] = "Enter a valid age number.";
            }

            OnPropertyChanged(nameof(ValidationErrors));
        }
        private void Execute_CancelReservationCommand(object sender)
        {
            CloseAction();
        }
        private void Execute_AddTouristInfoCommand(object sender)
        {
            _isScheduleValid = false; 
            _isTouristInfoValid = true;

            Validate();
            if (IsValid)
            {
                TourGuests.Add(new TourGuestDTO(Name, Age, Surname, -1));
                ClearInputFields();
            }
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

