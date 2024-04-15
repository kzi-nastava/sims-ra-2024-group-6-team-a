using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{

    
    public class UserService
    {

        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public static UserService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<UserService>();
        }

        public User GetByUsername(string username)
        {
          return  _userRepository.GetByUsername(username);
        }

        public string GetUsername(int id)
        {
          return  _userRepository.GetUsername(id);
        }
    }
}
