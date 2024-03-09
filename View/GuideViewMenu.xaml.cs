using BookingApp.DTOs;
using BookingApp.Model;
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
    public partial class GuideViewMenu : Window
    {
        public static ObservableCollection<TourGuideDTO> Tours { get; set; }
        public TourGuideDTO SelectedTour { get; set; }
        public User LoggedUser { get; set; }
        private readonly TourRepository _repository;
        private LocationRepository _locationRepository;




        public GuideViewMenu(User user,LocationRepository _locationRepository)
        {
            InitializeComponent();
            DataContext = this;
            _locationRepository = _locationRepository;
            LoggedUser = user;
            _repository = new TourRepository();
            Tours = new ObservableCollection<TourGuideDTO>();

            foreach(Tour tour in _repository.GetByUser(LoggedUser))
            {
                Tours.Add(new TourGuideDTO(tour, _locationRepository.GetById(tour.LocationId)));
            }

        }

         private void ShowCreateTourForm(object sender, EventArgs e)
        {
            TourForm createTourForm = new TourForm(LoggedUser);
            createTourForm.Show();
        }


    }
}
