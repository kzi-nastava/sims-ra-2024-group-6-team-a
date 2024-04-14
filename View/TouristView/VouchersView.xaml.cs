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
    /// Interaction logic for VouchersView.xaml
    /// </summary>
    public partial class VouchersView : Window
    {
        public ObservableCollection<VouchersDTO> Vouchers {  get; set; }
        public User LoggedUser { get; set; }

        private VoucherService _voucherService;

        public VouchersView(User user)
        {
            InitializeComponent();
            DataContext = this;

            LoggedUser = user;
            _voucherService = new VoucherService();

            Vouchers = new ObservableCollection<VouchersDTO>();
            Update();

        }

        public void Update()
        {
            Vouchers.Clear();
            foreach(Voucher voucher in _voucherService.GetAllByUser(LoggedUser))
            {
                Vouchers.Add(new VouchersDTO(voucher));
            }

        }
    }
}
