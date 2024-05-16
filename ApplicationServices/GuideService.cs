using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class GuideService
    {
        private IGuideRepository _guideRepository;

        public GuideService(IGuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        public static GuideService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<GuideService>();
        }

        public List<Guide> GetAll()
        {
            return _guideRepository.GetAll();
        }

        public Guide Save(Guide guide)
        {
            return _guideRepository.Save(guide);
        }

        public int NextId()
        {
           return _guideRepository.NextId();    
        }

        public void Delete(Guide guide)
        {
            _guideRepository.Delete(guide);
        }

        public Guide Update(Guide guide)
        {
           return _guideRepository.Update(guide);   
        }

        public Guide GetById(int id)
        {
           return _guideRepository.GetById(id);
        }

        public Guide GetByUserId(int id)
        {
            foreach(Guide guide in GetAll())
            {
                if(guide.UserId == id)
                {
                    return guide;
                }
            }
            return null;
        }

    }
}
