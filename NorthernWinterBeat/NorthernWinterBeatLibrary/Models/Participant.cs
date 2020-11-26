using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NorthernWinterBeat.Models
{
    public class Participant 
	{
		public enum ParticipantState
        {
			ACTIVE, INACTIVE
        }

        public ParticipantState State { get; set; }
        public string Name { get; set; } = "";
		public Ticket Ticket { get; protected set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int ID { get; set; }

        private IDataAccess DataAccess;
        public Participant()
        {

        }

        public Participant(NorthernWinterBeatConcertContext ctx)
        {
            DataAccess = new EFDataAccess(ctx);
            State = ParticipantState.ACTIVE;
        }

        public Participant(Ticket _ticket, IDataAccess dataAccess) 
        {
            DataAccess = dataAccess;
            Ticket = _ticket;
            
        }

        public Participant(Ticket _ticket, string name, string email, IDataAccess dataAccess) : this(_ticket, dataAccess)
        {
            Name = name;
            Email = email;
            Username = Email; 
        }


        public virtual bool CanMakeBookingAt(Concert concert, IFestivalManager festivalManager)
        {
            List<Concert> bookedConcerts = festivalManager.Calendar.GetConcerts().FindAll(c => c.Bookings.Find(b => b.Participant == this) != null);
            foreach (var c in bookedConcerts)
            {
                if (concert.Start < c.End && concert.End > c.Start)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Booking> GetParticipantBookings(IFestivalManager festivalManager)
        {
            return (festivalManager.Calendar
                 .GetConcerts()
                 .SelectMany(c => c.Bookings))
                 .ToList()
                 .FindAll(b => b.Participant?.ID == this.ID);
        }

        public void Update(Participant NewParticipant)
        {
            Name = NewParticipant?.Name;
            Username = NewParticipant?.Username;
           
            DataAccess.Save();    
        }
    }
}