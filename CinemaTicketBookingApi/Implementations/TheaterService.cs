using AutoMapper;
using CinemaTicketBookingApi.Data.Repositories;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Exceptions;
using CinemaTicketBookingApi.Models;
using CinemaTicketBookingApi.Services;

namespace CinemaTicketBookingApi.Implementations
{
    public class TheaterService : ITheaterService
    {
        private readonly IRepository<Theater> theaterRepository;
        private readonly IMapper mapper;

        public TheaterService(IRepository<Theater> theaterRepository, IMapper mapper)
        {
            this.theaterRepository = theaterRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TheaterDTO>> GetAllTheatersAsync()
        {
            var theaters = await theaterRepository.GetAllAsync();
            return mapper.Map<IEnumerable<TheaterDTO>>(theaters);
        }

        public async Task<TheaterDTO> GetTheaterByIdAsync(int id)
        {
            var theater = await theaterRepository.GetByIdAsync(id);
            if (theater == null)
            {
                throw new NotFoundException("Theater not found");
            }

            return mapper.Map<TheaterDTO>(theater);
        }

        public async Task<int> CreateTheaterAsync(TheaterCreateDTO theaterCreateDTO)
        {
            var theater = mapper.Map<Theater>(theaterCreateDTO);
            await theaterRepository.CreateAsync(theater);
            return theater.Id;
        }

        public async Task UpdateTheaterAsync(int id, TheaterUpdateDTO theaterUpdateDTO)
        {
            var theater = await theaterRepository.GetByIdAsync(id);
            if (theater == null)
            {
                throw new NotFoundException("Theater not found");
            }

            mapper.Map(theaterUpdateDTO, theater);
            await theaterRepository.UpdateAsync(theater);
        }

        public async Task DeleteTheaterAsync(int id)
        {
            var theater = await theaterRepository.GetByIdAsync(id);
            if (theater == null)
            {
                throw new NotFoundException("Theater not found");
            }

            await theaterRepository.DeleteAsync(theater);
        }
    }
}
