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
    /// Interaction logic for AccountSettingsPage.xaml
    /// </summary>
   
    

    public partial class AccountSettingsPage : Page
    {
        public AccountSettingsViewModel WindowViewModel { get; set; }

        public AccountSettingsPage(User user)
        {
            InitializeComponent();

            WindowViewModel = new AccountSettingsViewModel(this, user);
            DataContext = WindowViewModel;
            SetActiveTheme();
        }

        private void SetActiveTheme()
        {
            if (WindowViewModel.LoggedGuide.Theme == "Dark")
            {
                darkRadioButton.IsChecked = true;

            }
            else
            {
                lightRadioButton.IsChecked = true;
            }

        }


        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WindowViewModel.ChangeLanguage();

        }
        public void DarkModeChecked(object sender, RoutedEventArgs e)
        {
            WindowViewModel.DarkModeChecked();
        }


        public void LightModeChecked(object sender, RoutedEventArgs e)
        {
            WindowViewModel.LightModeChecked();
        }
    }
}
