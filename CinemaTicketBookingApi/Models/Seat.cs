using CinemaTicketBookingApi.Enums;

namespace CinemaTicketBookingApi.Models
{
    public class Seat : BaseEntity
    {
        public int TheaterId { get; set; }
        public string SeatNumber { get; set; }
        public SeatStatus Status { get; set; }
        public Theater Theater { get; set; }
    }
}
