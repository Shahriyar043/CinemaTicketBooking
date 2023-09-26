namespace CinemaTicketBookingApi.DTOs
{
    public record MovieCreateDTO(string Title,
                                 string Description,
                                 int DurationMinutes,
                                 string Genre);
}
