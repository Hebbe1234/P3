using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantBookingsModel : PageModel
    {

        public List<Booking> bookings { get; private set; } = new List<Booking>(); 
        public IFestivalManager FestivalManager { get; }

        public ParticipantBookingsModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }

        public void OnGet()
        {
            bookings = GetBookings();
        }

        private List<Booking> GetBookings()
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).FirstOrDefault();
            var retBookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).FirstOrDefault()?.GetParticipantBookings(FestivalManager);
            return retBookings ?? new List<Booking>();
        }

        public IActionResult OnPostRemoveBooking(int id)
        {
            bookings = GetBookings();

            var booking = bookings.Find(b => b.ID == id);
            booking?.Concert?.RemoveBooking(booking);
            return RedirectToPage("ParticipantBookings");
        }
        public IActionResult OnPostActivateBooking(int id)
        {
            bookings = GetBookings();

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
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).FirstOrDefault();
            Participant p = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).FirstOrDefault();
            await HttpContext.SignOutAsync();
            return RedirectToPage("../Pages/Index");
        }
    }
}
