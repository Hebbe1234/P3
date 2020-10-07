using System;

public class Booking
{
	public Booking(Participant _participant, Concert _concert)
	{
		Participant = _participant;
		Concert = _concert;
	}

	public Participant Participant { get; set; }
	public Concert Concert { get; set; }

}
