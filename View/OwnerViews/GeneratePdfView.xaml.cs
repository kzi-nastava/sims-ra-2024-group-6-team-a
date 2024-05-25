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


namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for GeneratePdfView.xaml
    /// </summary>
    public partial class GeneratePdfView : Window
    {
        public int accId {  get; set; }
        public GeneratePdfView(int id)
        {
            accId = id;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null && StartDatePicker.SelectedDate.Value <  EndDatePicker.SelectedDate.Value)
            {

                using (PdfDocument document = new PdfDocument())
                {

                    PdfPage page = document.Pages.Add();

                    PdfGraphics graphics = page.Graphics;

                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                    PdfGrid pdfGrid = new PdfGrid();

                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("Name");
                    dataTable.Columns.Add("Check in date");
                    dataTable.Columns.Add("Check out date");

                    foreach (AccommodationReservation res in AccommodationReservationService.GetInstance().GetByAccommodationId(accId))
                    {
                        DateOnly selectedStart = DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value);
                        DateOnly selectedEnd = DateOnly.FromDateTime(EndDatePicker.SelectedDate.Value);

                        if (res.CheckInDate > selectedStart && res.CheckOutDate < selectedEnd)
                        {
                            string name = GuestService.GetInstance().GetFullname(res.GuestId);

                            dataTable.Rows.Add(new object[] { name, res.CheckInDate, res.CheckOutDate });

                        }

                    }

                    pdfGrid.DataSource = dataTable;

                    pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                    pdfGrid.Draw(page, new PointF(10, 10));

                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    // Navigate up to the project root directory
                    string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\"));
                    // Define the relative path from the project root
                    string relativePath = @"Resources\owner-report.pdf";
                    // Combine the project root with the relative path
                    string fullPath = Path.Combine(projectRoot, relativePath);

                    document.Save(fullPath);

                }
                Close();
                MessageBox.Show("Generated the PDF report.");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                Close();
            }
        }
    }
}
