using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Models;

namespace CinemaTicketBookingApi.Services
{
    public interface ISeatReservationService
    {
        Task<int> CreateSeatAsync(Seat seat);
        Task<Seat> GetSeatByIdAsync(int id);
        Task UpdateSeatAsync(int id, Seat updatedSeat);
        Task DeleteSeatAsync(int id);
        Task<int> CreateReservedSeatAsync(ReservedSeat reservedSeat);
        Task<ReservedSeat> GetReservedSeatByIdAsync(int id);
        Task UpdateReservedSeatAsync(int id, ReservedSeat updatedReservedSeat);
        Task DeleteReservedSeatAsync(int id);
        Task<SeatReservationResponseDTO> ReserveSeatsAsync(SeatReservationRequestDTO request);
    }
}
