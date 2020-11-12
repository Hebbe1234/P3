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

        public DatabaseManager(IConfiguration configuration, NorthernWinterBeatConcertContext _context)
        {
            context = _context;

        }

        
    }
}