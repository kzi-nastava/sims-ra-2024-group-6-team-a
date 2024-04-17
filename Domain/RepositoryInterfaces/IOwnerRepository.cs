using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces
{
    public  interface IOwnerRepository
    {
        public List<Owner> GetAll();
        public Owner Save(Owner Owner);
        public int NextId();
        public void Delete(Owner Owner);
        public Owner Update(Owner owner);
    }
}
