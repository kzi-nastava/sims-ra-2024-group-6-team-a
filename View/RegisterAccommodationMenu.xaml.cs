using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Runtime.CompilerServices;
using BookingApp.Resources;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationMenu.xaml
    /// </summary>
    public partial class RegisterAccommodationMenu : Window,INotifyPropertyChanged
    {
        public Accommodation accommodation;
        public AccommodationRepository accommodationRepository;
        public LocationRepository locationRepository;
        public int userId;

        public event PropertyChangedEventHandler? PropertyChanged;
        public RegisterAccommodationMenu(AccommodationRepository accommodationRepository,LocationRepository locationRepository,int userId)
        {
            InitializeComponent();
            DataContext = this;
            this.locationRepository = locationRepository;
            this.accommodationRepository = accommodationRepository;
            apt.IsChecked = true;
            this.userId = userId;
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            Enums.AccommodationType type;
            if(apt.IsChecked == true)
            {
                type = Enums.AccommodationType.Apartment;
            }
            else if(cottage.IsChecked == true)
            {
                type = Enums.AccommodationType.Cottage; 
            }
            else
            {
                type = Enums.AccommodationType.House;
            }

            Location location = new Location(State.Text,City.Text);
            location = locationRepository.Save(location);
            accommodation = new Accommodation(Name.Text,type,int.Parse(MaxGuests.Text),int.Parse(MinReservation.Text),int.Parse(CancelDays.Text),location.Id,userId);
            accommodationRepository.Save(accommodation);
            Close();



        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
