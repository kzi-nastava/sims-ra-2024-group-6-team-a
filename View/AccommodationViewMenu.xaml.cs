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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationViewMenu.xaml
    /// </summary>
    public partial class AccommodationViewMenu : Window
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public User User { get; set; }
        private AccommodationRepository _repository;

        public AccommodationViewMenu(User user)
        {
            InitializeComponent();

            DataContext = this;

            Title = user.Username + "'s accommodations";
            User = user;
            _repository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_repository.GetByUser(user));
        }
    }
}
