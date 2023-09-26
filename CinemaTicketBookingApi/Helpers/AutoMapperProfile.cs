using AutoMapper;
using CinemaTicketBookingApi.DTOs;
using CinemaTicketBookingApi.Models;

namespace CinemaTicketBookingApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieUpdateDTO, Movie>();

            CreateMap<Theater, TheaterDTO>();
            CreateMap<TheaterCreateDTO, Theater>();
            CreateMap<TheaterUpdateDTO, Theater>();

            CreateMap<Showtime, ShowtimeDTO>();
            CreateMap<ShowtimeCreateDTO, Showtime>();
            CreateMap<ShowtimeUpdateDTO, Showtime>();

            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ShowtimeId, opt => opt.MapFrom(src => src.ShowtimeId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ReservationTime, opt => opt.MapFrom(src => src.ReservationTime))
                .ForMember(dest => dest.ExpiryTime, opt => opt.MapFrom(src => src.ExpiryTime))
                .ForMember(dest => dest.ReservedSeats, opt => opt.MapFrom(src => src.ReservedSeats));

            CreateMap<ReservedSeat, ReservedSeatDTO>()
                .ForMember(dest => dest.SeatId, opt => opt.MapFrom(src => src.SeatId))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber));

            CreateMap<ReservationCreateDTO, Reservation>();
            CreateMap<ReservationUpdateDTO, Reservation>();
            CreateMap<SeatReservationRequestDTO, ReservedSeat>();
            CreateMap<SeatReservationResponseDTO, ReservedSeat>();

            CreateMap<Seat, SeatDTO>();
            CreateMap<SeatCreateDTO, Seat>();
            CreateMap<SeatUpdateDTO, Seat>();

            CreateMap<ReservedSeat, ReservedSeatDTO>();
            CreateMap<ReservedSeatCreateDTO, ReservedSeat>();
            CreateMap<ReservedSeatUpdateDTO, ReservedSeat>();

            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingCreateDTO, Booking>();
            CreateMap<BookingUpdateDTO, Booking>();
        }
    }
}
