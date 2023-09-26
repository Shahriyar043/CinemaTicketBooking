using AutoMapper;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using CinemaTicketBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingApi.Controllers
{
    [ApiController]
    [Route("api/seat-reservation")]
    public class SeatReservationController : ControllerBase
    {
        private readonly ISeatReservationService seatReservationService;
        private readonly IMapper mapper;

        public SeatReservationController(ISeatReservationService seatReservationService, IMapper mapper)
        {
            this.seatReservationService = seatReservationService;
            this.mapper = mapper;
        }

        [HttpGet("seat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeatDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeatDTO>> GetSeatById(int id)
        {
            var seat = await seatReservationService.GetSeatByIdAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SeatDTO>(seat));
        }

        [HttpPost("seat")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateSeat(SeatCreateDTO seatCreateDTO)
        {
            var seat = mapper.Map<Seat>(seatCreateDTO);
            var seatId = await seatReservationService.CreateSeatAsync(seat);
            return CreatedAtAction(nameof(GetSeatById), new { id = seatId }, seatId);
        }

        [HttpPut("seat/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSeat(int id, SeatUpdateDTO seatUpdateDTO)
        {
            try
            {
                await seatReservationService.UpdateSeatAsync(id, mapper.Map<Seat>(seatUpdateDTO));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("seat/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            try
            {
                await seatReservationService.DeleteSeatAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("reserved-seat/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservedSeatDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservedSeatDTO>> GetReservedSeatById(int id)
        {
            var reservedSeat = await seatReservationService.GetReservedSeatByIdAsync(id);
            if (reservedSeat == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ReservedSeatDTO>(reservedSeat));
        }

        [HttpPost("reserved-seat")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateReservedSeat(ReservedSeatCreateDTO reservedSeatCreateDTO)
        {
            var reservedSeat = mapper.Map<ReservedSeat>(reservedSeatCreateDTO);
            var reservedSeatId = await seatReservationService.CreateReservedSeatAsync(reservedSeat);
            return CreatedAtAction(nameof(GetReservedSeatById), new { id = reservedSeatId }, reservedSeatId);
        }

        [HttpPut("reserved-seat/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReservedSeat(int id, ReservedSeatUpdateDTO reservedSeatUpdateDTO)
        {
            try
            {
                await seatReservationService.UpdateReservedSeatAsync(id, mapper.Map<ReservedSeat>(reservedSeatUpdateDTO));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("reserved-seat/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReservedSeat(int id)
        {
            try
            {
                await seatReservationService.DeleteReservedSeatAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("reserved")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReserveSeats([FromBody] SeatReservationRequestDTO request)
        {
            var response = await seatReservationService.ReserveSeatsAsync(request);

            if (response.IsReservationSuccessful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.ErrorMessage });
            }
        }
    }
}
