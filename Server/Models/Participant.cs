using System;
using System.Collections.Generic;

public class Participant 
{
	public Participant(Ticket _ticket)
	{
		Ticket = _ticket; 

	}

    public Ticket Ticket { get; protected set; }

	private List<Booking> bookings = new List<Booking>();
	
	public void AddBooking(Booking booking)
    {
		bookings.Add(booking);
    }
	public void RemoveBooking(Booking booking)
    {
		bookings.Remove(booking);
    }
}
