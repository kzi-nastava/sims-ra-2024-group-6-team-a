using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for OwnerInfo.xaml
    /// </summary>
    public partial class OwnerInfo : Window, IObserver
    {
        public OwnerInfoDTO ownerInfoDTO;
        
        public int AccommodationNumber;
        public OwnerInfo(OwnerInfoDTO ownerInfoDTO)
        {
            this.ownerInfoDTO = ownerInfoDTO;
            
            InitializeComponent();
            Update();
            DataContext = this.ownerInfoDTO;
        }

        public void Update()
        {
            
            
        }
    }
}
