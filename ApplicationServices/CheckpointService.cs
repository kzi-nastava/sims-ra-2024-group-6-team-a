using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
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


        public CheckpointService(ICheckpointRepository checkpointRepository)
        {
            _checkpointRepository = checkpointRepository;
        }

        public CheckpointService()
        {
            _checkpointRepository = new CheckpointRepository();
        }


        public static CheckpointService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<CheckpointService>();
        }

        public Checkpoint Update(Checkpoint checkpoint)
        {
            return _checkpointRepository.Update(checkpoint);    
        }

        public Checkpoint Save(Checkpoint checkpoint)
        {
            return _checkpointRepository.Save(checkpoint);  
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

        public List<Checkpoint> GetAllByTourId(int tourId)
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();


            foreach (Checkpoint oldCheckpoint in GetAll().Where(c => c.TourId == tourId).ToList())
            {
                if (checkpoints.Any(c => c.Name == oldCheckpoint.Name))
                    continue;
                checkpoints.Add(oldCheckpoint);
            }

            return checkpoints;
        }

        public List<Checkpoint> GetFinishedCheckpoints(TourScheduleDTO schedule)
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();

            foreach (Checkpoint checkpoint in GetAllByTourScheduleId(schedule.Id))
            {
                if (checkpoint.IsReached == true)
                    checkpoints.Add(checkpoint);

            }
            return checkpoints;
        }

        public List<String> GetAllNamesByTourId(int tourScheduleId)
        {
            List<String> checkpointNames = new List<String>();
            List<Checkpoint>checkpoints = GetAllByTourId(tourScheduleId);
            foreach(Checkpoint checkpoint in checkpoints)
            {
                checkpointNames.Add(checkpoint.Name);
            }

            return checkpointNames;
        }

        public Checkpoint GetById(int checkpointId)
        {

            return _checkpointRepository.GetById(checkpointId);
        }

        public void SaveAll(List<String> checkpoints,int tourId, int tourScheduleId)
        {
            foreach (string checkpoint in checkpoints)
            {
                CheckpointService.GetInstance().Save(new Checkpoint(checkpoint, tourId, false, tourScheduleId));
            }
        }
       
    }
}
