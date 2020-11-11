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
        public EFDataAccess(IDatabaseManager databaseManager)
        {
            DatabaseManager = databaseManager;
        }

        private IDatabaseManager DatabaseManager { get; }

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
            else if (typeof(T) == typeof(ParticipantEntity))
            {
                DatabaseManager.context.Participant.Add(input as ParticipantEntity);
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

        public void Remove<T>(T input)
        {
            if (typeof(T) == typeof(Concert))
            {
                DatabaseManager.context.Concert.Remove(input as Concert);
            }
            else if (typeof(T) == typeof(Booking))
            {
                DatabaseManager.context.Booking.Remove(input as Booking);
            }
            else if (typeof(T) == typeof(Ticket))
            {
                DatabaseManager.context.Ticket.Remove(input as Ticket);
            }
            else if (typeof(T) == typeof(ApplicationUser))
            {
                DatabaseManager.context.ApplicationUser.Remove(input as ApplicationUser);
            }
            else if (typeof(T) == typeof(ParticipantEntity))
            {
                DatabaseManager.context.Participant.Remove(input as ParticipantEntity);
            }
            else if (typeof(T) == typeof(Venue))
            {
                DatabaseManager.context.Venue.Remove(input as Venue);
            }
            else if (typeof(T) == typeof(LegalTicket))
            {
                DatabaseManager.context.LegalTickets.Remove(input as LegalTicket);
            }
            Save();
        }

        public List<T> Retrieve<T>()
        {
            NorthernWinterBeatConcertContext context = new NorthernWinterBeatConcertContext(DatabaseManager.options);

            if(typeof(T) == typeof(Concert))
            {
                return context.Concert.Include(c => c.Bookings).ThenInclude(b => b.Participant).Include(c => c.Venue).ToList() as List<T>; 
            } 
            else if(typeof(T) == typeof(Booking))
            {
                return context.Booking.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(Ticket))
            {
                return context.Ticket.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(ApplicationUser))
            {
                return context.ApplicationUser.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(ParticipantEntity))
            {
                return context.Participant.Include(p => p.Ticket).ToList() as List<T>;
            }
            else if (typeof(T) == typeof(Venue))
            {
                return context.Venue.ToList() as List<T>;
            }
            else if (typeof(T) == typeof(LegalTicket))
            {
                return context.LegalTickets.ToList() as List<T>;
            }
            return new List<T>(); 
        }
        public void Save()
        {
            DatabaseManager.context.SaveChanges();
        }
    }
}
