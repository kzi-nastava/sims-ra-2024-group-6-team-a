using BookingApp.Model;
using BookingApp.ViewModels.TouristViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ComplexRequest.xaml
    /// </summary>
    public partial class ComplexRequest : Window
    {
        public ComplexRequestViewModel ComplexRequestVM { get; set; }
        public ComplexRequest(User user)
        {
            InitializeComponent();
            ComplexRequestVM = new ComplexRequestViewModel(user);
            DataContext = ComplexRequestVM;
            if (ComplexRequestVM.CloseAction == null)
                ComplexRequestVM.CloseAction = new Action(() => this.Close());
        }
    }
}
