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
using System.Windows.Shapes;
using BookingApp.DTOs;
using BookingApp.Observer;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationImagesMenu.xaml
    /// </summary>
    public partial class AccommodationImagesMenu : Window,IObserver
    {
        public static ObservableCollection<ImageDTO> Images { get; set; }
        public static List<Model.Image> imageModels {  get; set; }
        public AccommodationImagesMenu(List<Model.Image> images)
        {
            
            InitializeComponent();
            DataContext = this;
            Images = new ObservableCollection<ImageDTO>();
            imageModels = images;

            Update();

       
        }

        public void Update()
        {
            Images.Clear();

            foreach(Model.Image i in imageModels)
            {
                Images.Add(new ImageDTO(i));
            }

 
        }


    }
}
