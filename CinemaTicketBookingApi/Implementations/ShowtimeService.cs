using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using CinemaTicketBookingApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingApi.Implementations
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IRepository<Showtime> showtimeRepository;
        private readonly IMapper mapper;

        public ShowtimeService(IRepository<Showtime> showtimeRepository, IMapper mapper)
        {
            this.showtimeRepository = showtimeRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByMovieAsync(int movieId)
        {
            var showtimes = await showtimeRepository.GetAll()
                .Where(s => s.MovieId == movieId)
                .ToListAsync();

            return mapper.Map<IEnumerable<ShowtimeDTO>>(showtimes);
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByTheaterAsync(int theaterId)
        {
            var showtimes = await showtimeRepository.GetAll()
                .Where(s => s.TheaterId == theaterId)
                .ToListAsync();

            return mapper.Map<IEnumerable<ShowtimeDTO>>(showtimes);
        }

        public async Task<IEnumerable<ShowtimeDTO>> GetShowtimesByMovieAndTheaterAsync(int movieId, int theaterId)
        {
            var showtimes = await showtimeRepository.GetAll()
                .Where(s => s.MovieId == movieId && s.TheaterId == theaterId)
                .ToListAsync();

            return mapper.Map<IEnumerable<ShowtimeDTO>>(showtimes);
        }

        public async Task<ShowtimeDTO> GetShowtimeByIdAsync(int id)
        {
            var showtime = await showtimeRepository.GetByIdAsync(id);
            if (showtime == null)
            {
                throw new NotFoundException("Showtime not found");
            }

            return mapper.Map<ShowtimeDTO>(showtime);
        }

        public async Task<int> CreateShowtimeAsync(ShowtimeCreateDTO showtimeCreateDTO)
        {
            var showtime = mapper.Map<Showtime>(showtimeCreateDTO);
            await showtimeRepository.CreateAsync(showtime);
            return showtime.Id;
        }

        public async Task UpdateShowtimeAsync(int id, ShowtimeUpdateDTO showtimeUpdateDTO)
        {
            var showtime = await showtimeRepository.GetByIdAsync(id);
            if (showtime == null)
            {
                throw new NotFoundException("Showtime not found");
            }

            mapper.Map(showtimeUpdateDTO, showtime);
        }

        public async Task DeleteShowtimeAsync(int id)
        {
            var showtime = await showtimeRepository.GetByIdAsync(id);
            if (showtime == null)
            {
                throw new NotFoundException("Showtime not found");
            }

            await showtimeRepository.DeleteAsync(showtime);
        }
    }
}
