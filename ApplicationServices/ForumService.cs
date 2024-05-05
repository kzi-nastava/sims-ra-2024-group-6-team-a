using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Resources;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static BookingApp.Resources.Enums;

namespace BookingApp.ApplicationServices
{
    public class ForumService
    {
        public IForumsRepository forumsRepository;
        public ForumService(IForumsRepository _forumsRepository)
        {
            forumsRepository = _forumsRepository;
        }
        public static ForumService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ForumService>();
        }
        public List<Forums> GetAll()
        {
            return forumsRepository.GetAll();
        }
        public Forums Save(Forums forum)
        {
            return forumsRepository.Save(forum);
        }
        public void Delete(Forums forum)
        {
            forumsRepository.Delete(forum);
        }
        public Forums Update(Forums forum)
        {
            return forumsRepository.Update(forum);
        }
    }
}
