using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Serializer;
using BookingApp.Observer;

namespace BookingApp.Repository
{
    public class TourScheduleRepository
    {

        private const string FilePath = "../../../Resources/Data/tourschedules.csv";

        private readonly Serializer <TourSchedule> _serializer;

        private List<TourSchedule> _tourSchedules;

        public Subject subject;

        public TourScheduleRepository()
        {
            _serializer = new Serializer<TourSchedule>();
            _tourSchedules = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }

        public List<TourSchedule>GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourSchedule Save(TourSchedule schedule)
        {
            schedule.Id = NextId();
            _tourSchedules = _serializer.FromCSV(FilePath);
            _tourSchedules.Add(schedule);
            _serializer.ToCSV(FilePath,_tourSchedules);

            subject.NotifyObservers();
            return schedule;
        }

        public int NextId()
        {
            _tourSchedules = _serializer.FromCSV(FilePath);
            if (_tourSchedules.Count < 1)
            {
                return 1;
            }
            return _tourSchedules.Max(x => x.Id) + 1;
        }

        public void Delete(TourSchedule schedule)
        {
            _tourSchedules = _serializer.FromCSV(FilePath);
            TourSchedule found = _tourSchedules.Find(x => x.Id == schedule.Id);
            _tourSchedules.Remove(found);
            _serializer.ToCSV(FilePath, _tourSchedules);
            subject.NotifyObservers();
        }

        public TourSchedule Update(TourSchedule schedule)
        {
            _tourSchedules = _serializer.FromCSV(FilePath);
            TourSchedule current = _tourSchedules.Find(x => x.Id == schedule.Id);
            int index = _tourSchedules.IndexOf(current);
            _tourSchedules.Remove(current);
            _tourSchedules.Insert(index, schedule);
            _serializer.ToCSV(FilePath, _tourSchedules);
            subject.NotifyObservers();
            return schedule;
        }

        public TourSchedule GetByTour(Tour tour) //termini ture na osnovu Id ture
        {
            return _tourSchedules.Find(c => c.TourId == tour.Id);
        }
    }
}
