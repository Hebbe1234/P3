﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NorthernWinterBeat.Models;

namespace NorthernWinterBeat.Data
{
    public class NorthernWinterBeatConcertContext : DbContext
    {
        public NorthernWinterBeatConcertContext (DbContextOptions<NorthernWinterBeatConcertContext> options)
            : base(options)
        {
            try
            {
                var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                databaseCreator.CreateTables();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DbSet<Concert> Concert { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Concert>().ToTable("Concert");
       
        }

        public DbSet<NorthernWinterBeat.Models.Venue> Venue { get; set; }

    }
}
