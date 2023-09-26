namespace CinemaTicketBookingApi.Models
{
    public class Reservation : BaseEntity
    {
        public int ShowtimeId { get; set; }
        public string UserId { get; set; }
        public List<ReservedSeat> ReservedSeats { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime ExpiryTime { get; set; }
        public Showtime Showtime { get; set; }
        public Booking Booking { get; set; }
    }
}
