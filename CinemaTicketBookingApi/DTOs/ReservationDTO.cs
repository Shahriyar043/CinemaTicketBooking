namespace CinemaTicketBookingApi.DTOs
{
    public record ReservationDTO(
        int Id = 0,
        int ShowtimeId = 0,
        string UserId = "",
        DateTime ReservationTime = default,
        DateTime ExpiryTime = default,
        List<ReservedSeatDTO> ReservedSeats = null);
}
