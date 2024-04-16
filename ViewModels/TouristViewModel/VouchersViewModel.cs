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
        public VouchersView Window;

        public VouchersViewModel(VouchersView window, User user)
        {
            Window = window;

            LoggedUser = user;

            Vouchers = new ObservableCollection<VouchersDTO>();

            Update();

        }

        public void Update()
        {
            Vouchers.Clear();
            foreach (Voucher voucher in VoucherService.GetInstance().GetAllByUser(LoggedUser))
            {
                Vouchers.Add(new VouchersDTO(voucher));
            }

        }
    }
}
