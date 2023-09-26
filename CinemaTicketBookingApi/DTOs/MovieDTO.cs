namespace CinemaTicketBookingApi.DTOs
{
    public record MovieDTO(int Id,
                           string Title,
                           string Description,
                           int DurationMinutes,
                           string Genre);
}
