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
        public NorthernWinterBeatConcertContext context { get; set; }

        public EFDataAccess(NorthernWinterBeatConcertContext _ctx)
        {
            context = _ctx;
        }


        public void Add<T>(T input)
        {
            if (input == null)
            {
                return;
            }
            if (typeof(T) == typeof(Concert))
            {
                context.Concert?.Add(input as Concert);
            }
            else if (typeof(T) == typeof(Booking))
            {
                context.Booking?.Add(input as Booking);
            }
            else if (typeof(T) == typeof(Ticket))
            {
                context.Ticket.Add(input as Ticket);
            }
            else if (typeof(T) == typeof(ApplicationUser))
            {
                context.ApplicationUser.Add(input as ApplicationUser);
            }
            else if (typeof(T) == typeof(Participant))
            {
                context.Participant?.Add(input as Participant);
            }
            else if (typeof(T) == typeof(Venue))
            {
                context.Venue?.Add(input as Venue);
            }
            else if (typeof(T) == typeof(LegalTicket))
            {
                context.LegalTickets.Add(input as LegalTicket);
            }
            Save(); 
        }

        public void Remove<T>(T input)
        {
            if(input == null)
            {
                return;
            }
            if (typeof(T) == typeof(Concert))
            {
                context.Concert?.Remove(input as Concert);
            }
            else if (typeof(T) == typeof(Booking))
            {
                context.Booking?.Remove(input as Booking);
            }
            else if (typeof(T) == typeof(Ticket))
            {
                context.Ticket.Remove(input as Ticket);
            }
            else if (typeof(T) == typeof(ApplicationUser))
            {
                context.ApplicationUser.Remove(input as ApplicationUser);
            }
            else if (typeof(T) == typeof(Participant))
            {
                context.Participant?.Remove(input as Participant);
            }
            else if (typeof(T) == typeof(Venue))
            {
                context.Venue?.Remove(input as Venue);
            }
            else if (typeof(T) == typeof(LegalTicket))
            {
                context.LegalTickets.Remove(input as LegalTicket);
            }
            Save();
        }

        public List<T> Retrieve<T>()
        {
            try
            {
                if (typeof(T) == typeof(Concert))
                {
                    return context.Concert?.Include(c => c.Bookings).ThenInclude(b => b.Participant).Include(c => c.Venue).ToList() as List<T>;
                }
                else if (typeof(T) == typeof(Booking))
                {
                    return context.Booking?.ToList() as List<T>;
                }
                else if (typeof(T) == typeof(Ticket))
                {
                    return context.Ticket.ToList() as List<T>;
                }
                else if (typeof(T) == typeof(ApplicationUser))
                {
                    return context.ApplicationUser.ToList() as List<T>;
                }
                else if (typeof(T) == typeof(Participant))
                {
                    return context.Participant?.Include(p => p.Ticket).ToList() as List<T>;
                }
                else if (typeof(T) == typeof(Venue))
                {
                    return context.Venue?.ToList() as List<T>;
                }
                else if (typeof(T) == typeof(LegalTicket))
                {
                    return context.LegalTickets.ToList() as List<T>;
                }
                return new List<T>();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return new List<T>(); 
            }
        }
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
