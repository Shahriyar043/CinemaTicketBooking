using CinemaTicketBookingApi.Enums;

namespace CinemaTicketBookingApi.DTOs
{
    public record SeatUpdateDTO(string SeatNumber,
                                SeatStatus Status);
}
