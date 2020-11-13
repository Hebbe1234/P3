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
            bookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First()?.GetParticipantBookings(FestivalManager);
        }
        //String?
        public IActionResult OnPostRemoveBooking(int id)
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();
            bookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First()?.GetParticipantBookings(FestivalManager);

            var booking = bookings.Find(b => b.ID == id);
            booking.Concert.RemoveBooking(booking);
            return RedirectToPage("ParticipantBookings");
        }
        public IActionResult OnPostActivateBooking(int id)
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();
            bookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First()?.GetParticipantBookings(FestivalManager);
            var booking = bookings.Find(b => b.ID == id);

            DateTime concertStartTime = booking.Concert.Start;
            DateTime now = DateTime.Now;
            TimeSpan diffrence = now.Subtract(concertStartTime);
            if(diffrence.TotalMinutes < 30)
            {
                RedirectToPage("ParticipantBookings"); 
            } else
            {
                RedirectToPage("ParticipantShowBooking", new { id = id });
            }

            return RedirectToPage("ParticipantBookings");
        }
    }
}
