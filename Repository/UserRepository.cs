using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;

namespace BookingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public string GetUsername(int id) 
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.Find(u => u.Id == id).Username;
        }

        public User GetById(int id)
        {
            return _users.Find(c => c.Id == id);
        }


        public void Delete(User user)
        {
            _users = _serializer.FromCSV(FilePath);
            User found = _users.Find(x => x.Id == user.Id);
            _users.Remove(found);
            _serializer.ToCSV(FilePath, _users);
        }
    }
}
