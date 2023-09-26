using AutoMapper;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            this.movieService = movieService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await movieService.GetAllMoviesAsync();
            return Ok(mapper.Map<IEnumerable<MovieDTO>>(movies));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDTO>> GetMovieById(int id)
        {
            var movie = await movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<MovieDTO>(movie));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieDTO>> CreateMovie(MovieCreateDTO movieCreateDTO)
        {
            var movieId = await movieService.CreateMovieAsync(movieCreateDTO);
            var movie = await movieService.GetMovieByIdAsync(movieId);
            return CreatedAtAction(nameof(GetMovieById), new { id = movieId }, mapper.Map<MovieDTO>(movie));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDTO movieUpdateDTO)
        {
            try
            {
                await movieService.UpdateMovieAsync(id, movieUpdateDTO);
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
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await movieService.DeleteMovieAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
