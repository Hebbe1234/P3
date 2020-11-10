﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeatLibrary.Managers
{
    public class NorthernWinterBeatConcertContext : DbContext
    {
        public NorthernWinterBeatConcertContext (DbContextOptions<NorthernWinterBeatConcertContext> options)
            : base(options)
        {
            try
            {
                var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                databaseCreator.CreateTables();  //Denne her er træls, og vil fucke op hvis der allerede er en tabel
            }
            catch (SqlException)
            {
                //This is ignored
            }
            catch(Microsoft.Data.SqlClient.SqlException)
            {
                //This is ignored
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NorthernWinterBeatConcertContext()
            :base(DatabaseManager.options)
        {
        }
        public DbSet<Concert> Concert { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<LegalTicket> LegalTickets { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Concert>().ToTable("Concert");
            modelBuilder.Entity<Venue>().ToTable("Venue");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<Participant>().ToTable("Participant");
            modelBuilder.Entity<LegalTicket>().ToTable("LegalTicket");
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
        }
    }
}
