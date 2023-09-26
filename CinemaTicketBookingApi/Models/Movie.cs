namespace CinemaTicketBookingApi.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public string Genre { get; set; }
        public List<Showtime> Showtimes { get; set; }
    }
}
