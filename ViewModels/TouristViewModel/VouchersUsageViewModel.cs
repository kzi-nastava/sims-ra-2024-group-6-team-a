using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View;
using BookingApp.View.TouristView;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class VouchersUsageViewModel
    {
        public User LoggedUser { get; set; }

        public VouchersDTO SelectedVoucher { get; set; }
        public ObservableCollection<VouchersDTO> Vouchers { get; set; }

        public TourReservationFormViewModel ParentWindow { get; set; }

        public RelayCommand UseVoucherCommand { get; set; }
        public VouchersUsageViewModel(User user, TourReservationFormViewModel parentWindow)
        {
            Vouchers = new ObservableCollection<VouchersDTO>();
            this.ParentWindow = parentWindow;
            LoggedUser = user;
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand);
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

        private void Execute_UseVoucherCommand(object obj)
        {
            VouchersDTO voucher = (VouchersDTO)obj;
            ParentWindow.Voucher = voucher;
            
            

        }
    }
}
