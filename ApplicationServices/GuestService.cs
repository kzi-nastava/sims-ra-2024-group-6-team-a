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
    }
}
