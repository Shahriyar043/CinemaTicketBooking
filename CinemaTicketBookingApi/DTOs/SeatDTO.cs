using CinemaTicketBookingApi.Enums;

namespace CinemaTicketBookingApi.DTOs
{
    public record SeatDTO(int Id,
                          int TheaterId,
                          string SeatNumber,
                          SeatStatus Status);
}
