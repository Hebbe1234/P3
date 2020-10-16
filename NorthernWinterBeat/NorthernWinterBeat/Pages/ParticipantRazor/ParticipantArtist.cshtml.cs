using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantArtistModel : PageModel
    {
        public Concert concert { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; } = 0;

        public ParticipantArtistModel()
        {
            concert = FestivalManager.instance._calendar.GetConcert(id);
        }
        public void OnGet(int id = 0)
        {

            concert = FestivalManager.instance._calendar.GetConcert(id);

        }

        public IActionResult OnPostBookSpot(int id)
        {
            Concert c = FestivalManager.instance._calendar.GetConcert(id);
            c.MakeBooking(FestivalManager.instance.GetParticipants().First());
            return RedirectToPage("./ParticipantArtist", new { id = id });
        }
    }
}
