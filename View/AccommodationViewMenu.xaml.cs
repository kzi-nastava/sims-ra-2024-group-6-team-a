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
using BookingApp.Repository;
using BookingApp.DTOs;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationViewMenu.xaml
    /// </summary>
    public partial class AccommodationViewMenu : Window
    {
        public static ObservableCollection<AccommodationOwnerDTO> Accommodations { get; set; }
        public AccommodationOwnerDTO SelectedAccommodation { get; set; }
        public User User { get; set; }
        private AccommodationRepository _repository;
        private LocationRepository _locationRepository;

        public AccommodationViewMenu(User user,LocationRepository _locRepository)
        {
            InitializeComponent();

            DataContext = this;
            _locationRepository = _locRepository;

            Title = user.Username + "'s accommodations";
            User = user;
            _repository = new AccommodationRepository();
            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();

            foreach(Accommodation a in  _repository.GetByUser(User))
            {
                Accommodations.Add(new AccommodationOwnerDTO(a,_locationRepository.GetByAccommodation(a)));
            }

        }
    }
}
