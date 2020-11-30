using System;
using System.ComponentModel.DataAnnotations;

public class Ticket
{
    public Ticket(string _ticketNumber)
    {
        TicketNumber = _ticketNumber;
    }
    public Ticket()
    {
    }
    [Key]
    public int ID { get; set; }
    public string TicketNumber { get; set; }
}

