﻿using System;
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

namespace NorthernWinterBeatLibrary.Managers
{
    public class NorthernWinterBeatConcertContext : DbContext
    {
        public NorthernWinterBeatConcertContext()
        {

        }
        public NorthernWinterBeatConcertContext (DbContextOptions<NorthernWinterBeatConcertContext> options)
            : base(options)
        {
           //Please do not bully me for changing my stance on the DB initialization
        }

        public virtual DbSet<Concert> Concert { get; set; }
        public virtual DbSet<Venue> Venue { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<LegalTicket> LegalTickets { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }

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
