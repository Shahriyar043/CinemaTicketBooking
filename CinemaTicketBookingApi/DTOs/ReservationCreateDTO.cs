using CinemaTicketBookingApi.Models;

namespace CinemaTicketBookingApi.DTOs
{
    public record ReservationCreateDTO(int ShowtimeId,
                                       string UserId);
}
