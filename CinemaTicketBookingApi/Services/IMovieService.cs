using CinemaTicketBookingApi.DTOs;

namespace CinemaTicketBookingApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetAllMoviesAsync();
        Task<MovieDTO> GetMovieByIdAsync(int id);
        Task<int> CreateMovieAsync(MovieCreateDTO movieCreateDTO);
        Task UpdateMovieAsync(int id, MovieUpdateDTO movieUpdateDTO);
        Task DeleteMovieAsync(int id);
    }
}
