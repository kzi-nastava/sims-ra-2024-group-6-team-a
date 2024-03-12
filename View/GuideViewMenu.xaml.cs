using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuideViewMenu.xaml
    /// </summary>
    public partial class GuideViewMenu : Window, IObserver
    {
        public static ObservableCollection<TourGuideDTO> Tours { get; set; }
        public TourGuideDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }
       
        
        private  TourRepository _tourRepository;
        private  LocationRepository _locationRepository;
        private  ImageRepository _imageRepository;
        private  CheckpointRepository _checkRepository;
        private  TourScheduleRepository _tourScheduleRepository;

        public GuideViewMenu(User user,LocationRepository locationRepository,ImageRepository imageRepository)
        {
            InitializeComponent();
            DataContext = this;
           
            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = new TourRepository();
            _checkRepository = new CheckpointRepository();
            _tourScheduleRepository = new TourScheduleRepository();

            LoggedUser = user;
            Tours = new ObservableCollection<TourGuideDTO>();


            Update();
        }
        public void Update()
        {
            Tours.Clear();
            foreach (Tour tour in _tourRepository.GetByUser(LoggedUser))
            {
                Tours.Add(new TourGuideDTO(tour, _locationRepository.GetById(tour.LocationId)));
            }

        }

         private void ShowCreateTourForm(object sender, EventArgs e)
        {
            TourForm createTourForm = new TourForm(LoggedUser,_tourRepository,_locationRepository,_imageRepository,_checkRepository,_tourScheduleRepository);
            createTourForm.ShowDialog();
            Update();
        }
    }
}
