using BookingApp.Domain.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    interface ITourisNotificationRepository
    {
        public List<TouristNotification> GetAll();

        public TouristNotification Save(TouristNotification notification);

        public int NextId();

        public void Delete(TouristNotification notification);

        public TouristNotification Update(TouristNotification notification);
        public void Subscribe(IObserver observer);
    }
}
