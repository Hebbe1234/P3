using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthernWinterBeatLibrary.DataAccess
{
    public class EFDataAccess : IDataAccess
    {
        public void Add<T>(T input)
        {
            if (typeof(T) == typeof(Concert))
            {
                DatabaseManager.context.Concert.Add(input as Concert);
            }
            else if (typeof(T) == typeof(Booking))
            {
                DatabaseManager.context.Booking.Add(input as Booking);
            }
            else if (typeof(T) == typeof(Ticket))
            {
                DatabaseManager.context.Ticket.Add(input as Ticket);
            }
            else if (typeof(T) == typeof(ApplicationUser))
            {
                DatabaseManager.context.ApplicationUser.Add(input as ApplicationUser);
            }
            else if (typeof(T) == typeof(Participant))
            {
                DatabaseManager.context.Participant.Add(input as Participant);
            }
            else if (typeof(T) == typeof(Venue))
            {
                DatabaseManager.context.Venue.Add(input as Venue);
            }
            else if (typeof(T) == typeof(LegalTicket))
            {
                DatabaseManager.context.LegalTickets.Add(input as LegalTicket);
            }
            Save(); 
        }

        public List<T> Retrieve<T>()
        {
            if(typeof(T) == typeof(Concert))
            {
                return DatabaseManager.context.Concert.ToList() as List<T>; 
            } 
            else if(typeof(T) == typeof(Booking))
            {
                return DatabaseManager.context.Booking.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(Ticket))
            {
                return DatabaseManager.context.Ticket.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(ApplicationUser))
            {
                return DatabaseManager.context.ApplicationUser.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(Participant))
            {
                return DatabaseManager.context.Participant.Include(p => p.Ticket).ToList() as List<T>;
            }
            else if (typeof(T) == typeof(Venue))
            {
                return DatabaseManager.context.Venue.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(LegalTicket))
            {
                return DatabaseManager.context.LegalTickets.ToList() as List<T>;
            }
            return new List<T>(); 
        }
        public void Save()
        {
            DatabaseManager.context.SaveChanges();
        }
    }
}
