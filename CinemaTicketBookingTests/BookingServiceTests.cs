using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Implementations;
using CinemaTicketBookingApi.Models;
using Moq;

namespace CinemaTicketBookingTests
{
    public class BookingServiceTests
    {
        public static IEnumerable<object[]> BookingTestData()
        {
            yield return new object[] { 1, 1 };
            yield return new object[] { 2, 2 };
            yield return new object[] { 3, 3 };
        }
        
        [Theory]
        [MemberData(nameof(BookingTestData))]
        public async Task GetBookingByIdAsync_ReturnsBookingDTO_WhenBookingExists(int bookingId, int expectedId)
        {
            // Arrange
            var mockBookingRepository = new Mock<IRepository<Booking>>();
            var mockMapper = new Mock<IMapper>();

            var booking = new Booking { Id = expectedId, ReservationId = 1, TotalPrice = 50 };

            var bookingDTO = new BookingDTO ( booking.Id, booking.ReservationId, booking.TotalPrice);

            mockBookingRepository.Setup(repo => repo.GetByIdAsync(bookingId))
                .ReturnsAsync(booking);

            mockMapper.Setup(mapper => mapper.Map<BookingDTO>(booking))
                .Returns(bookingDTO);

            var bookingService = new BookingService(mockBookingRepository.Object, mockMapper.Object);

            // Act
            var result = await bookingService.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookingDTO>(result);
            Assert.Equal(expectedId, result.Id);
        }
    }
}
