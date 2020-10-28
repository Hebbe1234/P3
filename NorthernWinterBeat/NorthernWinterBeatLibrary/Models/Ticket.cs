using System;
using System.ComponentModel.DataAnnotations;

public class Ticket
{
    public enum TicketState
    {
        AWAIT_VALIDATION, ACTIVE, INACTIVE
    }
    public Ticket(string _ticketNumber)
    {
        TicketNumber = _ticketNumber;
        State = TicketState.AWAIT_VALIDATION; 
    }
    public Ticket()
    {
    }
    [Key]
    public int ID { get; set; }
    private string TicketNumber { get; set; }
    public TicketState State { get; set; }
}

