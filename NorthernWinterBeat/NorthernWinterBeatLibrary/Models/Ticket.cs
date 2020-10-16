using System;


public class Ticket
{
    public enum TicketState
    {
        AWAIT_VALIDATION
    }
    public Ticket(int _ticketNumber)
    {
        TicketNumber = _ticketNumber;
        State = TicketState.AWAIT_VALIDATION; 
    }

    private int TicketNumber { get; set; }
    public TicketState State { get; set; }
}

