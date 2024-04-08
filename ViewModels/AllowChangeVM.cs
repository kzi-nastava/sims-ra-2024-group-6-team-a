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
        private ReservationChangeRepository _changesRepository;

        public AllowChangeVM(ReservationChangeDTO reservation, AccommodationReservationRepository reservationRepository, ReservationChangeRepository changesRepository)
        {
            this.reservation = reservation;
            _reservationRepository = reservationRepository;
            _changesRepository = changesRepository;
        }


        public void YesToChange(string commentBox)
        {
            ReservationChanges newRes = _changesRepository.GetAll().Find(c => c.ReservationId == reservation.ReservationID);
            AccommodationReservation oldRes = _reservationRepository.GetAll().Find(c => c.Id == reservation.ReservationID);

            newRes.Comment = commentBox;
            newRes.Status = BookingApp.Resources.Enums.ReservationChangeStatus.Accepted;
            oldRes.CheckInDate = newRes.NewCheckIn;
            oldRes.CheckOutDate = newRes.NewCheckOut;

            _reservationRepository.Update(oldRes);
            _changesRepository.Update(newRes);

        }

        public void NoToChange(string commentBox) 
        {
            ReservationChanges newRes = _changesRepository.GetAll().Find(c => c.ReservationId == reservation.ReservationID);

            newRes.Comment = commentBox;
            newRes.Status = BookingApp.Resources.Enums.ReservationChangeStatus.Rejected;

            _changesRepository.Update(newRes);
        }
    }
}
