using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.Model;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public User LoggedUser { get; set; }
        public Tourist tourist { get; set; }
        public Profile(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;
            tourist = TouristService.GetInstance().GetByTouristId(LoggedUser.Id);
        }

        public void Logout_Click(object sender, RoutedEventArgs e)
        {
   
            SignInForm form = new SignInForm();          
            this.Close();
            this.Owner.Close();
            form.ShowDialog();
        }
    }
}
