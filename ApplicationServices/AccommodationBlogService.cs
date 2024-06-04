using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class AccommodationBlogService
    {
        public IAccommodationBlogRepository AccommodationBlogRepository;

        public AccommodationBlogService(IAccommodationBlogRepository accommodationBlogRepository)
        {
            AccommodationBlogRepository = accommodationBlogRepository;
        }

        public static AccommodationBlogService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<AccommodationBlogService>();
        }

        public List<AccommodationBlog> GetAll()
        {
            return AccommodationBlogRepository.GetAll();
        }

        public AccommodationBlog Save(AccommodationBlog AccommodationBlog)
        {
            return AccommodationBlogRepository.Save(AccommodationBlog);
        }

        public void Delete(AccommodationBlog accommodationBlog)
        {
            AccommodationBlogRepository.Delete(accommodationBlog);
        }

        public AccommodationBlog GetById(int id)
        {
            return AccommodationBlogRepository.GetById(id);
        }

        public AccommodationBlog Update(AccommodationBlog AccommodationBlog)
        {
            return AccommodationBlogRepository.Update(AccommodationBlog);
        }

        public bool ExistsBlogWithinAccommodation(Accommodation accommodation)
        {
            foreach(AccommodationBlog accommodationBlog in AccommodationBlogRepository.GetAll())
            {
                if(accommodationBlog.AccommodationId == accommodation.Id)
                {
                    return true;
                }
            }

            return false;
        }

        

    }

}
