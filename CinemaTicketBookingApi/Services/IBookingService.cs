using CinemaTicketBookingApi.DTOs;

namespace CinemaTicketBookingApi.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();
        Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(string userId);
        Task<BookingDTO> GetBookingByIdAsync(int id);
        Task<int> CreateBookingAsync(BookingCreateDTO bookingCreateDTO);
        Task UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateDTO);
        Task DeleteBookingAsync(int id);
    }
}
