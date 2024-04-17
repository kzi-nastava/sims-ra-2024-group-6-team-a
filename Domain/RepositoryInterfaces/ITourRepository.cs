using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public List<Tour> GetAll();

        public Tour Save(Tour tour);

        public int NextId();

        public void Delete(Tour tour);

        public Tour Update(Tour tour);

        public List<Tour> GetAllByUser(User user);

        public Tour GetById(int id);
        public void Subscribe(IObserver observer);
    }
}
