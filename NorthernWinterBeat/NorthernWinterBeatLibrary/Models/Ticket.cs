using System;


public class Ticket
{
    public Ticket(int _ticketNumber)
    {
        TicketNumber = _ticketNumber;
        State = "Await validation"; 
    }

    private int TicketNumber { get; set; }
    public string State { get; set; }
}

