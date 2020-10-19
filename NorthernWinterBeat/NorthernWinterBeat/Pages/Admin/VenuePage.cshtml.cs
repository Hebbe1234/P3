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
        //public VenuePageModel()
        //{
        //    venue = FestivalManager.instance._calendar.GetVenue(id);
        //    concerts = FestivalManager.instance._calendar.GetConcertsAtVenue(venue.Name);
        //}
        public void OnGet(int id = 0)
        {
            venue = FestivalManager.instance._calendar.GetVenue(id);
            concerts = FestivalManager.instance?._calendar?.GetConcertsAtVenue(venue.Name); 
        }

    }
}
