using BookingApp.Model;
using BookingApp.ViewModels.GuideViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestView.xaml
    /// </summary>
    public partial class ComplexTourRequestView : Page
    {

        public ComplexTourRequestViewModel ComplexTourRequestViewModel { get; set; }
       
        public ComplexTourRequestView(User user)
        {
            InitializeComponent();
            ComplexTourRequestViewModel = new ComplexTourRequestViewModel(this, user);
            DataContext = ComplexTourRequestViewModel;
        }

        public void HandleDetailsClick(object sender, int id)
        {
            ComplexTourRequestViewModel.HandleDetailsClick(sender, id);          
        }

    }
}
