using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.Enums;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Implementations;
using CinemaTicketBookingApi.Models;
using Moq;

namespace CinemaTicketBookingTests
{
    public class SeatReservationServiceTests
    {
        [Fact]
        public async Task GetSeatByIdAsync_ReturnsSeat_WhenSeatExists()
        {
            // Arrange
            var mockSeatRepository = new Mock<IRepository<Seat>>();
            var seatId = 1;
            var expectedSeat = new Seat { Id = seatId, SeatNumber = "A1", Status = SeatStatus.Available, TheaterId = 1 };

            mockSeatRepository.Setup(repo => repo.GetByIdAsync(seatId))
                .ReturnsAsync(expectedSeat);

            var service = new SeatReservationService(
                mockSeatRepository.Object,
                null,
                null, 
                null
            );

            // Act
            var result = await service.GetSeatByIdAsync(seatId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(seatId, result.Id);
            Assert.Equal("A1", result.SeatNumber);
            Assert.Equal(SeatStatus.Available, result.Status);
            Assert.Equal(1, result.TheaterId);
        }

        [Fact]
        public async Task GetSeatByIdAsync_ReturnsNull_WhenSeatDoesNotExist()
        {
            // Arrange
            var mockSeatRepository = new Mock<IRepository<Seat>>();
            var seatId = 1;

            mockSeatRepository.Setup(repo => repo.GetByIdAsync(seatId))
                .ReturnsAsync(null as Seat);

            var service = new SeatReservationService(
                mockSeatRepository.Object,
                null,
                null, 
                null
            );

            // Act
            var result = await service.GetSeatByIdAsync(seatId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateSeatAsync_ReturnsCreatedSeatId()
        {
            // Arrange
            var mockSeatRepository = new Mock<IRepository<Seat>>();
            var seatToCreate = new Seat { SeatNumber = "B2", Status = SeatStatus.Available, TheaterId = 2 };
            var createdSeatId = 1;

            mockSeatRepository.Setup(repo => repo.CreateAsync(seatToCreate))
                .Callback(() => seatToCreate.Id = createdSeatId);

            var service = new SeatReservationService(
                mockSeatRepository.Object,
                null,
                null, 
                null
            );

            // Act
            var result = await service.CreateSeatAsync(seatToCreate);

            // Assert
            Assert.Equal(createdSeatId, result);
        }

        [Fact]
        public async Task UpdateSeatAsync_UpdatesSeat_WhenSeatExists()
        {
            // Arrange
            var mockSeatRepository = new Mock<IRepository<Seat>>();
            var seatId = 1;
            var existingSeat = new Seat { Id = seatId, SeatNumber = "C3", Status = SeatStatus.Available, TheaterId = 3 };
            var updatedSeat = new Seat { Id = seatId, SeatNumber = "D4", Status = SeatStatus.Reserved, TheaterId = 4 };

            mockSeatRepository.Setup(repo => repo.GetByIdAsync(seatId))
                .ReturnsAsync(existingSeat);

            var service = new SeatReservationService(
                mockSeatRepository.Object,
                null,
                null, 
                null
            );

            // Act
            await service.UpdateSeatAsync(seatId, updatedSeat);

            // Assert
            Assert.Equal("D4", existingSeat.SeatNumber);
            Assert.Equal(SeatStatus.Reserved, existingSeat.Status);
            Assert.Equal(4, existingSeat.TheaterId);
        }

        [Fact]
        public async Task UpdateSeatAsync_ThrowsNotFoundException_WhenSeatDoesNotExist()
        {
            // Arrange
            var mockSeatRepository = new Mock<IRepository<Seat>>();
            var seatId = 1;
            var updatedSeat = new Seat { Id = seatId, SeatNumber = "D4", Status = SeatStatus.Reserved, TheaterId = 4 };

            mockSeatRepository.Setup(repo => repo.GetByIdAsync(seatId))
                .ReturnsAsync(null as Seat);

            var service = new SeatReservationService(
                mockSeatRepository.Object,
                null,
                null, 
                null
            );

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.UpdateSeatAsync(seatId, updatedSeat));
        }

        [Fact]
        public async Task DeleteSeatAsync_DeletesSeat_WhenSeatExists()
        {
            // Arrange
            var mockSeatRepository = new Mock<IRepository<Seat>>();
            var seatId = 1;
            var existingSeat = new Seat { Id = seatId, SeatNumber = "C3", Status = SeatStatus.Available, TheaterId = 3 };

            mockSeatRepository.Setup(repo => repo.GetByIdAsync(seatId))
                .ReturnsAsync(existingSeat);

            var service = new SeatReservationService(
                mockSeatRepository.Object,
                null,
                null, 
                null
            );

            // Act
            await service.DeleteSeatAsync(seatId);

            // Assert
            mockSeatRepository.Verify(repo => repo.DeleteAsync(existingSeat), Times.Once);
        }

        [Fact]
        public async Task DeleteSeatAsync_ThrowsNotFoundException_WhenSeatDoesNotExist()
        {
            // Arrange
            var mockSeatRepository = new Mock<IRepository<Seat>>();
            var seatId = 1;

            mockSeatRepository.Setup(repo => repo.GetByIdAsync(seatId))
                .ReturnsAsync(null as Seat);

            var service = new SeatReservationService(
                mockSeatRepository.Object,
                null,
                null,
                null
            );

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.DeleteSeatAsync(seatId));
        }

        [Fact]
        public async Task GetReservedSeatByIdAsync_ReturnsReservedSeat_WhenReservedSeatExists()
        {
            // Arrange
            var mockReservedSeatRepository = new Mock<IRepository<ReservedSeat>>();
            var reservedSeatId = 1;
            var expectedReservedSeat = new ReservedSeat { Id = reservedSeatId, SeatId = 1, ReservationId = 1 };

            mockReservedSeatRepository.Setup(repo => repo.GetByIdAsync(reservedSeatId))
                .ReturnsAsync(expectedReservedSeat);

            var service = new SeatReservationService(
                null,
                null,
                mockReservedSeatRepository.Object,
                null
            );

            // Act
            var result = await service.GetReservedSeatByIdAsync(reservedSeatId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reservedSeatId, result.Id);
            Assert.Equal(1, result.SeatId);
            Assert.Equal(1, result.ReservationId);
        }

        [Fact]
        public async Task GetReservedSeatByIdAsync_ReturnsNull_WhenReservedSeatDoesNotExist()
        {
            // Arrange
            var mockReservedSeatRepository = new Mock<IRepository<ReservedSeat>>();
            var reservedSeatId = 1;

            mockReservedSeatRepository.Setup(repo => repo.GetByIdAsync(reservedSeatId))
                .ReturnsAsync(null as ReservedSeat);

            var service = new SeatReservationService(
                null,
                null,
                mockReservedSeatRepository.Object,
                null 
            );

            // Act
            var result = await service.GetReservedSeatByIdAsync(reservedSeatId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateReservedSeatAsync_ReturnsCreatedReservedSeatId()
        {
            // Arrange
            var mockReservedSeatRepository = new Mock<IRepository<ReservedSeat>>();
            var reservedSeatToCreate = new ReservedSeat { SeatId = 1, ReservationId = 1 };
            var createdReservedSeatId = 1;

            mockReservedSeatRepository.Setup(repo => repo.CreateAsync(reservedSeatToCreate))
                .Callback(() => reservedSeatToCreate.Id = createdReservedSeatId);

            var service = new SeatReservationService(
                null,  
                null, 
                mockReservedSeatRepository.Object,
                null
            );

            // Act
            var result = await service.CreateReservedSeatAsync(reservedSeatToCreate);

            // Assert
            Assert.Equal(createdReservedSeatId, result);
        }

        [Fact]
        public async Task UpdateReservedSeatAsync_UpdatesReservedSeat_WhenReservedSeatExists()
        {
            // Arrange
            var mockReservedSeatRepository = new Mock<IRepository<ReservedSeat>>();
            var reservedSeatId = 1;
            var existingReservedSeat = new ReservedSeat { Id = reservedSeatId, SeatId = 1, ReservationId = 1 };
            var updatedReservedSeat = new ReservedSeat { Id = reservedSeatId, SeatId = 2, ReservationId = 2 };

            mockReservedSeatRepository.Setup(repo => repo.GetByIdAsync(reservedSeatId))
                .ReturnsAsync(existingReservedSeat);

            var service = new SeatReservationService(
                null,  
                null, 
                mockReservedSeatRepository.Object,
                null 
            );

            // Act
            await service.UpdateReservedSeatAsync(reservedSeatId, updatedReservedSeat);

            // Assert
            Assert.Equal(2, existingReservedSeat.SeatId);
            Assert.Equal(2, existingReservedSeat.ReservationId);
        }

        [Fact]
        public async Task UpdateReservedSeatAsync_ThrowsNotFoundException_WhenReservedSeatDoesNotExist()
        {
            // Arrange
            var mockReservedSeatRepository = new Mock<IRepository<ReservedSeat>>();
            var reservedSeatId = 1;
            var updatedReservedSeat = new ReservedSeat { Id = reservedSeatId, SeatId = 2, ReservationId = 2 };

            mockReservedSeatRepository.Setup(repo => repo.GetByIdAsync(reservedSeatId))
                .ReturnsAsync(null as ReservedSeat);

            var service = new SeatReservationService(
                null,  
                null, 
                mockReservedSeatRepository.Object,
                null 
            );

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.UpdateReservedSeatAsync(reservedSeatId, updatedReservedSeat));
        }

        [Fact]
        public async Task DeleteReservedSeatAsync_DeletesReservedSeat_WhenReservedSeatExists()
        {
            // Arrange
            var mockReservedSeatRepository = new Mock<IRepository<ReservedSeat>>();
            var reservedSeatId = 1;
            var existingReservedSeat = new ReservedSeat { Id = reservedSeatId, SeatId = 1, ReservationId = 1 };

            mockReservedSeatRepository.Setup(repo => repo.GetByIdAsync(reservedSeatId))
                .ReturnsAsync(existingReservedSeat);

            var service = new SeatReservationService(
                null,  
                null, 
                mockReservedSeatRepository.Object,
                null 
            );

            // Act
            await service.DeleteReservedSeatAsync(reservedSeatId);

            // Assert
            mockReservedSeatRepository.Verify(repo => repo.DeleteAsync(existingReservedSeat), Times.Once);
        }

        [Fact]
        public async Task DeleteReservedSeatAsync_ThrowsNotFoundException_WhenReservedSeatDoesNotExist()
        {
            // Arrange
            var mockReservedSeatRepository = new Mock<IRepository<ReservedSeat>>();
            var reservedSeatId = 1;

            mockReservedSeatRepository.Setup(repo => repo.GetByIdAsync(reservedSeatId))
                .ReturnsAsync(null as ReservedSeat);

            var service = new SeatReservationService(
                null,  
                null, 
                mockReservedSeatRepository.Object,
                null 
            );

            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.DeleteReservedSeatAsync(reservedSeatId));
        }
    }
}
