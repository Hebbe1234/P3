using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages.VenueRazor
{
    public class IndexModel : PageModel
    {
        public List<Concert> Concerts { get; set; } = new List<Concert>() {
            new Concert( new DateTime(2021, 01, 28, 20, 00, 00), new DateTime(2021, 01, 28, 20, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 35), "Martin Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 21, 00, 00), new DateTime(2021, 01, 28, 21, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 35), "ANders Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 19, 00, 00), new DateTime(2021, 01, 28, 19, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 35), "Martsefs Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 19, 45, 00), new DateTime(2021, 01, 28, 20, 45, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 35), "Marsefsetin Hansen", "Fuckig sejg") };

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostSortByTime()
        {
            return RedirectToPage("./IndexVenue", new { SortBy = "Time" });
        }
        public IActionResult OnPostSortByArtist()
        {
            return RedirectToPage("./IndexVenue", new { SortBy = "Artist" });
        }
        public IActionResult OnPostSortByCapacity()
        {
            return RedirectToPage("./IndexVenue", new { SortBy = "Capacity" });
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
            else if (SortBy == "Capacity")
            {
                SortConcert = SortConcert.OrderBy(o => o.Venue.Capacity).ThenBy(o => o.Start).ToList<Concert>();
            }

            return SortConcert;
        }
    }
}