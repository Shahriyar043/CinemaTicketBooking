using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using CinemaTicketBookingApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingApi.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> bookingRepository;
        private readonly IMapper mapper;

        public BookingService(IRepository<Booking> bookingRepository, IMapper mapper)
        {
            this.bookingRepository = bookingRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await bookingRepository.GetAllAsync();
            return mapper.Map<IEnumerable<BookingDTO>>(bookings);
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(string userId)
        {
            var bookings = await bookingRepository.GetAll()
                .Where(b => b.Reservation.UserId == userId)
                .ToListAsync();

            return mapper.Map<IEnumerable<BookingDTO>>(bookings);
        }

        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await bookingRepository.GetByIdAsync(id);
            return mapper.Map<BookingDTO>(booking);
        }

        public async Task<int> CreateBookingAsync(BookingCreateDTO bookingCreateDTO)
        {
            var booking = mapper.Map<Booking>(bookingCreateDTO);
            await bookingRepository.CreateAsync(booking);
            return booking.Id;
        }

        public async Task UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateDTO)
        {
            var booking = await bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new NotFoundException("Booking not found");
            }

            mapper.Map(bookingUpdateDTO, booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new NotFoundException("Booking not found");
            }

            await bookingRepository.DeleteAsync(booking);
        }
    }
}
