using BookingApp.Model;
using BookingApp.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class RenovationService
    {
        public IAccommodationRenovationRepository accommodationRenovationRepository;

        public RenovationService(IAccommodationRenovationRepository accommodationRenovationRepository)
        {
            this.accommodationRenovationRepository = accommodationRenovationRepository;
        }

        public static RenovationService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<RenovationService>();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return accommodationRenovationRepository.GetAll();
        }

        public AccommodationRenovation Save(AccommodationRenovation AccommodationRenovation)
        {
            return accommodationRenovationRepository.Save(AccommodationRenovation);
        }

        public void Delete(AccommodationRenovation AccommodationRenovation)
        {
            accommodationRenovationRepository.Delete(AccommodationRenovation);
        }

        public AccommodationRenovation GetByAccommodation(int id)
        {
            return accommodationRenovationRepository.GetByAccommodation(id);
        }

        public List<AccommodationRenovation> GetAllByAccommodation(int id)
        {
            List<AccommodationRenovation> accommodationRenovations = new List<AccommodationRenovation>();

            foreach(AccommodationRenovation ren in accommodationRenovationRepository.GetAll())
            {
                if(ren.AccommodationId == id)
                    accommodationRenovations.Add(ren);
            }

            return accommodationRenovations;
        }
    }
}
