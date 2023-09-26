using AutoMapper;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingApi.Controllers
{
    [Route("api/show-time")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimeService showtimeService;
        private readonly IMapper mapper;

        public ShowtimeController(IShowtimeService showtimeService, IMapper mapper)
        {
            this.showtimeService = showtimeService ?? throw new ArgumentNullException(nameof(showtimeService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShowtimeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowtimeDTO>> GetShowtimeById(int id)
        {
            var showtime = await showtimeService.GetShowtimeByIdAsync(id);
            if (showtime == null)
            {
                return NotFound();
            }

            return Ok(showtime);
        }

        [HttpGet("movie/{movieId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShowtimeDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ShowtimeDTO>>> GetShowtimesByMovie(int movieId)
        {
            var showtimes = await showtimeService.GetShowtimesByMovieAsync(movieId);
            if (showtimes == null)
            {
                return NotFound();
            }

            return Ok(showtimes);
        }

        [HttpGet("theater/{theaterId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShowtimeDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ShowtimeDTO>>> GetShowtimesByTheater(int theaterId)
        {
            var showtimes = await showtimeService.GetShowtimesByTheaterAsync(theaterId);
            if (showtimes == null)
            {
                return NotFound();
            }

            return Ok(showtimes);
        }

        [HttpGet("movie/{movieId}/theater/{theaterId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShowtimeDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ShowtimeDTO>>> GetShowtimesByMovieAndTheater(int movieId, int theaterId)
        {
            var showtimes = await showtimeService.GetShowtimesByMovieAndTheaterAsync(movieId, theaterId);
            if (showtimes == null)
            {
                return NotFound();
            }

            return Ok(showtimes);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateShowtime(ShowtimeCreateDTO showtimeCreateDTO)
        {
            if (showtimeCreateDTO == null)
            {
                return BadRequest();
            }

            var showtimeId = await showtimeService.CreateShowtimeAsync(showtimeCreateDTO);
            return CreatedAtAction(nameof(GetShowtimeById), new { id = showtimeId }, showtimeId);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateShowtime(int id, ShowtimeUpdateDTO showtimeUpdateDTO)
        {
            try
            {
                await showtimeService.UpdateShowtimeAsync(id, showtimeUpdateDTO);
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
        public async Task<IActionResult> DeleteShowtime(int id)
        {
            try
            {
                await showtimeService.DeleteShowtimeAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
