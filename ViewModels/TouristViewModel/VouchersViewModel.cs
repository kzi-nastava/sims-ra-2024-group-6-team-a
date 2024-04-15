using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class VouchersViewModel
    {
        public ObservableCollection<VouchersDTO> Vouchers { get; set; }
        public User LoggedUser { get; set; }

        private VoucherService _voucherService;
        public VouchersView Window;

        public VouchersViewModel(VouchersView window, User user)
        {
            Window = window;

            LoggedUser = user;
            _voucherService = new VoucherService();

            Vouchers = new ObservableCollection<VouchersDTO>();

            Update();

        }

        public void Update()
        {
            Vouchers.Clear();
            foreach (Voucher voucher in _voucherService.GetAllByUser(LoggedUser))
            {
                Vouchers.Add(new VouchersDTO(voucher));
            }

        }
    }
}
