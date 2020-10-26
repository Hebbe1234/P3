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
    public class VenuesModel : PageModel
    {

        public List<Venue> venues { get; set; } = new List<Venue>();

        [BindProperty (SupportsGet =true)]
        public string SortBy { get; set; }
        public VenuesModel()
        {
            venues = FestivalManager.instance._calendar.GetVenues();
        }

        public void OnGet()
        {
            venues = FestivalManager.instance._calendar.GetVenues(); 
        }

        public IActionResult OnPostSortByVenue()
        {
            return RedirectToPage("./Venues", new { SortBy = "Venue" });

        }
        public IActionResult OnPostSortByCapacity()
        {
            return RedirectToPage("./Venues", new { SortBy = "Capacity" });
        }
        public IActionResult OnPostSortByAddress()
        {
            return RedirectToPage("./Venues", new { SortBy = "Address" });
        }
        
        public List<Venue> SortVenues(List<Venue> SortVenues)
        {
            if(SortBy == "Venue")
            {
                return SortVenues.OrderBy(o => o.Name).ToList<Venue>(); 
            } else if(SortBy == "Capacity")
            {
                return SortVenues.OrderBy(o => o.Capacity).ThenBy(o => o.Name).ToList<Venue>(); 
            } else if(SortBy == "Address")
            { 
                return SortVenues.OrderBy(o => o.Address).ToList<Venue>(); 
            } else
            {
                return SortVenues.OrderBy(o => o.Name).ToList<Venue>();
            }
        }
    }
}
