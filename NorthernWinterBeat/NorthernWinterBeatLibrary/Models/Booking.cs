using System;
using NorthernWinterBeat.Models;

public class Booking
{
	public enum BookingState
    {
		ACTIVE
    }
    public int ID { get; set; }
    public Booking(Participant _participant, Concert _concert)
	{
		Participant = _participant;
		Concert = _concert;
		State = BookingState.ACTIVE; 
	}
    public BookingState State { get; set; }
    public Participant Participant { get; set; }
	public Concert Concert { get; set; }
}
