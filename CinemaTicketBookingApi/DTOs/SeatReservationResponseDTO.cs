namespace CinemaTicketBookingApi.DTOs
{
    public record SeatReservationResponseDTO(bool IsReservationSuccessful,
                                             string ErrorMessage,
                                             ReservationDTO Reservation);

}
