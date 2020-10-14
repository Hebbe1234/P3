using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages.Venue
{
    public class TabelVenueModel : PageModel
    {
        public List<Concert> Concerts { get; set; } = new List<Concert>() {
            new Concert( new DateTime(2021, 01, 28, 20, 00, 00), new DateTime(2021, 01, 28, 20, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 33), "Martin Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 21, 00, 00), new DateTime(2021, 01, 28, 21, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 3", 36), "ANders Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 19, 00, 00), new DateTime(2021, 01, 28, 19, 30, 00), new Models.Venue("Lejlighed", "Borgergase 4", 38), "Martsefs Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 19, 45, 00), new DateTime(2021, 01, 28, 20, 45, 00), new Models.Venue("Lejlighed", "Borgergase 5", 30), "Marsefsetin Hansen", "Fuckig sejg") };
        
        [BindProperty (SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty (SupportsGet = true)]
        public string VenueName { get; set; }





        public void OnGet()
        {
        }
        public IActionResult OnPostSortByTime()
        {
            return RedirectToPage("./TabelVenue", new { SortBy = "Time" });
        }
        public IActionResult OnPostSortByArtist()
        {
            return RedirectToPage("./TabelVenue", new { SortBy = "Artist" });
        }
        public IActionResult OnPostSortByCapacity()
        {
            return RedirectToPage("./TabelVenue", new { SortBy = "Capacity" });
        }
        public List<Concert> SortConcertsBy()
        {
            if(SortBy == "Time" || SortBy == null)
            {
                return Concerts.OrderBy(o => o.Start).ThenBy(o => o.End).ThenBy(o => o.Artist).ToList<Concert>();
            } else if(SortBy == "Artist")
            {
                return Concerts.OrderBy(o => o.Artist).ThenBy(o => o.Start).ToList<Concert>();
            } else if(SortBy == "Capacity")
            {
                return Concerts.OrderBy(o => o.Capacity).ThenBy(o => o.Start).ToList<Concert>();
            } else
            {
                return Concerts.OrderBy(o => o.Start).ThenBy(o => o.End).ThenBy(o => o.Artist).ToList<Concert>();
            }
        }

    }
}
