namespace CinemaTicketBookingApi.DTOs
{
    public record ShowtimeUpdateDTO(DateTime StartTime,
                                    int AvailableSeats);
}
