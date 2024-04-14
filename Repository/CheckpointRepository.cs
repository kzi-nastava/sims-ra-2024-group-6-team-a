using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class CheckpointRepository : ICheckpointRepository
    {
        private const string FilePath = "../../../Resources/Data/checkpoints.csv";

        private readonly Serializer<Checkpoint> _serializer;

        private List<Checkpoint> _checkpoints;

        public Subject subject;

        public CheckpointRepository()
        {
            _serializer = new Serializer<Checkpoint>();
            _checkpoints = _serializer.FromCSV(FilePath);
            subject = new Subject();
        }
        public List<Checkpoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public Checkpoint Save(Checkpoint checkpoint)
        {
            checkpoint.Id = NextId();
            _checkpoints = _serializer.FromCSV(FilePath);
            _checkpoints.Add(checkpoint);
            _serializer.ToCSV(FilePath, _checkpoints);
            return checkpoint;
        }

        public int NextId()
        {
            _checkpoints = _serializer.FromCSV(FilePath);
            if (_checkpoints.Count < 1)
            {
                return 1;
            }
            return _checkpoints.Max(c => c.Id) + 1;
        }

        public void Delete(Checkpoint checkpoint)
        {
            _checkpoints = _serializer.FromCSV(FilePath);
            Checkpoint found = _checkpoints.Find(c => c.Id == checkpoint.Id);
            if (found != null)
            {
                _checkpoints.Remove(found);
            }
            _serializer.ToCSV(FilePath, _checkpoints);
        }

        public Checkpoint Update(Checkpoint checkpoint)
        {
            _checkpoints = _serializer.FromCSV(FilePath);
            Checkpoint current = _checkpoints.Find(x => x.Id == checkpoint.Id);
            int index = _checkpoints.IndexOf(current);
            _checkpoints.Remove(current);
            _checkpoints.Insert(index, checkpoint);
            _serializer.ToCSV(FilePath, _checkpoints);

            return checkpoint;
        }

        public Checkpoint GetById(int checkpointId)
        {

            return _checkpoints.Find(c => c.Id == checkpointId);
        }

        public List<Checkpoint> GetAllByTourScheduleId(int tourScheduleId)
        {
           List <Checkpoint> checkpoints = new List <Checkpoint>();
           

            foreach (Checkpoint checkpoint in GetAll())
            {
                if(checkpoint.TourScheduleId == tourScheduleId)
                {
                    checkpoints.Add(checkpoint);
                }
            }

            return checkpoints;
        }

    }
}
