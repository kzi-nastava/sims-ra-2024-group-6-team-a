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
using BookingApp.Observer;
using BookingApp.Resources;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for AccommodationViewMenu.xaml
    /// </summary>
    public partial class AccommodationViewMenu : Window, IObserver
    {
        public static ObservableCollection<AccommodationOwnerDTO> Accommodations { get; set; }
        public AccommodationOwnerDTO SelectedAccommodation { get; set; }
        public User User { get; set; }
        private AccommodationRepository _repository;
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        

        public AccommodationViewMenu(User user,LocationRepository _locationRepository,ImageRepository _imageRepository)
        {
            InitializeComponent();

            DataContext = this;
            this._locationRepository = _locationRepository;
            this._imageRepository = _imageRepository;

            Title = user.Username + "'s accommodations"; // ime prozora ce biti ime vlasnika
            User = user;
            _repository = new AccommodationRepository(); 
            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            Update(); //ovo se runna posle svake promene i na startu,da bi prikazalo sve najnovije promene realtime,kao sto je dodavanje i sl.


        }

        private void RegisterAccommodation(object sender, RoutedEventArgs e) //poziva konstruktor dodavanja novog smestaja
        {
            RegisterAccommodationMenu registerAccommodationMenu = new RegisterAccommodationMenu(_repository,_locationRepository,User.Id);
            registerAccommodationMenu.ShowDialog();
            Update();
        }

        

        public void Update()
        {
            Accommodations.Clear(); //moramo da ocistimo listu dto prvo,inace se duplira
            foreach (Accommodation a in _repository.GetByUser(User))
            {
                Model.Image image = new Model.Image();
                foreach(Model.Image i in _imageRepository.GetByEntity(a.Id,Enums.ImageType.Accommodation))
                {
                    image = i;
                    break;
                }

                Accommodations.Add(new AccommodationOwnerDTO(a, _locationRepository.GetByAccommodation(a),image.Path));  
            }



            
        }



        private void DetailedView(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                List<Model.Image> images = new List<Model.Image>();
                foreach(Model.Image i in _imageRepository.GetByEntity(SelectedAccommodation.Id, Enums.ImageType.Accommodation))
                {
                    images.Add(i);
                }

               
                AccommodationImagesMenu accommodationImagesMenu = new AccommodationImagesMenu(images);
                accommodationImagesMenu.ShowDialog();

            }
        }
    }
}
