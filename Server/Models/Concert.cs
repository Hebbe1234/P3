using System;
using System.Collections.Generic;

public struct ConcertHost
{
	public string Name { get; set; }
	public string Address { get; set; }
}

public class Concert
{
	public Concert(DateTime start, DateTime end, int capacity, ConcertHost host)
	{

	}
	
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
