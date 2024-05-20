using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationBlogRepository
    {
        public List<AccommodationBlog> GetAll();
        public AccommodationBlog Save(AccommodationBlog AccommodationBlog);
        public void Delete(AccommodationBlog AccommodationBlog);
        public AccommodationBlog GetById(int id);

    }
}
