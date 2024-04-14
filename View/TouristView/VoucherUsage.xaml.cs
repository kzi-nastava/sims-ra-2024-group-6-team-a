using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for VoucherUsage.xaml
    /// </summary>
    public partial class VoucherUsage : Window
    {
        public User LoggedUser { get; set; }
        public VouchersDTO SelectedVoucher { get; set; }
        public ObservableCollection<VouchersDTO> Vouchers { get; set; }

        private VoucherService _voucherService;
        public TourReservationForm ParentWindow { get; set; }
        public VoucherUsage(User user, TourReservationForm parentWindow)
        {
            InitializeComponent();
            DataContext = this;

            _voucherService = new VoucherService();
            Vouchers = new ObservableCollection<VouchersDTO>();
            this.ParentWindow = parentWindow;
            LoggedUser = user;
            Update();

        }

        private void Update()
        {
            Vouchers.Clear();
            foreach(Voucher voucher in _voucherService.GetAllByUser(LoggedUser))
            {
                Vouchers.Add(new VouchersDTO(voucher));
            }

        }

        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {
            if(SelectedVoucher != null)
            {
                ParentWindow.Voucher = SelectedVoucher;
            }
            this.Close();
            
        }
    }
}
