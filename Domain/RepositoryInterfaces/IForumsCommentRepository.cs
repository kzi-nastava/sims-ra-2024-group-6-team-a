using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IForumsCommentRepository
    {
        public List<ForumsComment> GetAll();
        public ForumsComment Save(ForumsComment reservation);
        public void Delete(ForumsComment reservation);
        public ForumsComment Update(ForumsComment AccommodationReservation);
        public ForumsComment Get(int id);

    }
}
