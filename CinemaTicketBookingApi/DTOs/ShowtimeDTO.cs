namespace CinemaTicketBookingApi.DTOs
{
    public record ShowtimeDTO(int Id,
                              int MovieId,
                              int TheaterId,
                              DateTime StartTime,
                              int AvailableSeats);
}
