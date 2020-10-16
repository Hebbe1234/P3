using NorthernWinterBeat.Models;
using System;
using System.Collections.Generic;


public class Concert
{

    public enum ConcertState
    {
        CREATION, MAX_CAPACITY
    }

    public Concert()
    {
        State = ConcertState.CREATION; 
    }
    public Concert(DateTime _start, DateTime _end, string _artist, string _artistDescription)
    {
        Start = _start;
        End = _end;
        Artist = _artist;
        ArtistDescription = _artistDescription;
        State = ConcertState.CREATION;
    }
    public Concert(DateTime _start, DateTime _end, Venue _venue, string _artist, string _artistDescription)
	{
        Start = _start;
        End = _end;
        Venue = _venue;
        Artist = _artist;
        ArtistDescription = _artistDescription;
        State = ConcertState.CREATION; 
	}
    public int ID { get; set; }
    public ConcertState State { get; set; }
    public Venue Venue { get; set; }
    public string Artist { get; set; }
    public string ArtistDescription { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<Booking> Bookings = new List<Booking>();

	public void AddBooking(Booking booking)
    {
		Bookings.Add(booking);
    }
	public void RemoveBooking(Booking booking)
    {
		Bookings.Remove(booking);
    }

    public Booking MakeBooking(Participant p)
    {
        Booking booking = null;

        if (State != ConcertState.MAX_CAPACITY && p.CanMakeBookingAt(this))
        {
            booking =  new Booking(p, this);
            Bookings.Add(booking);
        }

        
        return booking;
    }
}
