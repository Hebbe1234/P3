using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.VenueRazor
{
    public class IndexModel : PageModel
    {
        public List<Concert> concerts { get; set; } = new List<Concert>();
        public Venue venue { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        private IFestivalManager FestivalManager { get; }

        public IndexModel(IFestivalManager festivalManager, int id = 3)
        {
            FestivalManager = festivalManager;

            //var currentVenueIDstring = HttpContext.User.Claims.Where(c => c.Type == "VenueID").Select(c => c.Value).FirstOrDefault();
            //var currentVenueID = int.Parse(currentVenueIDstring);

            //concerts = FestivalManager.Calendar.GetConcertsAtVenue(id);
            //venue = FestivalManager.Calendar.GetVenue(id);
        }

        public void OnGet()
        {
            var currentVenueIDstring = HttpContext.User.Claims.Where(c => c.Type == "VenueID").Select(c => c.Value).FirstOrDefault();
            var currentVenueID = int.Parse(currentVenueIDstring);

            concerts = FestivalManager.Calendar.GetConcertsAtVenue(currentVenueID);
            venue = FestivalManager.Calendar.GetVenue(currentVenueID);
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