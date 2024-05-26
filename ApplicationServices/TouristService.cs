using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class TouristService
    {
        private ITouristRepository _touristRepository;

        public TouristService(ITouristRepository touristRepositoy)
        {
            _touristRepository = touristRepositoy;
        }

        public TouristService()
        {
            _touristRepository = new TouristRepository();

        }

        public static TouristService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<TouristService>();
        }
        public Tourist Save(Tourist tourist)
        {
            return _touristRepository.Save(tourist);
        }
        public Tourist Update(Tourist tourist)
        {
            return _touristRepository.Update(tourist);
        }
        public void Delete(Tourist tourist)
        {
            _touristRepository.Delete(tourist);
        }
        public List<Tourist> GetAll()
        {
            return _touristRepository.GetAll();
        }
        public Tourist GetByTouristId(int id)
        {
            return _touristRepository.GetByTouristId(id);
        }
    }
}
