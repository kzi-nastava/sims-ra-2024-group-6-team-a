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
    /// Interaction logic for SimpleRequest.xaml
    /// </summary>
    public partial class SimpleRequest : Window
    {
        public SimpleRequestViewModel SimpleRequestVM { get; set; } 
        public SimpleRequest(User user)
        {
            InitializeComponent();
            SimpleRequestVM = new SimpleRequestViewModel(user);
            DataContext = SimpleRequestVM;
            if (SimpleRequestVM.CloseAction == null)
                SimpleRequestVM.CloseAction = new Action(() => this.Close());
        }
    }
}
