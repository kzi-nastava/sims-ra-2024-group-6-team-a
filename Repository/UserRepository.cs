using BookingApp.Model;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;

namespace BookingApp.Repository
{
    public class UserRepository
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

        public int GetIdByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.Find(u => u.Username == username).Id;
        }
    }
}
