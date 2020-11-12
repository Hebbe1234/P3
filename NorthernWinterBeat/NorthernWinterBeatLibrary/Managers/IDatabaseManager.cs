using Microsoft.EntityFrameworkCore;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public interface IDatabaseManager
    {
        public DbContextOptions<NorthernWinterBeatConcertContext> options { get; set; }
        public NorthernWinterBeatConcertContext context { get; set; }

    }
}
