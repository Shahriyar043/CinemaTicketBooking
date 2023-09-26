using AutoMapper;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingApi.Controllers
{
    [ApiController]
    [Route("api/theaters")]
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterService theaterService;
        private readonly IMapper mapper;

        public TheaterController(ITheaterService theaterService, IMapper mapper)
        {
            this.theaterService = theaterService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TheaterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TheaterDTO>>> GetTheaters()
        {
            var theaters = await theaterService.GetAllTheatersAsync();
            return Ok(mapper.Map<IEnumerable<TheaterDTO>>(theaters));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TheaterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TheaterDTO>> GetTheaterById(int id)
        {
            var theater = await theaterService.GetTheaterByIdAsync(id);
            if (theater == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TheaterDTO>(theater));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TheaterDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TheaterDTO>> CreateTheater(TheaterCreateDTO theaterCreateDTO)
        {
            var theaterId = await theaterService.CreateTheaterAsync(theaterCreateDTO);
            var theater = await theaterService.GetTheaterByIdAsync(theaterId);
            return CreatedAtAction(nameof(GetTheaterById), new { id = theaterId }, mapper.Map<TheaterDTO>(theater));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTheater(int id, TheaterUpdateDTO theaterUpdateDTO)
        {
            try
            {
                await theaterService.UpdateTheaterAsync(id, theaterUpdateDTO);
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
        public async Task<IActionResult> DeleteTheater(int id)
        {
            try
            {
                await theaterService.DeleteTheaterAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
