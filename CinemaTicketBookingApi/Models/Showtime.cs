namespace CinemaTicketBookingApi.Models
{
    public class Showtime: BaseEntity
    {
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public DateTime StartTime { get; set; }
        public int AvailableSeats { get; set; }
        public Movie Movie { get; set; }
        public Theater Theater { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
