using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels
{
    public class AllowChangeVM
    {
        public ReservationChangeDTO reservation;
        private AccommodationReservationRepository _reservationRepository;
        private ReservationChangeService reservationChangeService;

        public AllowChangeVM(ReservationChangeDTO reservation, AccommodationReservationRepository reservationRepository, ReservationChangeService reservationChangeService)
        {
            this.reservation = reservation;
            _reservationRepository = reservationRepository;
            this.reservationChangeService = reservationChangeService;
        }


        public void YesToChange(string commentBox)
        {
            ReservationChanges newRes = reservationChangeService.GetAll().Find(c => c.ReservationId == reservation.ReservationID);
            AccommodationReservation oldRes = _reservationRepository.GetAll().Find(c => c.Id == reservation.ReservationID);

            newRes.Comment = commentBox;
            newRes.Status = BookingApp.Resources.Enums.ReservationChangeStatus.Accepted;
            oldRes.CheckInDate = newRes.NewCheckIn;
            oldRes.CheckOutDate = newRes.NewCheckOut;
            

            UpdateChanges(newRes, oldRes);

        }

        public void NoToChange(string commentBox) 
        {
            ReservationChanges newRes = reservationChangeService.GetAll().Find(c => c.ReservationId == reservation.ReservationID);
            AccommodationReservation oldRes = _reservationRepository.GetAll().Find(c => c.Id == reservation.ReservationID);

            newRes.Comment = commentBox;
            newRes.Status = BookingApp.Resources.Enums.ReservationChangeStatus.Rejected;
           

            UpdateChanges(newRes, oldRes);

        }

        public void UpdateChanges(ReservationChanges newRes,AccommodationReservation oldRes)
        {
            oldRes.Status = BookingApp.Resources.Enums.ReservationStatus.Active;
            reservationChangeService.Update(newRes);
            _reservationRepository.Update(oldRes);
        }
    }
}
