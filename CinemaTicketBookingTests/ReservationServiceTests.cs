using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Implementations;
using CinemaTicketBookingApi.Models;
using Moq;

namespace CinemaTicketBookingTests
{
    public class ReservationServiceTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetReservationByIdAsync_ReturnsReservationDTO_WhenReservationExists(int reservationId)
        {
            // Arrange
            var mockReservationRepository = new Mock<IRepository<Reservation>>();
            var mockMapper = new Mock<IMapper>();

            var userId = "User1";

            var reservation = new Reservation { Id = reservationId, UserId = userId };

            var reservationDTO = new ReservationDTO
            {
                Id = reservation.Id,
                UserId = reservation.UserId
            };

            mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(reservation);

            mockMapper.Setup(mapper => mapper.Map<ReservationDTO>(reservation))
                .Returns(reservationDTO);

            var reservationService = new ReservationService(mockReservationRepository.Object, mockMapper.Object);

            // Act
            var result = await reservationService.GetReservationByIdAsync(reservationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ReservationDTO>(result);
            Assert.Equal(reservationId, result.Id);
            Assert.Equal(userId, result.UserId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetReservationByIdAsync_ReturnsNull_WhenReservationDoesNotExist(int reservationId)
        {
            // Arrange
            var mockReservationRepository = new Mock<IRepository<Reservation>>();
            var mockMapper = new Mock<IMapper>();

            mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(null as Reservation);

            var reservationService = new ReservationService(mockReservationRepository.Object, mockMapper.Object);

            // Act
            var result = await reservationService.GetReservationByIdAsync(reservationId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateReservationAsync_ReturnsCreatedReservationId_WhenReservationIsCreated()
        {
            // Arrange
            var mockReservationRepository = new Mock<IRepository<Reservation>>();
            var mockMapper = new Mock<IMapper>();

            var reservationCreateDTO = new ReservationCreateDTO(1, "User1");

            var createdReservationId = 1;

            mockMapper.Setup(mapper => mapper.Map<Reservation>(reservationCreateDTO))
                .Returns(new Reservation { Id = createdReservationId, UserId = reservationCreateDTO.UserId });

            mockReservationRepository.Setup(repo => repo.CreateAsync(It.IsAny<Reservation>()))
                .Callback<Reservation>(reservation =>
                {
                    reservation.Id = createdReservationId;
                });

            var reservationService = new ReservationService(mockReservationRepository.Object, mockMapper.Object);

            // Act
            var result = await reservationService.CreateReservationAsync(reservationCreateDTO);

            // Assert
            Assert.Equal(createdReservationId, result);
        }

       
        [Fact]
        public async Task DeleteReservationAsync_DeletesReservation_WhenReservationExists()
        {
            // Arrange
            var mockReservationRepository = new Mock<IRepository<Reservation>>();
            var mockMapper = new Mock<IMapper>();

            var reservationId = 1;
            var userId = "User1";

            var existingReservation = new Reservation { Id = reservationId, UserId = userId };

            mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(existingReservation);

            var reservationService = new ReservationService(mockReservationRepository.Object, mockMapper.Object);

            // Act
            await reservationService.DeleteReservationAsync(reservationId);

            // Assert
            mockReservationRepository.Verify(repo => repo.DeleteAsync(existingReservation), Times.Once);
        }

        [Fact]
        public async Task DeleteReservationAsync_ThrowsNotFoundException_WhenReservationDoesNotExist()
        {
            // Arrange
            var mockReservationRepository = new Mock<IRepository<Reservation>>();
            var mockMapper = new Mock<IMapper>();

            var reservationId = 1;

            mockReservationRepository.Setup(repo => repo.GetByIdAsync(reservationId))
                .ReturnsAsync(null as Reservation);

            var reservationService = new ReservationService(mockReservationRepository.Object, mockMapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                reservationService.DeleteReservationAsync(reservationId));
        }
    }
}
