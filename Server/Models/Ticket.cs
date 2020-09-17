using System;


public class Ticket
{
    public Ticket(int ticketNumber, string owner)
    {
        TicketNumber = ticketNumber;
        Owner = owner;
    }

    private int TicketNumber { get; set; }
	public string Owner { get; set; }

}

