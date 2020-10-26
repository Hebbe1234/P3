using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class VenuePageModel : PageModel
    {
        public Venue venue { get; set; } = new Venue("Hej", "Mit Hus", 55);
        public List<Concert> concerts { get; set; }
        public VenuePageModel()
        {
            venue = FestivalManager.instance._calendar.GetVenue(id);
            concerts = FestivalManager.instance._calendar.GetConcertsAtVenue(venue.Name);
        }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; } = 1; 

        [BindProperty (SupportsGet =true)]
        public string SortBy { get; set; }
        public void OnGet(int id)
        {
            venue = FestivalManager.instance._calendar.GetVenue(id);
            concerts = FestivalManager.instance?._calendar?.GetConcertsAtVenue(venue.Name); 
        }
        public IActionResult OnPostSortByArtist(int id)
        {

            return RedirectToPage("./VenuePage", new { id = id, SortBy = "Artist" });

        }
        public IActionResult OnPostSortByCapacity()
        {
            return RedirectToPage("./VenuePage", new { id = id, SortBy = "Capacity" }) ;

        }
        public IActionResult OnPostSortByTime()
        {
            return RedirectToPage("./VenuePage", new { id = id, SortBy = "Time" });
        }
        public List<Concert> SortConcerts(List<Concert> sortConcerts)
        {
            if(SortBy == "Artist")
            {
                return sortConcerts.OrderBy(c => c.Artist).ToList<Concert>(); 
            } else if(SortBy == "Capacity")
            {
                return sortConcerts.OrderBy(c => c.Venue.Capacity).ToList<Concert>();
            } else if(SortBy == "Time")
            {
                return sortConcerts.OrderBy(c => c.Start).ToList<Concert>();
            } else
            {
                return sortConcerts.OrderBy(c => c.Start).ToList<Concert>();
            }
        }
    }
}
