using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Implementations;
using CinemaTicketBookingApi.Models;
using Moq;

namespace CinemaTicketBookingTests
{
    public class MovieServiceTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetMovieByIdAsync_ReturnsMovieDTO_WhenMovieExists(int movieId)
        {
            // Arrange
            var mockMovieRepository = new Mock<IRepository<Movie>>();
            var mockMapper = new Mock<IMapper>();

            var movie = new Movie { Id = movieId, Title = $"Movie {movieId}" , Description = "Test" , DurationMinutes = 20, Genre = "Test"};

            var movieDTO = new MovieDTO
            (
                 movie.Id,
                 movie.Title,
                 movie.Description,
                 movie.DurationMinutes,
                 movie.Genre
            );

            mockMovieRepository.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(movie);

            mockMapper.Setup(mapper => mapper.Map<MovieDTO>(movie))
                .Returns(movieDTO);

            var movieService = new MovieService(mockMovieRepository.Object, mockMapper.Object);

            // Act
            var result = await movieService.GetMovieByIdAsync(movieId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MovieDTO>(result);
            Assert.Equal(movie.Id, result.Id);
        }

        [Theory]
        [InlineData("New Movie", 1)]
        [InlineData("Another Movie", 2)]
        [InlineData("Test Movie", 3)]
        public async Task CreateMovieAsync_ReturnsCreatedMovieId_WhenMovieIsCreated(string movieTitle, int expectedMovieId)
        {
            // Arrange
            var mockMovieRepository = new Mock<IRepository<Movie>>();
            var mockMapper = new Mock<IMapper>();

            var movieCreateDTO = new MovieCreateDTO(movieTitle, "test", 20, "test");

            mockMapper.Setup(mapper => mapper.Map<Movie>(movieCreateDTO))
                .Returns(new Movie { Id = expectedMovieId, Title = movieCreateDTO.Title });

            mockMovieRepository.Setup(repo => repo.CreateAsync(It.IsAny<Movie>()))
                .Callback<Movie>(movie =>
                {
                    movie.Id = expectedMovieId;
                });

            var movieService = new MovieService(mockMovieRepository.Object, mockMapper.Object);

            // Act
            var result = await movieService.CreateMovieAsync(movieCreateDTO);

            // Assert
            Assert.Equal(expectedMovieId, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task UpdateMovieAsync_UpdatesMovie_WhenMovieExists(int movieId)
        {
            // Arrange
            var mockMovieRepository = new Mock<IRepository<Movie>>();
            var mockMapper = new Mock<IMapper>();

            var existingMovie = new Movie { Id = movieId, Title = "test" };
            var updatedMovie = new MovieUpdateDTO ("test", "test", 20, "test" );

            mockMovieRepository.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(existingMovie);

            var movieService = new MovieService(mockMovieRepository.Object, mockMapper.Object);

            // Act
            await movieService.UpdateMovieAsync(movieId, updatedMovie);

            // Assert
            Assert.Equal(updatedMovie.Title, existingMovie.Title);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task UpdateMovieAsync_ThrowsNotFoundException_WhenMovieDoesNotExist(int movieId)
        {
            // Arrange
            var mockMovieRepository = new Mock<IRepository<Movie>>();
            var mockMapper = new Mock<IMapper>();

            mockMovieRepository.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(null as Movie);

            var movieService = new MovieService(mockMovieRepository.Object, mockMapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => movieService.UpdateMovieAsync(movieId, It.IsAny<MovieUpdateDTO>()));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task DeleteMovieAsync_DeletesMovie_WhenMovieExists(int movieId)
        {
            // Arrange
            var mockMovieRepository = new Mock<IRepository<Movie>>();
            var mockMapper = new Mock<IMapper>();

            var existingMovie = new Movie { Id = movieId, Title = "Existing Movie" };

            mockMovieRepository.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(existingMovie);

            var movieService = new MovieService(mockMovieRepository.Object, mockMapper.Object);

            // Act
            await movieService.DeleteMovieAsync(movieId);

            // Assert
            mockMovieRepository.Verify(repo => repo.DeleteAsync(existingMovie), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task DeleteMovieAsync_ThrowsNotFoundException_WhenMovieDoesNotExist(int movieId)
        {
            // Arrange
            var mockMovieRepository = new Mock<IRepository<Movie>>();
            var mockMapper = new Mock<IMapper>();

            mockMovieRepository.Setup(repo => repo.GetByIdAsync(movieId))
                .ReturnsAsync(null as Movie);

            var movieService = new MovieService(mockMovieRepository.Object, mockMapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => movieService.DeleteMovieAsync(movieId));
        }
    }
}
