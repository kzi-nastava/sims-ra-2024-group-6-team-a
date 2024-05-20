using BookingApp.Domain.RepositoryInterfaces;
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

        public static AccommodationService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<AccommodationService>();
        }

    }
}
