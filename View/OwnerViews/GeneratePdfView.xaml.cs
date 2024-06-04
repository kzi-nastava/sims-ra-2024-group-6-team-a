using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using BookingApp.Model;
using BookingApp.ApplicationServices;
using Syncfusion.Pdf.Grid;
using System.Data;
using BookingApp.ViewModels.OwnerViewModel;

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for GeneratePdfView.xaml
    /// </summary>
    public partial class GeneratePdfView : Window
    {
        GeneratePdfVM vm;
        public GeneratePdfView(int id)
        {
            vm = new GeneratePdfVM(id);
            DataContext = vm;
            InitializeComponent();
        }

    }
}
