using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class ReservationSeeMoreViewModel : INotifyPropertyChanged
    {
        public ReservationGuestDTO Reservation { get; set; }
        public Guest Guest { get; set; }
        public int _reservationId { get; set; }
        public int _minNumberOfDays { get; set; }
        public NavigationService NavService { get; set; }
        public ReservationSeeMoreView ReservationView { get; set; }
        private ImageRepository _imageRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;
        private AccommodationRepository _accommodationRepository;
        private AccommodationReservationRepository _reservationRepository;
        private ReservationChangeRepository _reservationChangeRepository;
        public List<Model.Image> ListImages { get; set; }
        public RelayCommand CancelReservationCommand { get; set; }
        public RelayCommand MoveReservationComand { get; set; }
        public RelayCommand SendRequestCommand { get; set; }
        public RelayCommand FirstDateCommand { get; set; }
        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }

        public ReservationSeeMoreViewModel(Guest guest, ReservationGuestDTO SelectedReservation, ReservationSeeMoreView reservationView, NavigationService navigation)
        {
            Guest = guest;
            Reservation = SelectedReservation;
            ReservationView = reservationView;
            NavService = navigation;
            ImageRepository _imageRepository = new ImageRepository();
            _reservationId = SelectedReservation.Id;
            _minNumberOfDays = SelectedReservation.MinNumberOfDays;
            AccommodationName = SelectedReservation.AccommodationName;
            State = SelectedReservation.State;
            City = SelectedReservation.City;
            Type = SelectedReservation.Type.ToString();
            CancelationDays = SelectedReservation.CancelationDays.ToString();

            List<Model.Image> lista = new List<Model.Image>();
            foreach (Model.Image image in _imageRepository.GetByEntity(SelectedReservation.AccommodationId, Enums.ImageType.Accommodation))
            {
                lista.Add(image);
            }
            ListImages = lista;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationRepository = new AccommodationRepository();
            _reservationChangeRepository = new ReservationChangeRepository();

            NextImageCommand = new RelayCommand(Execute_NextImageCommand);
            PreviousImageCommand = new RelayCommand(Execute_PreviousImageCommand);
            CancelReservationCommand = new RelayCommand(Execute_CancelReservationCommand);
            MoveReservationComand = new RelayCommand(Execute_MoveReservationComand);
            SendRequestCommand = new RelayCommand(Execute_SendRequestCommand);
            FirstDateCommand = new RelayCommand(Execute_FirstDateCommand);

            RequestToMoveVisibility = Visibility.Visible;
            ReservationsDataVisibility = Visibility.Collapsed;

        }
        public bool CanCancelReservation()
        {
            int cancelationDays = Reservation.CancelationDays;
            if (DateOnly.FromDateTime(DateTime.Today) <= Reservation.CheckIn.AddDays(-cancelationDays))
            {
                return true;
            }
            return false;
        }

        public void Execute_CancelReservationCommand(object obj)
        {
            AccommodationReservation canceledReservationa = _accommodationReservationRepository.GetByReservationId(_reservationId);
            if (CanCancelReservation())
            {
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to cancel the reservation?", "Cancel reservation", MessageBoxButton.YesNo);
                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        canceledReservationa.Status = Enums.ReservationStatus.Canceled;
                        _accommodationReservationRepository.Update(canceledReservationa);
                        NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
                        break;
                    default:
                        break;
                }
            }
            else MessageBox.Show("eror 404");
        }
        public void Execute_MoveReservationComand(object obj)
        {
            RequestToMoveVisibility = Visibility.Collapsed;
            ReservationsDataVisibility = Visibility.Visible;

        }
        public void Execute_FirstDateCommand(object obj)
        {
            ReservationView.LastDatePicker.IsEnabled = true;
            DateTime? firstDatePicekr = ReservationView.FirstDatePicker.SelectedDate;
            if (firstDatePicekr.HasValue) ReservationView.LastDatePicker.DisplayDateStart = firstDatePicekr.Value.AddDays(Convert.ToInt32(_minNumberOfDays));
        }
        public void Execute_SendRequestCommand(object obj)
        {
            if (ReservationView.FirstDatePicker.SelectedDate.HasValue && ReservationView.LastDatePicker.SelectedDate.HasValue) { 
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to move the reservation?", "Move reservation", MessageBoxButton.YesNo);
                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        AccommodationReservation movedReservation = _accommodationReservationRepository.GetByReservationId(_reservationId);
                        movedReservation.Status = Enums.ReservationStatus.Changed;
                        _accommodationReservationRepository.Update(movedReservation);
                        DateOnly firstDate = DateOnly.FromDateTime((DateTime)ReservationView.FirstDatePicker.SelectedDate);
                        DateOnly lastDate = DateOnly.FromDateTime((DateTime)ReservationView.LastDatePicker.SelectedDate);
                        ReservationChanges changedReservation = new ReservationChanges(Reservation.Id, Reservation.AccommodationId, Reservation.CheckIn, Reservation.CheckOut, firstDate, lastDate, "", Enums.ReservationChangeStatus.Pending);
                        _reservationChangeRepository.Save(changedReservation);
                        MessageBox.Show("Moved reservation sent!");
                        NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
                        break;
                    default:
                        break;
                }
            } else MessageBox.Show("The dates are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }
        public void Execute_NextImageCommand(object obj)
        {
            if (CurrentImageIndex < ListImages.Count - 1)
                CurrentImageIndex++;
            else CurrentImageIndex = 0;
        }
        public void Execute_PreviousImageCommand(object obj)
        {
            if (CurrentImageIndex > 0)
                CurrentImageIndex--;
            else CurrentImageIndex = ListImages.Count - 1;

        }
        private int _currentImageIndex = 0;
        public int CurrentImageIndex
        {
            get => _currentImageIndex;
            set
            {
                _currentImageIndex = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }
        public Model.Image CurrentImage => ListImages[CurrentImageIndex];


        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _state;
        public string State
        {
            get => _state;
            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _cancelationDays;
        public string CancelationDays
        {
            get => _cancelationDays;
            set
            {
                if (value != _cancelationDays)
                {
                    _cancelationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        private Visibility requestToMoveVisibility;

        public Visibility RequestToMoveVisibility
        {
            get { return requestToMoveVisibility; }
            set
            {
                if (requestToMoveVisibility != value)
                {
                    requestToMoveVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility reservationsDataVisibility;

        public Visibility ReservationsDataVisibility
        {
            get { return reservationsDataVisibility; }
            set
            {
                if (reservationsDataVisibility != value)
                {
                    reservationsDataVisibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
