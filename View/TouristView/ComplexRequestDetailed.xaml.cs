using BookingApp.DTOs;
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
    /// Interaction logic for ComplexRequestDetailed.xaml
    /// </summary>
    public partial class ComplexRequestDetailed : Window
    {
        public ComplexRequestDetailsViewModel DetailesVm { get; set; }
        public ComplexRequestDetailed(ComplexRequestDTO complexRequest)
        {
            InitializeComponent();
            DetailesVm = new ComplexRequestDetailsViewModel(complexRequest);
            DataContext = DetailesVm;
        }
    }
}
