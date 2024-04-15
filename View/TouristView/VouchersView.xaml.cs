using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModels.TouristViewModel;
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

        public VouchersViewModel VouchersWindow { get; set; }
        public VouchersView(User user)
        {
            InitializeComponent();
            VouchersWindow = new VouchersViewModel(this, user);
            DataContext = VouchersWindow;
            Update();

        }

        public void Update()
        {
            VouchersWindow.Update();
        }
    }
}
