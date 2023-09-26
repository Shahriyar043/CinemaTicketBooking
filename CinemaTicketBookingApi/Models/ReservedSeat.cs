using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketBookingApi.Models
{
    public class ReservedSeat : BaseEntity
    {
        public int ReservationId { get; set; }
        public int SeatId { get; set; }
        public Reservation Reservation { get; set; }
        public Seat Seat { get; set; }
    }
}
