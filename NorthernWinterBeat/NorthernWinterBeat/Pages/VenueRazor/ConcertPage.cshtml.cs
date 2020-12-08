using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.VenueRazor
{
    public class ConcertPageModel : PageModel
    {
        public Concert concert { get; private set; }
        private IFestivalManager FestivalManager { get; }
        public ConcertPageModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager; 
        }
        public IActionResult OnGet(int id)
        {
            var currentVenueIDstring = HttpContext.User.Claims.Where(c => c.Type == "VenueID").Select(c => c.Value).FirstOrDefault();
            var currentVenueID = int.Parse(currentVenueIDstring);
            if(FestivalManager.Calendar.GetConcertsAtVenue(currentVenueID).Select(c => c.ID).Contains(id)) {
                concert = FestivalManager.Calendar?.GetConcert(id);
            } else
            {
                return RedirectToPage("./IndexVenue");
            }

            return Page();
        }
    }
}
