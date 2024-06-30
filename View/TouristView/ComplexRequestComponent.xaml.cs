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
    /// Interaction logic for ComplexRequestComponent.xaml
    /// </summary>
    public partial class ComplexRequestComponent : Window
    {
        public ComplexRequestComponentViewModel componentVM { get; set; }
        public ComplexRequestComponent(User user, ComplexRequestViewModel parentWindow)
        {
            InitializeComponent();
            componentVM = new ComplexRequestComponentViewModel(user, parentWindow);
            DataContext = componentVM;
            if (componentVM.CloseAction == null)
                componentVM.CloseAction = new Action(() => this.Close());
        }
    }
}
