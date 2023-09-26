using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using CinemaTicketBookingApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingApi.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> movieRepository;
        private readonly IMapper mapper;

        public MovieService(IRepository<Movie> movieRepository, IMapper mapper)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            var movies = await movieRepository.GetAllAsync();
            return mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public async Task<MovieDTO> GetMovieByIdAsync(int id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            return mapper.Map<MovieDTO>(movie);
        }

        public async Task<int> CreateMovieAsync(MovieCreateDTO movieCreateDTO)
        {
            var movie = mapper.Map<Movie>(movieCreateDTO);
            await movieRepository.CreateAsync(movie);
            return movie.Id;
        }

        public async Task UpdateMovieAsync(int id, MovieUpdateDTO movieUpdateDTO)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                throw new NotFoundException("Movie not found");
            }

            mapper.Map(movieUpdateDTO, movie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                throw new NotFoundException("Movie not found");
            }

            await movieRepository.DeleteAsync(movie);
        }
    }
}
