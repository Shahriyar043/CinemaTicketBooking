using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Enums;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using CinemaTicketBookingApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingApi.Implementations
{
    public class SeatReservationService : ISeatReservationService
    {
        private readonly IRepository<Seat> seatRepository;
        private readonly IRepository<Showtime> showtimeRepository;
        private readonly IRepository<ReservedSeat> reservedSeatRepository;
        private readonly IMapper mapper;

        public SeatReservationService(
            IRepository<Seat> seatRepository,
            IRepository<Showtime> showtimeRepository,
            IRepository<ReservedSeat> reservedSeatRepository,
            IMapper mapper)
        {
            this.seatRepository = seatRepository;
            this.showtimeRepository = showtimeRepository;
            this.reservedSeatRepository = reservedSeatRepository;
            this.mapper = mapper;
        }

        public async Task<Seat> GetSeatByIdAsync(int id)
        {
            return await seatRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateSeatAsync(Seat seat)
        {
            await seatRepository.CreateAsync(seat);
            return seat.Id;
        }        

        public async Task UpdateSeatAsync(int id, Seat updatedSeat)
        {
            var existingSeat = await seatRepository.GetByIdAsync(id);
            if (existingSeat == null)
            {
                throw new NotFoundException("Seat not found");
            }

            existingSeat.SeatNumber = updatedSeat.SeatNumber;
            existingSeat.Status = updatedSeat.Status;
            existingSeat.TheaterId = updatedSeat.TheaterId;

            await seatRepository.SaveChangesAsync();
        }

        public async Task DeleteSeatAsync(int id)
        {
            var seat = await seatRepository.GetByIdAsync(id);
            if (seat == null)
            {
                throw new NotFoundException("Seat not found");
            }

            await seatRepository.DeleteAsync(seat);
            await seatRepository.SaveChangesAsync();
        }

        public async Task<ReservedSeat> GetReservedSeatByIdAsync(int id)
        {
            return await reservedSeatRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateReservedSeatAsync(ReservedSeat reservedSeat)
        {
            await reservedSeatRepository.CreateAsync(reservedSeat);
            await reservedSeatRepository.SaveChangesAsync();
            return reservedSeat.Id;
        }
        
        public async Task UpdateReservedSeatAsync(int id, ReservedSeat updatedReservedSeat)
        {
            var existingReservedSeat = await reservedSeatRepository.GetByIdAsync(id);
            if (existingReservedSeat == null)
            {
                throw new NotFoundException("ReservedSeat not found");
            }

            existingReservedSeat.SeatId = updatedReservedSeat.SeatId;
            existingReservedSeat.ReservationId = updatedReservedSeat.ReservationId;

            await reservedSeatRepository.SaveChangesAsync();
        }

        public async Task DeleteReservedSeatAsync(int id)
        {
            var reservedSeat = await reservedSeatRepository.GetByIdAsync(id);
            if (reservedSeat == null)
            {
                throw new NotFoundException("ReservedSeat not found");
            }

            await reservedSeatRepository.DeleteAsync(reservedSeat);
            await reservedSeatRepository.SaveChangesAsync();
        }

        public async Task<SeatReservationResponseDTO> ReserveSeatsAsync(SeatReservationRequestDTO request)
        {
            var showtime = await showtimeRepository.GetAll()
                .Where(s => s.Id == request.ShowtimeId)
                .SingleOrDefaultAsync();

            if (showtime == null)
            {
                throw new NotFoundException("Showtime not found.");
            }

            var availableSeats = seatRepository.GetAll()
                .Where(s => s.Status == SeatStatus.Available && s.TheaterId == showtime.TheaterId)
                .ToList();

            if (availableSeats.Count < request.SeatNumbers.Count)
            {
                return new SeatReservationResponseDTO(false, "Not enough available seats.", null);
            }

            var reservation = new Reservation
            {
                ShowtimeId = request.ShowtimeId,
                UserId = request.UserId,
                ReservationTime = DateTime.Now,
                ExpiryTime = DateTime.Now.AddMinutes(15)
            };

            foreach (var seatNumber in request.SeatNumbers)
            {
                var seat = availableSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
                if (seat != null)
                {
                    seat.Status = SeatStatus.Reserved;
                    var reservedSeat = new ReservedSeat
                    {
                        Reservation = reservation,
                        Seat = seat
                    };
                    await reservedSeatRepository.CreateAsync(reservedSeat);
                }
            }

            return new SeatReservationResponseDTO(true, null, mapper.Map<ReservationDTO>(reservation));
        }
    }
}
