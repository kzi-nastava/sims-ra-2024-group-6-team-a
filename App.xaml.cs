using BookingApp.ApplicationServices;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.View;
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
            services.AddSingleton<ILocationRepository,LocationRepository>();    
            services.AddSingleton<IUserRepository, UserRepository>();
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
            services.AddSingleton<LocationService>();  
            services.AddSingleton<UserService>();   

        }


    }

}
