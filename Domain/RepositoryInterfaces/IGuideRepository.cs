using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuideRepository
    {
        public List<Guide> GetAll();
        public Guide Save(Guide guide);
        public int NextId();
        public void Delete(Guide guide);
        public Guide Update(Guide guide);

        public Guide GetById(int id);


    }
}