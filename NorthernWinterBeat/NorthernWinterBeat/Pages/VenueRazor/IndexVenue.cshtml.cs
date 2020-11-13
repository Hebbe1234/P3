using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.VenueRazor
{
    public class IndexModel : PageModel
    {
        public List<Concert> concerts { get; set; } = new List<Concert>();

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        private IFestivalManager FestivalManager { get; }

        public IndexModel(IFestivalManager festivalManager, int id = 3)
        {
            FestivalManager = festivalManager;
            concerts = FestivalManager.Calendar.GetConcertsAtVenue(id); 
        }

        public void OnGet(int id = 3)
        {
            concerts = FestivalManager.Calendar.GetConcertsAtVenue(id);
        }

        public List<Concert> SortConcertBy(List<Concert> SortConcert)
        {
            if (SortBy == "Artist" || SortBy == null)
            {
                SortConcert = SortConcert.OrderBy(o => o.Artist).ToList<Concert>();
            }
            else if (SortBy == "Time")
            {
                SortConcert = SortConcert.OrderBy(o => o.Start).ThenBy(o => o.End).ThenBy(o => o.Artist).ToList<Concert>();
            }
            else if (SortBy == "Venue")
            {
                SortConcert = SortConcert.OrderBy(o => o.Venue.Name).ThenBy(o => o.Start).ToList<Concert>();
            }
            else if (SortBy == "Capacity")
            {
                SortConcert = SortConcert.OrderBy(c => c.Bookings.Count()).ToList<Concert>(); 
            }
            return SortConcert;
        }
        public PartialViewResult OnGetVenueRazorPartial(string sortBy)
        {
            this.SortBy = sortBy;
            return Partial("Partials/_VenueRazorConcertTable", this);
        }
    }
}