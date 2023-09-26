namespace CinemaTicketBookingApi.Models
{
    public class Theater : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Showtime> Showtimes { get; set; }
    }
}
