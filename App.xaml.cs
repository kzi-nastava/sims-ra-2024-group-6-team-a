using BookingApp.ApplicationServices;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.View;
using BookingApp.View.GuideView.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider => _serviceProvider;

        public static Dictionary<string, string> Languages = new Dictionary<string, string>
        {
            {"English", "en-US" },
            {"Serbian", "sr-RS" }
        };

        private void App_Startup(object sender, StartupEventArgs e)
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureRepositories(services);
            ConfigureServices(services);
            ConfigureWindows(services);


            _serviceProvider = services.BuildServiceProvider();
            var signInForm = _serviceProvider.GetRequiredService<SignInForm>();
            signInForm.Show();

        }

        public void  ChangeLanguage(string language)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo(Languages[language]);
        }


        private void ConfigureWindows(IServiceCollection services)
        {
            services.AddTransient<SignInForm>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {

            services.AddSingleton<ITourRepository, TourRepository>();
            services.AddSingleton<ITourReservationRepository, TourReservationRepository>();
            services.AddSingleton<ITourGuestRepository, TourGuestRepository>();
            services.AddSingleton<IReservationChangeRepository, ReservationChangeRepository>();
            services.AddSingleton<ITourReviewRepository, TourReviewRepository>();   
            services.AddSingleton<ITourScheduleRepository, TourScheduleRepository>();
            services.AddSingleton<IVoucherRepository, VoucherRepository>(); 
            services.AddSingleton<ICheckpointRepository, CheckpointRepository>();   
            services.AddSingleton<IImageRepository, ImageRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ILocationRepository,LocationRepository>(); 
            services.AddSingleton<IAccommodationRepository, AccommodationRepository>();
            services.AddSingleton<IReservationChangeRepository, ReservationChangeRepository>();
            services.AddSingleton<IOwnerRepository, OwnerRepository>();
            services.AddSingleton<IGuestReviewRepository, GuestReviewRepository>();
            services.AddSingleton<ITourisNotificationRepository, TourisNotificationRepository>();
            services.AddSingleton<IGuestsRepository, GuestRepository>();  
            services.AddSingleton<IOwnerReviewRepository, OwnerReviewRepository>();  
            services.AddSingleton<IAccommodationReservationRepository, AccommodationReservationRepository>();
            services.AddSingleton<IAccommodationRenovationRepository, AccommodationRenovationRepository>();
            services.AddSingleton<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IForumsRepository, ForumsRepository>();
            services.AddSingleton<ITouristRepository, TouristRepository>();
            services.AddSingleton<ITourRequestRepository, TourRequestRepository>();
            services.AddSingleton<IGuideRepository,GuideRepository>();
            services.AddSingleton<IAccommodationBlogRepository, AccommodationBlogRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();
            services.AddSingleton<IComplexTourRequestRepository, ComplexTourRequestRepository>();
            services.AddSingleton<IForumsCommentRepository, ForumsCommentRepository>();

}


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TourService>();
            services.AddSingleton<CheckpointService>();
            services.AddSingleton<TourGuestService>();
            services.AddSingleton<TourReservationService>();
            services.AddSingleton<TourReviewService>(); 
            services.AddSingleton<TourScheduleService>();
            services.AddSingleton<TourService>();
            services.AddSingleton<VoucherService>();
            services.AddSingleton<ImageService>();
            services.AddSingleton<UserService>();   
            services.AddSingleton<LocationService>();
            services.AddSingleton<AccommodationService>();
            services.AddSingleton<ReservationChangeService>();
            services.AddSingleton<OwnerService>();
            services.AddSingleton<GuestReviewService>();
            services.AddSingleton<TouristNotificationService>();
            services.AddSingleton<GuestService>();
            services.AddSingleton<OwnerReviewService>();
            services.AddSingleton<AccommodationReservationService>();
            services.AddSingleton<ReservationAvailableDatesService>();
            services.AddSingleton<RenovationService>();
            services.AddSingleton<LanguageService>();   
            services.AddSingleton<ForumService>();   
            services.AddSingleton<TouristService>();

            services.AddSingleton<TourRequestService>();
            services.AddSingleton<GuideService>();
            services.AddSingleton<AccommodationBlogService>();
            services.AddSingleton<CommentService>();

            //services.AddSingleton<SimpleRequestService>();
            services.AddSingleton<GuideService>();  
            services.AddSingleton<ForumsCommentService>();  
            services.AddSingleton<TourRequestService>();
            services.AddSingleton<ComplexTourRequestService>();

        }


    }

}
