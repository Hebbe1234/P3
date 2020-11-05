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
        public Participant()
        {
            State = ParticipantState.ACTIVE;
        }
        public Participant(Ticket _ticket): 
            this()
        {
            Ticket = _ticket;
        }
        public Participant(Ticket _ticket, string _name, string _email):
            this(_ticket)
        {
            Name = _name;
            Username = _email; 
        }
        public bool CanMakeBookingAt(Concert concert)
        {
			List<Concert> bookedConcerts =  FestivalManager.instance._calendar.GetConcerts().FindAll(c => c.Bookings.Find(b => b.Participant == this) != null);
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
           return (FestivalManager.instance._calendar
                .GetConcerts()
                .SelectMany(c => c.Bookings))
                .ToList()
                .FindAll(b => b.Participant.ID == this.ID);
        }
    }
}