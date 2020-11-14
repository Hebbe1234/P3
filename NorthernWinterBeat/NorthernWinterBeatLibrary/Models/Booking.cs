using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

public class Booking
{
	public enum BookingState
    {
		ACTIVE, INACTIVE
	}

	public Booking(Participant _participant, Concert _concert, DateTime _bookingDate, IDataAccess dataAccess)
	{
		Participant = _participant;
		Concert = _concert;
		State = BookingState.ACTIVE;
		BookingDate = _bookingDate;
		DataAccess = dataAccess; 
	}
	public Booking(NorthernWinterBeatConcertContext ctx)
	{
		DataAccess = new EFDataAccess(ctx);
		State = BookingState.ACTIVE;
	}

	[Key]
    public int ID { get; set; }
	private IDataAccess DataAccess;
	public BookingState State { get; set; }
    public Participant Participant { get; set; }
	public Concert Concert { get; set; }
    public DateTime BookingDate { get; set; }

	public void Disable()
    {
		State = BookingState.INACTIVE;
		DataAccess.Save(); 
    }
}
