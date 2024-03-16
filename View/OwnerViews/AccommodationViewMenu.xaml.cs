using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public static ObservableCollection<GuestReviewDTO> GuestReviews { get; set; }


        public AccommodationOwnerDTO SelectedAccommodation { get; set; }
        public GuestReviewDTO SelectedGuestReview { get; set; }


        public User User { get; set; }
        private AccommodationRepository _repository;
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private AccommodationReservationRepository _reservationRepository;
        private UserRepository _userRepository;
        private GuestReviewRepository _guestReviewRepository;
        bool existsNotReviewed = false;
        

        public AccommodationViewMenu(User user,LocationRepository _locationRepository,ImageRepository _imageRepository,AccommodationReservationRepository _reservationRepository,UserRepository _userRepository)
        {
            InitializeComponent();
            DataContext = this;


            InitiliazeRepositories(_locationRepository,  _imageRepository, _reservationRepository, _userRepository);
           

            Title = user.Username + "'s accommodations"; // ime prozora ce biti ime vlasnika
            User = user;
            

            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            GuestReviews = new ObservableCollection<GuestReviewDTO>();

            Update();
            GuestsNotReviewedNotification();



        }

        public void InitiliazeRepositories(LocationRepository _locationRepository, ImageRepository _imageRepository, AccommodationReservationRepository _reservationRepository, UserRepository _userRepository)
        {
            this._locationRepository = _locationRepository;
            this._imageRepository = _imageRepository;
            this._reservationRepository = _reservationRepository;
            this._userRepository = _userRepository;
            _repository = new AccommodationRepository();
            _guestReviewRepository = new GuestReviewRepository();
        }

        private void RegisterAccommodation(object sender, RoutedEventArgs e) //poziva konstruktor dodavanja novog smestaja
        {
            RegisterAccommodationMenu registerAccommodationMenu = new RegisterAccommodationMenu(_repository,_locationRepository,_imageRepository,User.Id);
            registerAccommodationMenu.ShowDialog();
            Update();
        }

        

        public void Update()
        {
            Accommodations.Clear(); //we must clear so it doesnt duplicate
            GuestReviews.Clear();

            foreach (Accommodation a in _repository.GetByUser(User))
            {
                foreach (AccommodationReservation r in _reservationRepository.GetByAccommodation(a))
                {
                    CheckGuestReview(a, r);  
                }
                
                string imagePath = AddMainAccommodationImage(a);

                Accommodations.Add(new AccommodationOwnerDTO(a, _locationRepository.GetByAccommodation(a),imagePath));  
            }

            

        }

        //check to see if the review exists and the guest left the premises
        public void CheckGuestReview(Accommodation accommodation,AccommodationReservation reservation)
        {
            if (reservation.CheckOutDate < DateOnly.FromDateTime(DateTime.Today))
            {
                GuestReview review = _guestReviewRepository.Get(reservation.Id);

                if (review == null)
                {
                    AddGuestReview(accommodation, reservation);
                    existsNotReviewed = true;
                }
                else
                {
                    AddGuestReview(accommodation, reservation, review.CleanlinessGrade, review.RespectGrade, review.Comment);
                }

            }
        }

        public void AddGuestReview(Accommodation accommodation, AccommodationReservation reservation, int cleanlinessGrade = 0,int respectGrade = 0,string comment = "")
        {
            string GuestOccupationPeriod = reservation.CheckInDate.ToString("dd.MM.yyyy") + " - " + reservation.CheckOutDate.ToString("dd.MM.yyyy");
            GuestReviews.Add(new GuestReviewDTO(accommodation.Name, _userRepository.GetUsername(reservation.GuestId) , cleanlinessGrade, respectGrade, comment, GuestOccupationPeriod, reservation.Id));
        }
    

        private void GuestsNotReviewedNotification()
        {
            if(existsNotReviewed)
            {
                MessageBox.Show("You have unreviewed guests!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public string AddMainAccommodationImage(Accommodation a)
        {
            Model.Image image = new Model.Image();
            foreach (Model.Image i in _imageRepository.GetByEntity(a.Id, Enums.ImageType.Accommodation))
            {
                image = i;
                return image.Path;
            }
            return null;
        }


        private void DetailedView(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (Tabs.SelectedItem == AccommodationsTab && SelectedAccommodation != null)
                {
                    DetailedAccommodationView();
                }
                else if (Tabs.SelectedItem == ReviewsTab && SelectedGuestReview != null)
                {
                    GradeEmptyReview();
                }
            }

            Update();
        }


        private void DetailedAccommodationView()
        {

            List<Model.Image> images = new List<Model.Image>();
            foreach (Model.Image i in _imageRepository.GetByEntity(SelectedAccommodation.Id, Enums.ImageType.Accommodation))
            {
                images.Add(i);
            }


            AccommodationImagesMenu accommodationImagesMenu = new AccommodationImagesMenu(images);
            accommodationImagesMenu.ShowDialog();

        }

        private void GradeEmptyReview()
        {
            
            if (SelectedGuestReview.respectGrade == 0)
            {
                ReviewGuest reviewGuest = new ReviewGuest(_guestReviewRepository, SelectedGuestReview.ReservationId);
                reviewGuest.ShowDialog();
            }
        }



        //this allows the user to do everything using keyboard buttons,in window or tabs
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                Close();
            }
            else if(e.Key == Key.R) 
            {
                RegisterAccommodation(sender,e);
            }
            else if(e.Key == Key.A)
            {
                Tabs.SelectedItem = AccommodationsTab;

                SelectFirstAccommodation();

            }
            else if(e.Key == Key.G)
            {
                Tabs.SelectedItem = ReviewsTab;

                SelectFirstReview();

            }
        }

        private void SelectFirstAccommodation()
        {
            if (SelectedAccommodation == null)
            {
                SelectedGuestReview = null;
                SelectedAccommodation = Accommodations.First();
                AccommodationsList.SelectedIndex = 0;
                AccommodationsList.UpdateLayout();
                AccommodationsList.Focus();

            }
        }

        private void SelectFirstReview()
        {
            if (SelectedGuestReview == null)
            {
                SelectedAccommodation = null;
                SelectedGuestReview = GuestReviews.First();
                ReviewsList.SelectedIndex = 0;
                ReviewsList.UpdateLayout();
                ReviewsList.Focus();
            }

        }


    }
}
