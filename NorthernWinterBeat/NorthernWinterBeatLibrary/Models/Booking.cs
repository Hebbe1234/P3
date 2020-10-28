using System;
using System.ComponentModel.DataAnnotations;
using NorthernWinterBeat.Models;

public class Booking
{
	public enum BookingState
    {
		ACTIVE, INACTIVE
	}
	public Booking(Participant _participant, Concert _concert)
	{
		Participant = _participant;
		Concert = _concert;
		State = BookingState.ACTIVE;
	}
	public Booking()
    {

    }
	[Key]
    public int ID { get; set; }

    public BookingState State { get; set; }
    public Participant Participant { get; set; }
	public Concert Concert { get; set; }
    public DateTime BookingDate { get; set; }
}
