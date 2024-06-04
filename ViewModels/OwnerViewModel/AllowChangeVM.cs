using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.ViewModels
{
    public class AllowChangeVM
    {
        public ReservationChangeDTO Reservation {  get; set; }
        public ICommand YesToChangeCommand { get; }
        public ICommand NoToChangeCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public AllowChangeVM(ReservationChangeDTO reservation)
        {
            this.Reservation = reservation;
            YesToChangeCommand = new RelayCommand(YesToChange);
            NoToChangeCommand = new RelayCommand(NoToChange);
            CloseWindowCommand = new RelayCommand(CloseWindow);

        }


        public void YesToChange(object parameter)
        {
            if (MessageBox.Show("Allow the reservation change", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string commentBox = parameter as string;
                ReservationChanges newRes = ReservationChangeService.GetInstance().GetAll().Find(c => c.ReservationId == Reservation.ReservationID);
                AccommodationReservation oldRes = AccommodationReservationService.GetInstance().GetAll().Find(c => c.Id == Reservation.ReservationID);

                newRes.Comment = commentBox;
                newRes.Status = BookingApp.Resources.Enums.ReservationChangeStatus.Accepted;
                oldRes.CheckInDate = newRes.NewCheckIn;
                oldRes.CheckOutDate = newRes.NewCheckOut;


                UpdateChanges(newRes, oldRes);
                CloseWindow(Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));
            }
        }

        public void NoToChange(object parameter) 
        {
            if (MessageBox.Show("Reject the reservation change", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string commentBox = parameter as string;
                ReservationChanges newRes = ReservationChangeService.GetInstance().GetAll().Find(c => c.ReservationId == Reservation.ReservationID);
                AccommodationReservation oldRes = AccommodationReservationService.GetInstance().GetAll().Find(c => c.Id == Reservation.ReservationID);

                newRes.Comment = commentBox;
                newRes.Status = BookingApp.Resources.Enums.ReservationChangeStatus.Rejected;


                UpdateChanges(newRes, oldRes);
                CloseWindow(Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));
            }

        }

        public void UpdateChanges(ReservationChanges newRes,AccommodationReservation oldRes)
        {
            oldRes.Status = BookingApp.Resources.Enums.ReservationStatus.Active;
            ReservationChangeService.GetInstance().Update(newRes);
            AccommodationReservationService.GetInstance().Update(oldRes);
        }

        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}

               