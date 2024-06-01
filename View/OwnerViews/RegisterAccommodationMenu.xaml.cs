using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BookingApp.Repository;
using System.Runtime.CompilerServices;
using BookingApp.Resources;
using Microsoft.Win32;
using BookingApp.ViewModels;
using BookingApp.ApplicationServices;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationMenu.xaml
    /// </summary>
    /// 
    

    public partial class RegisterAccommodationMenu : Window
    {

        public RegisterAccommodationVM registerAccommodationVM;

        public RegisterAccommodationMenu( int userId)
        {
            InitializeComponent();
            
            registerAccommodationVM = new RegisterAccommodationVM(userId);
            DataContext = registerAccommodationVM;

           
            
        }

    }
}
