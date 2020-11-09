using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantBookingsModel : PageModel
    {

        public List<Booking> bookings { get; private set; }
        private IFestivalManager FestivalManager { get; }

        public ParticipantBookingsModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }

        public void OnGet()
        {

            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();
            bookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First()?.GetParticipantBookings();
        }

        public IActionResult OnPostRemoveBooking(string id)
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();
            bookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First()?.GetParticipantBookings();

            var booking = bookings.Find(b => b.ID.ToString() == id);
            booking.Concert.RemoveBooking(booking);
            return RedirectToPage("ParticipantBookings");
        }
    }

}
