namespace CinemaTicketBookingApi.DTOs
{
    public record MovieUpdateDTO(string Title,
                                 string Description,
                                 int DurationMinutes,
                                 string Genre);
}
