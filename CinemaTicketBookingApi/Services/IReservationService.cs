using CinemaTicketBookingApi.DTOs;

namespace CinemaTicketBookingApi.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationDTO>> GetReservationsByUserIdAsync(string userId);
        Task<ReservationDTO> GetReservationByIdAsync(int id);
        Task<int> CreateReservationAsync(ReservationCreateDTO reservationCreateDTO);
        Task UpdateReservationAsync(int id, ReservationUpdateDTO reservationUpdateDTO);
        Task DeleteReservationAsync(int id);
    }
}
