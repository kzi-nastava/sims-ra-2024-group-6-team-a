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
    /// Interaction logic for RequestForm.xaml
    /// </summary>
    public partial class RequestForm : Window
    {
        public static User LoggedUser { get; set; }
        public RequestForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;
        }

        private void SimpleRequest_Click(object sender, RoutedEventArgs e)
        {
            SimpleRequest simple = new SimpleRequest(LoggedUser);
            simple.Owner = this;
            simple.ShowDialog();
            this.Close();
        }
    }
}
