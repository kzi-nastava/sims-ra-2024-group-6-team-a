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
        public static ObservableCollection<ReservationOwnerDTO> Reservations { get; set; }
        public static ObservableCollection<ReservationChangeDTO> ReservationChanges { get; set; }



        public AccommodationOwnerDTO SelectedAccommodation { get; set; }
        public GuestReviewDTO SelectedGuestReview { get; set; }
        public ReservationOwnerDTO SelectedReservation { get; set; }
        public ReservationChangeDTO SelectedChange { get; set; }

        public OwnerInfoDTO OwnerInfo { get; set; }


        public Owner Owner { get; set; }
        private AccommodationRepository _repository;
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private AccommodationReservationRepository _reservationRepository;
        private UserRepository _userRepository;
        private GuestReviewRepository _guestReviewRepository;
        private ReservationChangeRepository _reservationChangeRepository;
        private OwnerRepository _ownerRepository;
        bool existsNotReviewed = false;

        

        public AccommodationViewMenu(Owner owner, LocationRepository _locationRepository, ImageRepository _imageRepository, AccommodationReservationRepository _reservationRepository, UserRepository _userRepository, ReservationChangeRepository _reservationChangeRepository,OwnerRepository _ownerRepository)
        {
            InitializeComponent();
            DataContext = this;


            InitiliazeRepositories(_locationRepository, _imageRepository, _reservationRepository, _userRepository, _reservationChangeRepository,_ownerRepository);


            Title = owner.Name + " " + owner.Surname + "'s accommodations"; // ime prozora ce biti ime vlasnika
            Owner = owner;


            Accommodations = new ObservableCollection<AccommodationOwnerDTO>();
            GuestReviews = new ObservableCollection<GuestReviewDTO>();
            Reservations = new ObservableCollection<ReservationOwnerDTO>();
            ReservationChanges = new ObservableCollection<ReservationChangeDTO>();
            
            

            Update();
            GuestsNotReviewedNotification();



        }

        public void InitiliazeRepositories(LocationRepository _locationRepository, ImageRepository _imageRepository, AccommodationReservationRepository _reservationRepository, UserRepository _userRepository, ReservationChangeRepository _reservationChangeRepository, OwnerRepository _ownerRepository)
        {
            this._locationRepository = _locationRepository;
            this._imageRepository = _imageRepository;
            this._reservationRepository = _reservationRepository;
            this._userRepository = _userRepository;
            this._reservationChangeRepository = _reservationChangeRepository;
            this._ownerRepository = _ownerRepository;
            _repository = new AccommodationRepository();
            _guestReviewRepository = new GuestReviewRepository();
        }

        private void RegisterAccommodation(object sender, RoutedEventArgs e) //poziva konstruktor dodavanja novog smestaja
        {
            RegisterAccommodationMenu registerAccommodationMenu = new RegisterAccommodationMenu(_repository, _locationRepository, _imageRepository, Owner.Id);
            registerAccommodationMenu.ShowDialog();
            Update();
        }



        public void Update()
        {
            Accommodations.Clear(); //we must clear so it doesnt duplicate
            GuestReviews.Clear();
            Reservations.Clear();
            ReservationChanges.Clear();

            foreach (Accommodation a in _repository.GetByOwnerId(Owner.Id))
            {
                foreach (AccommodationReservation r in _reservationRepository.GetByAccommodation(a))
                {
                    CheckReservationStatus(r, a);
                    CheckGuestReview(a, r);
                }

                string imagePath = AddMainAccommodationImage(a);



                Accommodations.Add(new AccommodationOwnerDTO(a, _locationRepository.GetByAccommodation(a), imagePath));
            }



        }


        public void CheckReservationStatus(AccommodationReservation reservation, Accommodation accommodation)
        {
            if (reservation.Status == Enums.ReservationStatus.Active)
                AddReservations(reservation, accommodation);
            else if (reservation.Status == Enums.ReservationStatus.Changed)
            {
                AccommodationReservation newReservation = _reservationChangeRepository.Get(reservation.Id);

                AddChangedReservations(newReservation, reservation, accommodation);
            }
        }

        public bool CheckIfAlreadyBooked(AccommodationReservation newReservation, Accommodation accommodation)
        {
            foreach (AccommodationReservation reservation in _reservationRepository.GetAll())
            {
                if (reservation.AccommodationId == accommodation.Id && newReservation.Id != reservation.Id && DoesDateInterfere(reservation, newReservation))
                {
                    return true;
                }
            }

            return false;
        }

        public bool DoesDateInterfere(AccommodationReservation oldR, AccommodationReservation newR)
        {
            if (newR.CheckInDate < oldR.CheckInDate && newR.CheckOutDate < oldR.CheckInDate)
                return false;

            if (newR.CheckInDate > oldR.CheckOutDate && newR.CheckOutDate > oldR.CheckOutDate)
                return false;

            return true;
        }

        public void AddChangedReservations(AccommodationReservation newReservation, AccommodationReservation oldReservation, Accommodation accommodation)
        {
            String userName = _userRepository.GetUsername(oldReservation.GuestId);
            String oldDate = oldReservation.CheckInDate.ToString("dd MMMM yyyy") + "   ->   " + oldReservation.CheckOutDate.ToString("dd MMMM yyyy");
            String newDate = newReservation.CheckInDate.ToString("dd MMMM yyyy") + "   ->   " + newReservation.CheckOutDate.ToString("dd MMMM yyyy");
            String bookedStatus = "No";
            if (CheckIfAlreadyBooked(newReservation, accommodation))
                bookedStatus = "Yes";


            ReservationChangeDTO newChange = new ReservationChangeDTO(newReservation.Id,userName, accommodation.Name, oldDate, newDate, bookedStatus);

            ReservationChanges.Add(newChange);

        }


        public void AddReservations(AccommodationReservation reservation, Accommodation accommodation)
        {
            if (reservation.CheckOutDate > DateOnly.FromDateTime(DateTime.Now))
            {
                String userName = _userRepository.GetUsername(reservation.GuestId);
                String imagePath = AddMainAccommodationImage(accommodation);

                ReservationOwnerDTO newReservation = new ReservationOwnerDTO(userName, reservation, accommodation.Name, _locationRepository.GetByAccommodation(accommodation), imagePath);

                Reservations.Add(newReservation);
            }
        }

        //check to see if the review exists and the guest left the premises,adds empty reviews if only less than 5 days passed,and all filled reviews
        public void CheckGuestReview(Accommodation accommodation, AccommodationReservation reservation)
        {
            DateTime GuestCheckout = reservation.CheckOutDate.ToDateTime(TimeOnly.MinValue);
            double DaysPassedForReview = (DateTime.Today - GuestCheckout).TotalDays;

            if (DaysPassedForReview >= 0)
            {
                GuestReview review = _guestReviewRepository.Get(reservation.Id);

                if (review == null && DaysPassedForReview < 5)
                {
                    AddGuestReview(accommodation, reservation);
                    existsNotReviewed = true;
                }
                else if (review != null)
                {
                    AddGuestReview(accommodation, reservation, review.CleanlinessGrade, review.RespectGrade, review.Comment);
                }

            }
        }

        public void AddGuestReview(Accommodation accommodation, AccommodationReservation reservation, int cleanlinessGrade = 0, int respectGrade = 0, string comment = "")
        {
            string GuestOccupationPeriod = reservation.CheckInDate.ToString("dd.MM.yyyy") + " - " + reservation.CheckOutDate.ToString("dd.MM.yyyy");
            GuestReviews.Add(new GuestReviewDTO(accommodation.Name, _userRepository.GetUsername(reservation.GuestId), cleanlinessGrade, respectGrade, comment, GuestOccupationPeriod, reservation.Id));
        }


        private void GuestsNotReviewedNotification()
        {
            if (existsNotReviewed)
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
                SelectedDetailed(sender, e);
            }
            
        }

   

        public void EnterDetailedView()
        {
            if (Tabs.SelectedItem == AccommodationsTab && SelectedAccommodation != null)
            {
                DetailedAccommodationView();
            }
            else if (Tabs.SelectedItem == ReviewsTab && SelectedGuestReview != null)
            {
                GradeEmptyReview();
            }
            else if (Tabs.SelectedItem == ReservationChangesTab && SelectedChange != null)
            {
                
                AllowReservationChange();
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

        private void AllowReservationChange()
        {
            AllowReservationChange allowReservationChange = new AllowReservationChange(SelectedChange,_reservationRepository,_reservationChangeRepository);
            allowReservationChange.ShowDialog();



        }





        //this allows the user to do everything using keyboard buttons,in window or tabs
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                Close();
            }
            else if(e.Key == Key.F1) 
            {
                RegisterAccommodation(sender,e);
            }
            else if(e.Key == Key.F2)
            {
                EnterOwnerInfo(sender,e);
            }
            else if (e.Key == Key.F3)
            {
                SelectedDetailed(sender,e);
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
            else if(e.Key == Key.R) 
            {
                Tabs.SelectedItem = ReservationsTab;

                SelectFirstReservation();
            }
            else if(e.Key == Key.C)
            {
                Tabs.SelectedItem = ReservationChangesTab;

                SelectFirstResChange();
            }
        }

        private void SelectFirstAccommodation()
        {
            if (SelectedAccommodation == null)
            {
                SelectedGuestReview = null;
                SelectedReservation = null;
                SelectedChange = null;
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
                SelectedReservation = null;
                SelectedChange = null;
                SelectedGuestReview = GuestReviews.First();
                ReviewsList.SelectedIndex = 0;
                ReviewsList.UpdateLayout();
                ReviewsList.Focus();
                
            }

        }

        private void SelectFirstReservation()
        {
            if (SelectedReservation == null)
            {
                SelectedGuestReview = null;
                SelectedAccommodation = null;
                SelectedChange = null;
                SelectedReservation = Reservations.First();
                ReservationsList.SelectedIndex = 0;
                ReservationsList.UpdateLayout();
                ReservationsList.Focus();

            }
        }


        private void SelectFirstResChange()
        {
            if (SelectedChange == null)
            {
                SelectedGuestReview = null;
                SelectedAccommodation = null;
                SelectedReservation = null;
                SelectedChange = ReservationChanges.First();
                ReservationChangesGrid.SelectedIndex = 0;
                ReservationChangesGrid.UpdateLayout();
                ReservationChangesGrid.Focus();
 

            }
        }



        private void EnterOwnerInfo(object sender, RoutedEventArgs e)
        {
            UpdateOwner();
            OwnerInfo = new OwnerInfoDTO(Owner,GetTotalReservationCount(),GetTotalAccommodationCount());
            OwnerInfo ownerInfo = new OwnerInfo(OwnerInfo);
            ownerInfo.ShowDialog();

        }

        private void UpdateOwner()
        {
            
            UpdateOwnerStatus();
        }

        private void UpdateOwnerStatus()
        {
            //dodati i proveru da postoji 50 ocena,iz csv
            if(!Owner.IsSuper && Owner.AverageGrade > 4.5)
            {
                Owner.IsSuper = true;
                _ownerRepository.Update(Owner);
            }
            else if(Owner.IsSuper && Owner.AverageGrade <  4.5)
            {
                 Owner.IsSuper= false;
                _ownerRepository.Update(Owner);
            }
        }

        public int GetTotalReservationCount()
        {
            int total = 0;
            foreach(AccommodationReservation reservation in _reservationRepository.GetAll())
            {
                Accommodation temp = _repository.GetByReservationId(reservation.AccommodationId);
                if(temp.OwnerId == Owner.Id)
                {
                    total++;
                }
            }

            return total;
        }

        public int GetTotalAccommodationCount()
        {
            int total = 0;
            foreach(Accommodation accommodation in _repository.GetAll())
            {
                if(accommodation.OwnerId == Owner.Id)
                {
                    total++;
                }
            }
            return total;
        }

        private void SelectedDetailed(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null && SelectedGuestReview == null && SelectedChange == null)
                MessageBox.Show("You have not selected an item", "Warning", MessageBoxButton.OK);
            else
                EnterDetailedView();
        }


    }
}
