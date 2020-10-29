using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace NorthernWinterBeatLibrary.Managers
{
    public class DatabaseManager
    {
        public static NorthernWinterBeatConcertContext context { get; private set; }
        public DatabaseManager(IConfiguration configuration)
        {
            if (context == null)
            {
                DbContextOptions<NorthernWinterBeatConcertContext> options = new DbContextOptionsBuilder<NorthernWinterBeatConcertContext>().UseSqlServer(configuration.GetConnectionString("NorthernWinterBeatConcertContext")).Options;
                context = new NorthernWinterBeatConcertContext(options);

            }
        }

        public static ApplicationUser GetUser(string username)
        {
            return context.ApplicationUser.Where(u => u.Username == username)?.First();
        }
    }
}