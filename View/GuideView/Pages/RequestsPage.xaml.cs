using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.ViewModels.GuideViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        
        public RequestsViewModel RequestsViewModel { get; set; }




        public RequestsPage(User user)
        {
            InitializeComponent();
            RequestsViewModel = new RequestsViewModel(this,user);
            DataContext = RequestsViewModel;
        }

        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestsViewModel.languageComboBox_SelectionChanged();
        }
        private void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestsViewModel.locationComboBox_SelectionChanged();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            RequestsViewModel.Search_Click();
        }

        public void DatePickerBeginning_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestsViewModel.DatePickerBeginning_SelectedDateChanged();
        }
        public void DatePickerEnding_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RequestsViewModel.DatePickerEnding_SelectedDateChanged();
        }
    }
}
