using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthernWinterBeatLibrary.DataAccess;
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

        private IDataAccess DataAccess { get; set; }

        public DatabaseManager(IConfiguration configuration, IDataAccess dataAccess)
        {
            options = new DbContextOptionsBuilder<NorthernWinterBeatConcertContext>().UseSqlServer(configuration.GetConnectionString("NorthernWinterBeatConcertContext")).Options;
            context = new NorthernWinterBeatConcertContext(options, dataAccess);
            DataAccess = dataAccess;

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