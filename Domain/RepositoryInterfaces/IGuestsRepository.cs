using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestsRepository
    {
        public List<Guest> GetAll();

        public Guest Save(Guest guest);

        public int NextId();

        public void Delete(Guest guest);

        public Guest Update(Guest guest);

        public string GetFullname(int id);

    }
}
