using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using CinemaTicketBookingApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingApi.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> reservationRepository;
        private readonly IMapper mapper;

        public ReservationService(IRepository<Reservation> reservationRepository, IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync()
        {
            var reservations = await reservationRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsByUserIdAsync(string userId)
        {
            var reservations = await reservationRepository.GetAll()
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        }

        public async Task<ReservationDTO> GetReservationByIdAsync(int id)
        {
            var reservation = await reservationRepository.GetByIdAsync(id);
            return mapper.Map<ReservationDTO>(reservation);
        }

        public async Task<int> CreateReservationAsync(ReservationCreateDTO reservationCreateDTO)
        {
            var reservation = mapper.Map<Reservation>(reservationCreateDTO);
            await reservationRepository.CreateAsync(reservation);
            return reservation.Id;
        }

        public async Task UpdateReservationAsync(int id, ReservationUpdateDTO reservationUpdateDTO)
        {
            var reservation = await reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new NotFoundException("Reservation not found");
            }

            mapper.Map(reservationUpdateDTO, reservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new NotFoundException("Reservation not found");
            }

            await reservationRepository.DeleteAsync(reservation);
        }
    }
}
