using BookingApp.Domain.Model;
using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.Repository
{
    public interface ILanguageRepository
    {
        void Delete(Language language);
        List<Language> GetAll();
        Language GetById(int id);
        int NextId();
        Language Save(Language language);
    }
}