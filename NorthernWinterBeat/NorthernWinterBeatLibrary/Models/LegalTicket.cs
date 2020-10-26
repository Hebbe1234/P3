using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class LegalTicket
    {
        [Key]
        public string TicketNumber { get; set; }
    }
}
