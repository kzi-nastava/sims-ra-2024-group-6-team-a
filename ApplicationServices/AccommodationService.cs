using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class AccommodationService
    {
        public IAccommodationRepository AccommodationRepository;
        public IImageRepository ImageRepository;
        public ILocationRepository LocationRepository;

        public LocationService locationService;
        public ImageService imageService;

        public AccommodationService(IAccommodationRepository accommodationRepository,IImageRepository imageRepository,ILocationRepository locationRepository) 
        {
            AccommodationRepository = accommodationRepository;
            ImageRepository = imageRepository;
            LocationRepository = locationRepository;
            imageService = ImageService.GetInstance();
            locationService = LocationService.GetInstance();
        }


        public static AccommodationService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<AccommodationService>();
        }

        public int GetTotalReservationCount(int id)
        {
            int total = 0;
            Accommodation temp = new Accommodation();
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetAll())
            {
                temp = AccommodationRepository.GetByReservationId(reservation.AccommodationId);
                if (temp.OwnerId == id)
                {
                    total++;
                }
            }

            return total;
        }

        public int GetTotalAccommodationCount(int id)
        {
            int total = 0;
            foreach (Accommodation accommodation in AccommodationRepository.GetAll())
            {
                if (accommodation.OwnerId == id)
                {
                    total++;
                }
            }
            return total;
        }

        public List<Accommodation> GetAll()
        {
            return AccommodationRepository.GetAll();
        }

        public Accommodation Save(Accommodation accommodation)
        {
            return AccommodationRepository.Save(accommodation);
        }

        public List<Accommodation> GetByOwnerId(int id)
        {
            return AccommodationRepository.GetByOwnerId(id);
        }

        public Accommodation GetByReservationId(int id)
        {
            return AccommodationRepository.GetByReservationId(id);
        }

        public bool CheckIfAlreadyBooked(ReservationChanges reservationChange, Accommodation accommodation)
        {
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetAll())
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

        public ObservableCollection<ReservationOwnerDTO> GetReservationsForAccommodation(AccommodationOwnerDTO SelectedAccommodation)
        {
            ObservableCollection<ReservationOwnerDTO> ReservationsForAccommodation = new ObservableCollection<ReservationOwnerDTO>();

            foreach (AccommodationReservation reservation in AccommodationReservationService.GetInstance().GetAll())
            {
                if (reservation.AccommodationId == SelectedAccommodation.Id && reservation.Status != Enums.ReservationStatus.Changed)
                {
                    Accommodation accommodation = AccommodationRepository.GetByReservationId(SelectedAccommodation.Id);
                    String userName = GuestService.GetInstance().GetFullname(reservation.GuestId);
                    String imagePath = imageService.AddMainAccommodationImage(accommodation);
                    Location location = locationService.GetByAccommodation(accommodation);

                    ReservationOwnerDTO newReservation = new ReservationOwnerDTO(userName, reservation, SelectedAccommodation.Name, location, imagePath);

                    ReservationsForAccommodation.Add(newReservation);
                }
            }

            return ReservationsForAccommodation;
        }

        public Enums.AccommodationType GetType(bool? aptChecked, bool? cottageChecked)
        {
            if (aptChecked == true)
            {
                return Enums.AccommodationType.Apartment;
            }
            else if (cottageChecked == true)
            {
                return Enums.AccommodationType.Cottage;
            }
            else
            {
                return Enums.AccommodationType.House;
            }
        }


    }
}
