using AutoMapper;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingApi.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;
        private readonly IMapper mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            this.bookingService = bookingService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
        {
            var bookings = await bookingService.GetAllBookingsAsync();
            return Ok(mapper.Map<IEnumerable<BookingDTO>>(bookings));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDTO>> GetBookingById(int id)
        {
            var booking = await bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookingDTO>(booking));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingDTO>> CreateBooking(BookingCreateDTO bookingCreateDTO)
        {
            var bookingId = await bookingService.CreateBookingAsync(bookingCreateDTO);
            var booking = await bookingService.GetBookingByIdAsync(bookingId);
            return CreatedAtAction(nameof(GetBookingById), new { id = bookingId }, mapper.Map<BookingDTO>(booking));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBooking(int id, BookingUpdateDTO bookingUpdateDTO)
        {
            try
            {
                await bookingService.UpdateBookingAsync(id, bookingUpdateDTO);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                await bookingService.DeleteBookingAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
