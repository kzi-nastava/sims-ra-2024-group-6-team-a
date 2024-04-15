using BookingApp.DTOs;
using BookingApp.View.GuestViews;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Xml.Linq;
using BookingApp.ApplicationServices;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class GuestMyRequestViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GuestRequstDTO> ChangeReservations { get; set; }
        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand SeenRCommand { get; set; }
        public RelayCommand SeenACommand { get; set; }
        public GuestMyRequestViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;
            ChangeReservations = new ObservableCollection<GuestRequstDTO>();
            NavService = navigation;
            SeenRCommand = new RelayCommand(Execute_SeenRCommand);
            SeenACommand = new RelayCommand(Execute_SeenACommand);
            Update();
        }
        public void Update()
        {
            ChangeReservations.Clear();
            foreach (ReservationChanges reservationChange in ReservationChangeService.GetInstance().GetAll()) { 
                Model.Image image = new Model.Image();
                foreach (Model.Image i in ImageService.GetInstance().GetByEntity(reservationChange.AccommodationId, Enums.ImageType.Accommodation)) { 
                    image = i;
                    break;
                }
                Accommodation accommodation = AccommodationService.GetInstance().GetByReservationId(reservationChange.AccommodationId);
                ChangeReservations.Add(new GuestRequstDTO(Guest, reservationChange, accommodation, image.Path));
            }
        }
        private void Execute_SeenRCommand(object obj)
        {
            GuestRequstDTO guestRequstDTO = obj as GuestRequstDTO;
            ReservationChanges changeReservations = new ReservationChanges(guestRequstDTO.Id, guestRequstDTO.AccommodationId, guestRequstDTO.OldCheckIn, guestRequstDTO.OldCheckOut, guestRequstDTO.NewCheckIn, guestRequstDTO.NewCheckOut,guestRequstDTO.Comment, Enums.ReservationChangeStatus.RejectedSeen);
            ReservationChangeService.GetInstance().Update(changeReservations);
            NavService.Navigate(new GuestMyRequestView(Guest, NavService));
        }
        private void Execute_SeenACommand(object obj)
        {
            GuestRequstDTO guestRequstDTO = obj as GuestRequstDTO;
            ReservationChanges changeReservations = new ReservationChanges(guestRequstDTO.Id, guestRequstDTO.AccommodationId, guestRequstDTO.OldCheckIn, guestRequstDTO.OldCheckOut, guestRequstDTO.NewCheckIn, guestRequstDTO.NewCheckOut, guestRequstDTO.Comment, Enums.ReservationChangeStatus.AcceptedSeen);
            ReservationChangeService.GetInstance().Update(changeReservations);
            NavService.Navigate(new GuestMyRequestView(Guest, NavService));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
