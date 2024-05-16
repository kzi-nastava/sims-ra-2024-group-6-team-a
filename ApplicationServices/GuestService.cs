using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ApplicationServices
{
    public class GuestService
    {
        public IGuestsRepository guestsRepository;

        public GuestService(IGuestsRepository _guestsRepository)
        {
            guestsRepository = _guestsRepository;

        }
        public GuestService()
        {

        }

        public static GuestService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<GuestService>();
        }

        public Guest Save(Guest guest)
        {
            return guestsRepository.Save(guest);
        }
        public Guest Update(Guest guest)
        {
            return guestsRepository.Update(guest);
        }
        public void Delete(Guest guest)
        {
             guestsRepository.Delete(guest);
        }
        public List<Guest> GetAll()
        {
            return guestsRepository.GetAll();
        }
        public string GetFullname(int id)
        {
            return guestsRepository.GetFullname(id);

        }
        private void RemoveSuperGuestStats(Guest Guest) {
            MessageBox.Show("pre Uso");
            Guest.IsSuperGuest = false;
            Guest.BonusPoints = 0;
            GuestService.GetInstance().Update(Guest);
        }
        private void CheckSuperGuest(Guest Guest) {
            if (AccommodationReservationService.GetInstance().GetNumberOfReservationInPreviousYear(Guest.Id) >= 10)
            { 
            AddSuperGuestsStats(Guest);
            }
            else RemoveSuperGuestStats(Guest);
        }
        private void AddSuperGuestsStats(Guest Guest) {
                MessageBox.Show("Uso");
                Guest.BonusPoints = 5;
                Guest.IsSuperGuest = true;
                Guest.StartSuperGuestDate = DateOnly.FromDateTime(DateTime.Today);
                GuestService.GetInstance().Update(Guest);
        }
        public void SuperGuestRenewal(Guest Guest) {
            MessageBox.Show("SuperGuestRenewal");

            if (DateOnly.FromDateTime(DateTime.Today) > Guest.StartSuperGuestDate.AddYears(1))
             CheckSuperGuest(Guest);
        }
    }
}
