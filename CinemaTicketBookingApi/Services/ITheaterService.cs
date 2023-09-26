using CinemaTicketBookingApi.DTOs;

namespace CinemaTicketBookingApi.Services
{
    public interface ITheaterService
    {
        Task<IEnumerable<TheaterDTO>> GetAllTheatersAsync();
        Task<TheaterDTO> GetTheaterByIdAsync(int id);
        Task<int> CreateTheaterAsync(TheaterCreateDTO theaterCreateDTO);
        Task UpdateTheaterAsync(int id, TheaterUpdateDTO theaterUpdateDTO);
        Task DeleteTheaterAsync(int id);
    }
}
