namespace CinemaTicketBookingApi.DTOs
{
    public record ShowtimeCreateDTO(int MovieId,
                                    int TheaterId,
                                    DateTime StartTime,
                                    int AvailableSeats);
}
