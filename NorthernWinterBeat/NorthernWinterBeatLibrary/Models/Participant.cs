using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}