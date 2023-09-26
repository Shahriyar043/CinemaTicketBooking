namespace CinemaTicketBookingApi.DTOs
{
    public record SeatReservationRequestDTO(int ShowtimeId,
                                            List<string> SeatNumbers,
                                            string UserId);

}
