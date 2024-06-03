using BookingApp.DTOs;
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

namespace BookingApp.View.GuideView.Components
{
    /// <summary>
    /// Interaction logic for ComplexRequestCard.xaml
    /// </summary>
    /// 

    public delegate void DetailsClickEventHandler(object sender, int id);

    public partial class ComplexRequestCard : UserControl
    {


        public DetailsClickEventHandler DetailsClickEvent { get; set; }

        ComplexRequestShowDTO complexRequestShowDTO { get; set; }
        public ComplexRequestCard()
        {
            InitializeComponent();

        }

        public void DetailsClick(object sender, RoutedEventArgs e)
        {
            complexRequestShowDTO = (ComplexRequestShowDTO)DataContext;
            DetailsClickEvent?.Invoke(this, complexRequestShowDTO.Id);
        }
    }
}
