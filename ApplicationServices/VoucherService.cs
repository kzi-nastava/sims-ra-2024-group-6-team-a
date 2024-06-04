using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class VoucherService
    {
        private IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public static VoucherService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<VoucherService>();
        }

        public void Delete(Voucher voucher)
        {
            _voucherRepository.Delete(voucher);
        }
        public void Save(Voucher voucher)
        {
            _voucherRepository.Save(voucher);
        }

        public List<Voucher> GetAllByUser(User user)
        {
            List<Voucher> vouchers = new List<Voucher>();

            foreach (Voucher voucher in _voucherRepository.GetAll())
            {
                if (voucher.UserId == user.Id && voucher.IssuingDate > DateTime.Now)
                {
                    vouchers.Add(voucher);
                }
            }
            return vouchers;
        }

        public void SaveAllGuests(List<TourGuests> guests)
        {

            foreach (TourGuests tourGuest in guests)
            {
                TourReservation tourReservation = TourReservationService.GetInstance().GetById(tourGuest.ReservationId);
                Voucher voucher = new Voucher();
                voucher.TouristName = tourGuest.Name;
                voucher.TouristSurname = tourGuest.Surname;
                voucher.TouristBirth = tourGuest.Age;
                voucher.IssuingDate = DateTime.Now.AddYears(1);
                voucher.ReceivingDate = DateTime.Now;
                voucher.UserId = tourGuest.UserTypeId;
                _voucherRepository.Save(voucher);
            }
        }
    }
}
