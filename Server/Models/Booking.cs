using System;

public class Booking
{
	public Booking(Participant participant, Concert concert)
	{
		Participant = participant;
		Concert = concert;
	}

	public Participant Participant { get; set; }
	public Concert Concert { get; set; }

}
