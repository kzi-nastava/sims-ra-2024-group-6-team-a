using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.Observer;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Domain.Model;

namespace BookingApp.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.csv";
        
        private readonly Serializer<Language> _serializer;
        private List <Language> _languages;
        public Subject subject;

        public LanguageRepository()
        {
            _serializer = new Serializer<Language>();
            _languages = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<Language> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Language Save(Language language)
        {
            language.Id = NextId();
            _languages = _serializer.FromCSV(FilePath);
            _languages.Add(language);
            _serializer.ToCSV(FilePath, _languages);

            subject.NotifyObservers();
            return language;
        }

        public int NextId()
        {
            _languages = _serializer.FromCSV(FilePath);
            if (_languages.Count < 1)
            {
                return 1;
            }
            return _languages.Max(c => c.Id) + 1;
        }


        public void Delete(Language language)
        {
            _languages = _serializer.FromCSV(FilePath);
            Language found = _languages.Find(c => c.Id == language.Id);
            if (found != null)
            {
                _languages.Remove(found);
            }
            _serializer.ToCSV(FilePath, _languages);
            subject.NotifyObservers();
        }

        public Language GetById(int id)
        {

            return _languages.Find(c => c.Id == id);
        }

    }
}
