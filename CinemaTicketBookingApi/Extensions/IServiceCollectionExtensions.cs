using AutoMapper;
using CinemaTicketBookingApi.Data;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.Helpers;
using CinemaTicketBookingApi.Implementations;
using CinemaTicketBookingApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CinemaTicketBookingApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ITheaterService, TheaterService>();
            services.AddScoped<IShowtimeService, ShowtimeService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ISeatReservationService, SeatReservationService>();

            return services;
        }       
    }    
}
