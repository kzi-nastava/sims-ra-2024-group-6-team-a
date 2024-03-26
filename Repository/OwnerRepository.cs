using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace BookingApp.Repository
{
    public class OwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/owners.csv";

        private readonly Serializer<Owner> _serializer;
        private readonly List<IObserver> _observers;

        private List<Owner> _owners;
        public Subject subject;

        public OwnerRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<Owner>();
            _owners = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Owner> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Owner Save(Owner Owner)
        {
            Owner.Id = NextId();
            _owners = _serializer.FromCSV(FilePath);
            _owners.Add(Owner);
            _serializer.ToCSV(FilePath, _owners);
            subject.NotifyObservers();
            return Owner;
        }

        public int NextId()
        {
            _owners = _serializer.FromCSV(FilePath);
            if (_owners.Count < 1)
            {
                return 1;
            }
            return _owners.Max(c => c.Id) + 1;
        }

        public void Delete(Owner Owner)
        {
            _owners = _serializer.FromCSV(FilePath);
            Owner founded = _owners.Find(c => c.Id == Owner.Id);
            _owners.Remove(founded);
            _serializer.ToCSV(FilePath, _owners);
            subject.NotifyObservers();
        }

        public Owner Update(Owner owner)
        {
            _owners = _serializer.FromCSV(FilePath);
            Owner current = _owners.Find(c => c.Id == owner.Id);
            int index = _owners.IndexOf(current);
            _owners.Remove(current);
            _owners.Insert(index, owner);       
            _serializer.ToCSV(FilePath, _owners);
            subject.NotifyObservers();
            return owner;
        }

    }
}
