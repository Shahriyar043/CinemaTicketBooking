using AutoMapper;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingApi.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IMapper mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            this.reservationService = reservationService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var reservations = await reservationService.GetAllReservationsAsync();
            return Ok(mapper.Map<IEnumerable<ReservationDTO>>(reservations));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDTO>> GetReservationById(int id)
        {
            var reservation = await reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ReservationDTO>(reservation));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReservationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationDTO>> CreateReservation(ReservationCreateDTO reservationCreateDTO)
        {
            var reservationId = await reservationService.CreateReservationAsync(reservationCreateDTO);
            var reservation = await reservationService.GetReservationByIdAsync(reservationId);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservationId }, mapper.Map<ReservationDTO>(reservation));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReservation(int id, ReservationUpdateDTO reservationUpdateDTO)
        {
            try
            {
                await reservationService.UpdateReservationAsync(id, reservationUpdateDTO);
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
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await reservationService.DeleteReservationAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
