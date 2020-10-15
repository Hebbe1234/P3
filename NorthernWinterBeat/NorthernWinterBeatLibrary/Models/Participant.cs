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

		private List<Booking> Bookings = new List<Booking>();

		public void AddBooking(Booking booking)
		{
			Bookings.Add(booking);
		}
		public void RemoveBooking(Booking booking)
		{
			Bookings.Remove(booking);
		}
	}

	
}