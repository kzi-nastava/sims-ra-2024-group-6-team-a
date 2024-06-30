using BookingApp.Domain.Model;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVisitedTourRepository
    {
        public List<VisitedTour> GetAll();
        public VisitedTour Save(VisitedTour tour);

        public void Delete(VisitedTour tour);
        public int NextId();
    }
}
