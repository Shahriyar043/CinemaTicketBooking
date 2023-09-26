namespace CinemaTicketBookingApi.Models
{
    public class Booking : BaseEntity
    {
        public int ReservationId { get; set; }
        public decimal TotalPrice { get; set; }
        public Reservation Reservation { get; set; }
    }
}
