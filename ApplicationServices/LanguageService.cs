using BookingApp.Domain.Model;
using BookingApp.Observer;
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
    public class LanguageService
    {
        private ILanguageRepository _langRepository;

        public LanguageService(ILanguageRepository langRepository)
        {
            _langRepository = langRepository;
        }

        public static LanguageService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<LanguageService>();
        }

        public List<Language> GetAll()
        {
            return _langRepository.GetAll();
        }
        public Language Save(Language language)
        {
            return _langRepository.Save(language);
        }

        public int NextId()
        {
            return _langRepository.NextId();
        }


        public void Delete(Language language)
        {
            _langRepository.Delete(language);
        }

        public Language GetById(int id)
        {
            return _langRepository.GetById(id);
        }

        public string GetNameById(int id)
        {
            foreach(Language lang in GetAll())
            {
                if(lang.Id == id)
                    return lang.Name;
            }
            return "Unknown";
        }

        public List<Language> GetAllByUserRequest(int userId)
        {
            List<Language> list = new List<Language>(); 
            foreach(TourRequest request in TourRequestService.GetInstance().GetByTouristId(userId))
            {
                list.Add(GetById(request.LanguageId));
            }
            return list;
        }
    }
}
