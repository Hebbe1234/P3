using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace NorthernWinterBeat.Data
{
    public class NorthernWinterBeatConcertContext : DbContext
    {
        public NorthernWinterBeatConcertContext (DbContextOptions<NorthernWinterBeatConcertContext> options)
            : base(options)
        {
        }

        public DbSet<Concert> Concert { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                databaseCreator.CreateTables();
            }
            catch (Exception)
            {
                modelBuilder.Entity<Concert>().ToTable("ConcertsTest");
            }

       
        }

    }
}
