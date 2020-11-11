using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace NorthernWinterBeatLibrary.Managers
{
    public class DatabaseManager : IDatabaseManager
    {
        public DbContextOptions<NorthernWinterBeatConcertContext> options { get; set; }
        public NorthernWinterBeatConcertContext context { get; set;}


        public DatabaseManager(IConfiguration configuration)
        {
            options = new DbContextOptionsBuilder<NorthernWinterBeatConcertContext>().UseSqlServer(configuration.GetConnectionString("NorthernWinterBeatConcertContext")).Options;
            context = new NorthernWinterBeatConcertContext(options);

        }

        public ApplicationUser GetUser(string username)
        {
            var t1 = context.ApplicationUser;
            var t2 = t1.Where(u => u.Username == username).ToList();

            if(t2.Count() == 0)
            {
                return null; 
            }

            return t2.First();
            //return context.ApplicationUser.Where(u => u.Username == username)?.First();
        }
    }
}