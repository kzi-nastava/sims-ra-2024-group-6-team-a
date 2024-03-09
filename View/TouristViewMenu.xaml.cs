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

        public static ObservableCollection<TourTouristDTO> Tours {  get; set; }
        public TourTouristDTO SelectedTour { get; set; }

        public string NameSearch { get; set; }
        public string CitySearch { get; set; }
        public string StateSearch { get; set; }

        public string LanguageSearch { get; set; }
        public string CapacitySearch { get; set; }
        public string DurationSearch { get; set; }


        private LocationRepository _locationRepository;
        private TourRepository _repository;
        private ImageRepository _imageRepository;

        public TouristViewMenu( LocationRepository _locationRepository, ImageRepository _imageRepository)
        {
            InitializeComponent();
            DataContext = this;

            this._locationRepository = _locationRepository;
            this._imageRepository = _imageRepository; // will be used later on for more complex GUI 
            
            _repository = new TourRepository();
            _repository.Subscribe(this);
            
            Tours = new ObservableCollection<TourTouristDTO>();
            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach(Tour tour in _repository.GetAll()) 
            {
               
                Tours.Add(new TourTouristDTO(tour, _locationRepository.GetById(tour.LocationId)));
            }

            
        }

        private void TextboxCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            CitySearch = TextboxCity.Text;

        }

        private void TextboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameSearch = TextboxName.Text;
        }

        private void TextboxState_TextChanged(object sender, TextChangedEventArgs e)
        {
            StateSearch = TextboxState.Text;
        }

        private void TextboxDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            DurationSearch= TextboxDuration.Text;
        }

        private void TextboxLanguage_TextChanged(object sender, TextChangedEventArgs e)
        {
            LanguageSearch = TextboxLanguage.Text;
        }

        private void TextboxCapacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            CapacitySearch = TextboxCapacity.Text;
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            Tours.Clear();


            foreach (Tour tour in _repository.GetAll())
            {
                if (CheckSearchConditions(tour))
                {
                    Tours.Add(new TourTouristDTO(tour, _locationRepository.GetById(tour.LocationId)));
                }
            }
        }

        public bool CheckSearchConditions(Tour tour)
        {
            bool ContainsName, ContainsState, ContainsCity, ContainsDuration, ContainsLanguage, CapacityIsLower;

            ContainsName = string.IsNullOrEmpty(NameSearch) ? true : tour.Name.ToLower().Contains(NameSearch.ToLower());
            ContainsCity = string.IsNullOrEmpty(CitySearch) ? true : _locationRepository.GetById(tour.LocationId).City.ToLower().Contains(CitySearch.ToLower());
            ContainsState = string.IsNullOrEmpty(StateSearch) ? true : _locationRepository.GetById(tour.LocationId).State.ToLower().Contains(StateSearch.ToLower());
            ContainsLanguage = string.IsNullOrEmpty(LanguageSearch) ? true : tour.Language.ToString().ToLower().Contains(LanguageSearch.ToLower());
            CapacityIsLower = string.IsNullOrEmpty(CapacitySearch) ? true : Convert.ToInt32(CapacitySearch) <= tour.Capacity;
            ContainsDuration = string.IsNullOrEmpty(DurationSearch) ? true : Convert.ToDouble(DurationSearch) == tour.Duration;

            return ContainsName && ContainsState && ContainsCity && ContainsDuration && CapacityIsLower && ContainsLanguage;
        }
    }
}
