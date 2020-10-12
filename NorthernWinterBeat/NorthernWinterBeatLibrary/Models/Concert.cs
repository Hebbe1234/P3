using NorthernWinterBeat.Models;
using System;
using System.Collections.Generic;


public class Concert
{

    public Concert()
    {

    }
	public Concert(DateTime _start, DateTime _end, Venue _venue, string _artist, string _artistDescription)
	{
        Start = _start;
        End = _end;
        Venue = _venue;
        Artist = _artist;
        ArtistDescription = _artistDescription;
        Capacity = Venue.Capacity; 
	}
    public Venue Venue { get; set; }
    public string Artist { get; set; }
    public string ArtistDescription { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Capacity { get; set; }
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
