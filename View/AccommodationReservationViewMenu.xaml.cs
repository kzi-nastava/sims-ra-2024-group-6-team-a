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
    /// Interaction logic for AccommodationReservationViewMenu.xaml
    /// </summary>
    public partial class AccommodationReservationViewMenu : Window, IObserver
    {
        private AccommodationRepository _repository;
        private LocationRepository _locationRepository;

        public static ObservableCollection<AccommodationOwnerDTO> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public string NameSearch { get; set; }
        public string CitySearch { get; set; }
        public string StateSearch { get; set; }
       // public string LocationSearch { get; set; }
        public string TypeSearch { get; set; }
        public string GuestsNumberSearch { get; set; }
        public string DaysNumberSearch { get; set; }
        public AccommodationReservationViewMenu(LocationRepository locationRepository)
        {
            InitializeComponent();
            DataContext = this;

            Title = "Accommodation registration";
            _repository = new AccommodationRepository();
            _repository.Subscribe(this);

            _locationRepository = locationRepository;

            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            Update();
        }
        public void TextboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameSearch = TextboxName.Text;
        }

        public void TextboxCity_TextChanged(object sender, TextChangedEventArgs e)
        {
           CitySearch = TextboxCity.Text;
        }

        public void TextboxState_TextChanged(object sender, TextChangedEventArgs e)
        {
            StateSearch = TextboxState.Text;
        }
      

        public void TextboxType_TextChanged(object sender, TextChangedEventArgs e)
        {
            TypeSearch = TextboxType.Text;
        }

        public void TextboxGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuestsNumberSearch = TextboxGuests.Text;
        }

        public void TextboxDays_TextChanged(object sender, TextChangedEventArgs e)
        {
            DaysNumberSearch = TextboxDays.Text;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Accommodations.Clear();


            foreach (Accommodation accommodation in _repository.GetAll())
            {
                if (CheckSearchConditions(accommodation))
                {
                    Accommodations.Add(new AccommodationOwnerDTO(accommodation, _locationRepository.GetByAccommodation(accommodation)));
                }
            }
        }

        public bool CheckSearchConditions(Accommodation accommodation)
        {
            bool ContainsName, ContainsState, ContainsCity, ContainsType, GuestsNumberIsLower, DaysNumberIsGreater;

            ContainsName = string.IsNullOrEmpty(NameSearch) ? true : accommodation.Name.ToLower().Contains(NameSearch.ToLower());
            ContainsCity = string.IsNullOrEmpty(CitySearch) ? true : _locationRepository.GetByAccommodation(accommodation).City.ToLower().Contains(CitySearch.ToLower());
            ContainsState = string.IsNullOrEmpty(StateSearch) ? true : _locationRepository.GetByAccommodation(accommodation).State.ToLower().Contains(StateSearch.ToLower());
            ContainsType = string.IsNullOrEmpty(TypeSearch) ? true : accommodation.Type.ToString().ToLower().Contains(TypeSearch.ToLower());
            GuestsNumberIsLower = string.IsNullOrEmpty(GuestsNumberSearch) ? true : Convert.ToInt32(GuestsNumberSearch) <= accommodation.MaxGuests;
            DaysNumberIsGreater = string.IsNullOrEmpty(DaysNumberSearch) ? true : Convert.ToInt32(DaysNumberSearch) >= accommodation.MinReservationDays;
            
            return ContainsName && ContainsState && ContainsCity && ContainsType && GuestsNumberIsLower && DaysNumberIsGreater;
        }
        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in _repository.GetAll())
            {
                //Accommodations.Add(accommodation);

                Accommodations.Add(new AccommodationOwnerDTO(accommodation, _locationRepository.GetByAccommodation(accommodation)));
            }
        }

    }
}
