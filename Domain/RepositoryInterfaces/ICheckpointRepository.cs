using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ICheckpointRepository
    {
        public List<Checkpoint> GetAll();
        public Checkpoint Save(Checkpoint checkpoint);

        public int NextId();
        public void Delete(Checkpoint checkpoint);
        public Checkpoint Update(Checkpoint checkpoint);
        public Checkpoint GetById(int checkpointId);

    }
}
