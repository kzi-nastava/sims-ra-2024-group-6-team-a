using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Observer;
using BookingApp.Resources;
using BookingApp.Model;
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
using System.ComponentModel;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TouristViewMenu.xaml
    /// </summary>
    public partial class TouristViewMenu : Window, IObserver
    {

        public static ObservableCollection<TourGuideDTO> Tours {  get; set; }
        public TourGuideDTO SelectedTour { get; set; }
        public User User { get; set; }
        private LocationRepository _locationRepository;
        private TourRepository _tourRepository;
        private ImageRepository _imageRepository;

        public TouristViewMenu(User user, LocationRepository _locationRepository, ImageRepository _imageRepository)
        {
            InitializeComponent();
            DataContext = this;
            this._locationRepository = _locationRepository;
            this._imageRepository = _imageRepository;
            User = user;
            
            _tourRepository = new TourRepository();
            Tours = new ObservableCollection<TourGuideDTO>();
            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach(Tour t in _tourRepository.GetByUser(User)) 
            {
                Model.Image image = new Model.Image();
                foreach (Model.Image i in _imageRepository.GetByEntity(t.Id, Enums.ImageType.Tour))
                {
                    image = i;
                    break;
                }
                Tours.Add(new TourGuideDTO(t, _locationRepository.GetById(t.LocationId), image.Path));
            }

            TourList.ItemsSource = Tours;
        }

       
    }
}
