using CinemaTicketBookingApi.Enums;

namespace CinemaTicketBookingApi.DTOs
{
    public record SeatCreateDTO(int TheaterId,
                                string SeatNumber,
                                SeatStatus Status);
}
