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
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class MoveReservationViewModel : INotifyPropertyChanged
    {
        public ReservationGuestDTO Reservation { get; set; }
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public MoveReservationView ReservationView { get; set; }
        private ImageRepository _imageRepository;
        private AccommodationReservationRepository _accommodationReservationRepository;
        private ReservationChangeRepository _reservationChangeRepository;
        public List<Model.Image> ListImages { get; set; }
        public RelayCommand SendRequestCommand { get; set; }
        public RelayCommand FirstDateCommand { get; set; }
        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }
        public MoveReservationViewModel(Guest guest, ReservationGuestDTO SelectedReservation, MoveReservationView reservationView, NavigationService navigation)
        {
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _reservationChangeRepository = new ReservationChangeRepository();
            _imageRepository = new ImageRepository();
            Guest = guest;
            Reservation = SelectedReservation;
            ReservationView = reservationView;
            NavService = navigation;
            List<Model.Image> lista = new List<Model.Image>();
            foreach (Model.Image image in _imageRepository.GetByEntity(SelectedReservation.AccommodationId, Enums.ImageType.Accommodation))
            {
                lista.Add(image);
            }
            ListImages = lista;
            NextImageCommand = new RelayCommand(Execute_NextImageCommand);
            PreviousImageCommand = new RelayCommand(Execute_PreviousImageCommand);
            SendRequestCommand = new RelayCommand(Execute_SendRequestCommand);
            FirstDateCommand = new RelayCommand(Execute_FirstDateCommand);
        }
        public void Execute_SendRequestCommand(object obj)
        {
            if (ReservationView.FirstDatePicker.SelectedDate.HasValue && ReservationView.LastDatePicker.SelectedDate.HasValue) { 
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to move the reservation?", "Move reservation", MessageBoxButton.YesNo);
                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        AccommodationReservation movedReservation = _accommodationReservationRepository.GetByReservationId(Reservation.Id);
                        movedReservation.Status = Enums.ReservationStatus.Changed;
                        _accommodationReservationRepository.Update(movedReservation);
                        DateOnly firstDate = DateOnly.FromDateTime((DateTime)ReservationView.FirstDatePicker.SelectedDate);
                        DateOnly lastDate = DateOnly.FromDateTime((DateTime)ReservationView.LastDatePicker.SelectedDate);
                        ReservationChanges changedReservation = new ReservationChanges(Reservation.Id, Reservation.AccommodationId, Reservation.CheckIn, Reservation.CheckOut, firstDate, lastDate, "", Enums.ReservationChangeStatus.Pending);
                        _reservationChangeRepository.Save(changedReservation);
                        NavService.Navigate(new GuestMyReservationsView(Guest, NavService));
                        break;
                    default:
                        break;
                }
            }
            else MessageBox.Show("The dates are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        public void Execute_FirstDateCommand(object obj)
        {
            ReservationView.LastDatePicker.IsEnabled = true;
            DateTime? firstDatePicekr = ReservationView.FirstDatePicker.SelectedDate;
            if (firstDatePicekr.HasValue) ReservationView.LastDatePicker.DisplayDateStart = firstDatePicekr.Value.AddDays(Convert.ToInt32(Reservation.MinNumberOfDays));
        }
        public void Execute_NextImageCommand(object obj)
        {
            if (CurrentImageIndex < ListImages.Count - 1) CurrentImageIndex++;
            else CurrentImageIndex = 0;
        }
        public void Execute_PreviousImageCommand(object obj)
        {
            if (CurrentImageIndex > 0) CurrentImageIndex--;
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
