using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantShowBookingModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public string ticketNumber { get; private set; }
        private IFestivalManager FestivalManager { get; }
        public Participant Participant { get; set; }
        public Concert Concert { get; set; }
        public ParticipantShowBookingModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }
        public void OnGet(int bookingID = -1)
        {
            Id = bookingID;
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).FirstOrDefault();
            var bookings = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).FirstOrDefault()?.GetParticipantBookings(FestivalManager);
            if(bookings != null)
            {
                var booking = bookings.Find(b => b.ID == bookingID);
                if (booking == null || booking.State == Booking.BookingState.INACTIVE)
                {
                    Concert = null;
                    Participant = null;
                }
                else
                {
                    Participant = FestivalManager.GetParticipant(booking.Participant.ID);
                    Concert = FestivalManager.Calendar.GetConcert(booking.Concert.ID);
                    booking.Disable();
                }
            }
        }
    }
}
