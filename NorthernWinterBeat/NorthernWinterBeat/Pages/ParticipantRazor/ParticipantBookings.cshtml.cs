using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantBookingsModel : PageModel
    {

        public List<Booking> bookings { get; private set; } = new List<Booking>();

        public void OnGet()
        {

            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();
            bookings = FestivalManager.instance.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First()?.GetParticipantBookings();
        }
    }
}
