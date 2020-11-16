using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantArtistModel : PageModel
    {
        public Concert concert { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; } = 0;

        private IFestivalManager FestivalManager { get; }

        public ParticipantArtistModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager; 
            concert = FestivalManager.Calendar.GetConcert(id);
        }
        public void OnGet(int id = 0)
        {
            concert = FestivalManager.Calendar.GetConcert(id);
        }

        public IActionResult OnPostBookSpot(int id)
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();

            Concert c = FestivalManager.Calendar.GetConcert(id);
            c.MakeBooking(FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First(), FestivalManager);
            return RedirectToPage("./ParticipantArtist", new { id = id });
        }
        public IActionResult OnPostGoBack(DateTime Day)
        {
            return RedirectToPage("./ParticipantConcertOverview", new { Day = Day });
        }
        public IActionResult OnPostRemoveBooking(int id)
        {
            var claimTicketID = HttpContext.User.Claims.Where(c => c.Type == "TicketID").Select(t => t.Value).First();

            Concert c = FestivalManager.Calendar.GetConcert(id);
            Participant p = FestivalManager.GetParticipants().Where(p => p.Ticket?.TicketNumber == claimTicketID).First();
            Booking b = c.Bookings.Where(b => b.Participant == p).FirstOrDefault();
            c.RemoveBooking(b);
            return RedirectToPage("./ParticipantArtist", new { id = id });
        }
    }
}
