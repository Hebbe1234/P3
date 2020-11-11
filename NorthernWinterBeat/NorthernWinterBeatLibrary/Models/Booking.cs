using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Models;

public class Booking
{
	public enum BookingState
    {
		ACTIVE, INACTIVE
	}
	public Booking(Participant _participant, Concert _concert, DateTime _bookingDate)
	{
		Participant = _participant;
		Concert = _concert;
		State = BookingState.ACTIVE;
		BookingDate = _bookingDate;
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
