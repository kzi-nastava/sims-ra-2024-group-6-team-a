using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels
{
    public class AccommodationMenuVM
    {
        public  ObservableCollection<AccommodationOwnerDTO> Accommodations { get; set; }
        public  ObservableCollection<GuestReviewDTO> GuestReviews { get; set; }
        public  ObservableCollection<ReservationOwnerDTO> Reservations { get; set; }
        public ObservableCollection<ReservationChangeDTO> ReservationChanges { get; set; }



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

        public AccommodationMenuVM(Owner owner, LocationRepository _locationRepository, ImageRepository _imageRepository, AccommodationReservationRepository _reservationRepository, UserRepository _userRepository, ReservationChangeRepository _reservationChangeRepository, OwnerRepository _ownerRepository)
        {
            InitiliazeRepositories(_locationRepository, _imageRepository, _reservationRepository, _userRepository, _reservationChangeRepository, _ownerRepository);

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

        public void Register()
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

                AddChangedReservations(a);

                Accommodations.Add(new AccommodationOwnerDTO(a, _locationRepository.GetByAccommodation(a), imagePath));
            }



        }

        public void CheckReservationStatus(AccommodationReservation reservation, Accommodation accommodation)
        {

            AddReservations(reservation, accommodation);

        }

        public bool CheckIfAlreadyBooked(ReservationChanges reservationChange, Accommodation accommodation)
        {
            foreach (AccommodationReservation reservation in _reservationRepository.GetAll())
            {
                if (reservation.AccommodationId == accommodation.Id && reservationChange.ReservationId != reservation.Id && DoesDateInterfere(reservation, reservationChange))
                {
                    return true;
                }
            }

            return false;
        }

        public bool DoesDateInterfere(AccommodationReservation oldR, ReservationChanges reservationChange)
        {
            if (reservationChange.NewCheckIn < oldR.CheckInDate && reservationChange.NewCheckOut < oldR.CheckInDate)
                return false;

            if (reservationChange.NewCheckIn > oldR.CheckOutDate && reservationChange.NewCheckOut > oldR.CheckOutDate)
                return false;

            return true;
        }

        public void AddChangedReservations(Accommodation accommodation)
        {
            foreach (ReservationChanges reservationChange in _reservationChangeRepository.GetAll())
            {
                if (reservationChange.AccommodationId == accommodation.Id && reservationChange.Status == Enums.ReservationChangeStatus.Pending)
                {

                    String userName = _userRepository.GetUsername(_reservationRepository.GetAll().Find(r => r.Id == reservationChange.ReservationId).GuestId);
                    String oldDate = reservationChange.OldCheckIn.ToString("dd MMMM yyyy") + "   ->   " + reservationChange.OldCheckOut.ToString("dd MMMM yyyy");
                    String newDate = reservationChange.NewCheckIn.ToString("dd MMMM yyyy") + "   ->   " + reservationChange.NewCheckOut.ToString("dd MMMM yyyy");
                    String bookedStatus = "No";
                    if (CheckIfAlreadyBooked(reservationChange, accommodation))
                        bookedStatus = "Yes";


                    ReservationChangeDTO newChange = new ReservationChangeDTO(reservationChange.ReservationId, userName, accommodation.Name, oldDate, newDate, bookedStatus);

                    ReservationChanges.Add(newChange);
                }

            }


        }


        public void AddReservations(AccommodationReservation reservation, Accommodation accommodation)
        {

            String userName = _userRepository.GetUsername(reservation.GuestId);
            String imagePath = AddMainAccommodationImage(accommodation);

            ReservationOwnerDTO newReservation = new ReservationOwnerDTO(userName, reservation, accommodation.Name, _locationRepository.GetByAccommodation(accommodation), imagePath);

            Reservations.Add(newReservation);
        }

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

        private void GuestsNotReviewedNotification()
        {
            if (existsNotReviewed)
            {
                MessageBox.Show("You have unreviewed guests!", "Notice!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void DetailedAccommodationView()
        {

            AccommodationDetailedMenu AccommodationDetailedMenu = new AccommodationDetailedMenu(GetImagesForAccommodaton(), GetReservationsForAccommodation(), SelectedAccommodation);
            AccommodationDetailedMenu.ShowDialog();

        }

        private ObservableCollection<ReservationOwnerDTO> GetReservationsForAccommodation()
        {
            ObservableCollection<ReservationOwnerDTO> ReservationsForAccommodation = new ObservableCollection<ReservationOwnerDTO>();

            foreach (AccommodationReservation reservation in _reservationRepository.GetAll())
            {
                if (reservation.AccommodationId == SelectedAccommodation.Id && reservation.Status != Enums.ReservationStatus.Changed)
                {
                    Accommodation accommodation = _repository.GetByReservationId(SelectedAccommodation.Id);
                    String userName = _userRepository.GetUsername(reservation.GuestId);
                    String imagePath = AddMainAccommodationImage(accommodation);
                    Location location = _locationRepository.GetByAccommodation(accommodation);

                    ReservationOwnerDTO newReservation = new ReservationOwnerDTO(userName, reservation, SelectedAccommodation.Name, location, imagePath);

                    ReservationsForAccommodation.Add(newReservation);
                }
            }

            return ReservationsForAccommodation;
        }

        private List<Model.Image> GetImagesForAccommodaton()
        {
            List<Model.Image> images = new List<Model.Image>();

            foreach (Model.Image i in _imageRepository.GetByEntity(SelectedAccommodation.Id, Enums.ImageType.Accommodation))
            {
                images.Add(i);
            }

            return images;
        }

        public void GradeEmptyReview()
        {

            if (SelectedGuestReview.respectGrade == 0)
            {
                ReviewGuest reviewGuest = new ReviewGuest(_guestReviewRepository, SelectedGuestReview.ReservationId);
                reviewGuest.ShowDialog();
            }
        }

        public void AllowReservationChange()
        {
            AllowReservationChange allowReservationChange = new AllowReservationChange(SelectedChange, _reservationRepository, _reservationChangeRepository);
            allowReservationChange.ShowDialog();



        }

        public void EnterOwnerInfo()
        {
            UpdateOwner();
            OwnerInfo = new OwnerInfoDTO(Owner, GetTotalReservationCount(), GetTotalAccommodationCount());
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
            if (!Owner.IsSuper && Owner.AverageGrade > 4.5)
            {
                Owner.IsSuper = true;
                _ownerRepository.Update(Owner);
            }
            else if (Owner.IsSuper && Owner.AverageGrade < 4.5)
            {
                Owner.IsSuper = false;
                _ownerRepository.Update(Owner);
            }
        }

        public int GetTotalReservationCount()
        {
            int total = 0;
            foreach (AccommodationReservation reservation in _reservationRepository.GetAll())
            {
                Accommodation temp = _repository.GetByReservationId(reservation.AccommodationId);
                if (temp.OwnerId == Owner.Id)
                {
                    total++;
                }
            }

            return total;
        }

        public int GetTotalAccommodationCount()
        {
            int total = 0;
            foreach (Accommodation accommodation in _repository.GetAll())
            {
                if (accommodation.OwnerId == Owner.Id)
                {
                    total++;
                }
            }
            return total;
        }


    }
}
