using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NorthernWinterBeat.Models
{	public class Participant : User
	{
		public enum ParticipantState
        {
			ACTIVE, INACTIVE
        }

        public ParticipantState State { get; set; }
        public string Name { get; set; } = "";
		public Ticket Ticket { get; protected set; }
        private IFestivalManager FestivalManager { get; }

        public Participant()
        {
            State = ParticipantState.ACTIVE;
        }

        public Participant(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }
        public Participant(Ticket _ticket, IFestivalManager _festivalManager): 
            this(_festivalManager)
        {
            Ticket = _ticket;
        }
        public virtual bool CanMakeBookingAt(Concert concert)
        {
			List<Concert> bookedConcerts =  FestivalManager.Calendar.GetConcerts().FindAll(c => c.Bookings.Find(b => b.Participant == this) != null);
            foreach (var c in bookedConcerts)
            {
				if(concert.Start < c.End && concert.End > c.Start)
                {
					return false;
                }
            }
			return true;
        }	

        public List<Booking> GetParticipantBookings()
        {
           return (FestivalManager.Calendar
                .GetConcerts()
                .SelectMany(c => c.Bookings))
                .ToList()
                .FindAll(b => b.Participant.ID == this.ID);
        }
    }
}