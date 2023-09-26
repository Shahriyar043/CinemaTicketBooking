using CinemaTicketBookingApi.DTOs;

namespace CinemaTicketBookingApi.Services
{
    public interface IShowtimeService
    {
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByMovieAsync(int movieId);
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByTheaterAsync(int theaterId);
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByMovieAndTheaterAsync(int movieId, int theaterId);
        Task<ShowtimeDTO> GetShowtimeByIdAsync(int id);
        Task<int> CreateShowtimeAsync(ShowtimeCreateDTO showtimeCreateDTO);
        Task UpdateShowtimeAsync(int id, ShowtimeUpdateDTO showtimeUpdateDTO);
        Task DeleteShowtimeAsync(int id);
    }
}
