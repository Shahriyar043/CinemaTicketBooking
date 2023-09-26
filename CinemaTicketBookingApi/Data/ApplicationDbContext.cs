﻿using CinemaTicketBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //private readonly string _connectionString; // Private field for the connection string

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        //    : base(options)
        //{
        //    // Retrieve the connection string from the configuration
        //    _connectionString = configuration.GetConnectionString("DefaultConnection");
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // Use the connection string retrieved from the configuration
        //        optionsBuilder.UseSqlServer(_connectionString);
        //    }
        //}

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservedSeat> ReservedSeats { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Theater> Theaters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Movie>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<ReservedSeat>()
                .HasKey(rs => rs.Id);

            modelBuilder.Entity<Seat>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Showtime>()
                .HasKey(st => st.Id);

            modelBuilder.Entity<Theater>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<ReservedSeat>()
               .HasOne(rs => rs.Seat)
               .WithMany()
               .HasForeignKey(rs => rs.SeatId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
