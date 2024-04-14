using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class CheckpointService
    {
        private ICheckpointRepository _checkpointRepository;

        public CheckpointService()
        {
            _checkpointRepository = new CheckpointRepository();
        }

        public List<Checkpoint> GetAll()
        {
            return _checkpointRepository.GetAll();
        }
        public List<Checkpoint> GetAllByTourScheduleId(int tourScheduleId)
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();


            foreach (Checkpoint checkpoint in GetAll())
            {
                if (checkpoint.TourScheduleId == tourScheduleId)
                {
                    checkpoints.Add(checkpoint);
                }
            }

            return checkpoints;
        }
        public List<Checkpoint> GetFinishedCheckpoints(TourSchedule schedule)
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();

            foreach (Checkpoint checkpoint in GetAllByTourScheduleId(schedule.Id))
            {
                if (checkpoint.IsReached == true)
                    checkpoints.Add(checkpoint);

            }
            return checkpoints;
        }
    }
}
