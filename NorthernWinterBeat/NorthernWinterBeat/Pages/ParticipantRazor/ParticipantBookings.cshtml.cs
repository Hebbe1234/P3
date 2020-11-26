using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantBookingsModel : PageModel
    {

        public List<Booking> bookings { get; private set; }
        public IFestivalManager FestivalManager { get; }

        public ParticipantBookingsModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }

        public void OnGet()
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();
            bookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First()?.GetParticipantBookings(FestivalManager);
        }
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
            TimeSpan diffrence = concertStartTime.Subtract(now);

            if(diffrence.TotalMinutes < 30)
            {
                return RedirectToPage("ParticipantShowBooking", new { bookingID = id });
            } else
            {
                return RedirectToPage("ParticipantBookings");
            }
        }
        public async Task<IActionResult> OnSignOutAsync()
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();
            Participant p = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First();
            await HttpContext.SignOutAsync();
            return RedirectToPage("../Pages/Index");
        }
    }
}
