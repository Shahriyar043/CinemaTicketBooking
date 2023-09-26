namespace CinemaTicketBookingApi.DTOs
{
    public record BookingDTO(int Id,
                             int ReservationId,
                             decimal TotalPrice);
}
