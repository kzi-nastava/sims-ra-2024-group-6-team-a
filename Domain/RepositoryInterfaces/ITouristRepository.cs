using BookingApp.Domain.Model;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITouristRepository
    {
        public List<Tourist> GetAll();
        public Tourist Save(Tourist tourist);
        public int NextId();
        public void Delete(Tourist tourist);
        public Tourist Update(Tourist tourist);
        public Tourist GetByTouristId(int id);
    }
}
