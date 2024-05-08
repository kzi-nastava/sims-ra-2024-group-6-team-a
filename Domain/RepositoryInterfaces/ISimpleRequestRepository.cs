using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ISimpleRequestRepository
    {
        public List<TourRequest> GetAll();

        public TourRequest Save(TourRequest request);

        public int NextId();

        public void Delete(TourRequest request);

        public TourRequest Update(TourRequest request);
        public void Subscribe(IObserver observer);
        public TourRequest GetById(int id);
    }
}
