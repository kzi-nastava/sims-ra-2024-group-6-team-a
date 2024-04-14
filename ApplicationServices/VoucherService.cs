using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class VoucherService
    {
        private IVoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = new VoucherRepository();
        }

        public void Delete(Voucher voucher)
        {
            _voucherRepository.Delete(voucher);
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
    }
}
