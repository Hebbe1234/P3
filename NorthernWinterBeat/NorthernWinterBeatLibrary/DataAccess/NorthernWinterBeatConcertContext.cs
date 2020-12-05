using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeatLibrary.DataAccess
{
    public class NorthernWinterBeatConcertContext : DbContext
    {
        public NorthernWinterBeatConcertContext()
        {

        }
        public NorthernWinterBeatConcertContext (DbContextOptions<NorthernWinterBeatConcertContext> options)
            : base(options)
        {
            
        }

        public DbSet<Concert> Concert { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<LegalTicket> LegalTickets { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<PasswordRequest> PasswordRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Concert>().ToTable("Concert");
            modelBuilder.Entity<Venue>().ToTable("Venue");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<Participant>().ToTable("Participant");
            modelBuilder.Entity<LegalTicket>().ToTable("LegalTicket");
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<PasswordRequest>().ToTable("ResetPasswordRequest");
        }
    }
}
