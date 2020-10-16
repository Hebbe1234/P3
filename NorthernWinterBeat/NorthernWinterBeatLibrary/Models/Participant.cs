﻿using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;

namespace NorthernWinterBeat.Models
{	public class Participant
	{
		public Participant(Ticket _ticket)
		{
			Ticket = _ticket;
			State = "Active"; 
		}
		public int ID { get; set; }
        public string State { get; set; }
        public string Name { get; set; } = "";
		public Ticket Ticket { get; protected set; }
		public string Username { get; set; }
		public string Password { get; set; }

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