using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    class TourisNotificationRepository : ITourisNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/touristnotifications.csv";

        private readonly Serializer<TouristNotification> _serializer;
        private readonly List<IObserver> _observers;

        private List<TouristNotification> _notifications;

        public Subject subject;


        public TourisNotificationRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<TouristNotification>();
            _notifications = _serializer.FromCSV(FilePath);
            subject = new Subject();

        }

        public List<TouristNotification> GetAll()
        {
            _notifications = _serializer.FromCSV(FilePath);
            return _notifications;
        }

        public TouristNotification Save(TouristNotification notification)
        {
            notification.Id = NextId();
            _notifications = _serializer.FromCSV(FilePath);
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
            subject.NotifyObservers();

            return notification;
        }

        public int NextId()
        {
            _notifications = _serializer.FromCSV(FilePath);
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(x => x.Id) + 1;
        }

        public void Delete(TouristNotification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            TouristNotification found = _notifications.Find(x => x.Id == notification.Id);
            _notifications.Remove(found);
            _serializer.ToCSV(FilePath, _notifications);
            subject.NotifyObservers();
        }

        public TouristNotification Update(TouristNotification notification)
        {
            _notifications = _serializer.FromCSV(FilePath);
            TouristNotification current = _notifications.Find(x => x.Id == notification.Id);
            int index = _notifications.IndexOf(current);
            _notifications.Remove(current);
            _notifications.Insert(index, notification);
            _serializer.ToCSV(FilePath, _notifications);
            subject.NotifyObservers();

            return notification;
        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }
    }
}
