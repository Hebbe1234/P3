using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class VenuePageModel : PageModel
    {

        public Venue venue { get; set; }
        public List<Concert> concerts { get; set; }
        public IDataAccess DataAccess { get; set; }
        public VenuePageModel(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; } = 1; 

        [BindProperty (SupportsGet =true)]
        public string SortBy { get; set; }
        public void OnGet(int id)
        {
            venue = FestivalManager.instance._calendar.GetVenue(id);
            concerts = FestivalManager.instance?._calendar?.GetConcertsAtVenue(venue.ID); 
        }

        public List<Concert> SortConcerts(List<Concert> sortConcerts)
        {
            if(SortBy == "Artist")
            {
                var list = sortConcerts.OrderBy(c => c.Artist).ToList<Concert>();
                return list;
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

        public PartialViewResult OnGetVenueConcertsPartial(string sortBy, int id)
        {
            this.SortBy = sortBy;
            this.id = id;
            this.venue = FestivalManager.instance._calendar.GetVenue(id);
            this.concerts = FestivalManager.instance?._calendar?.GetConcertsAtVenue(venue.ID);
            return Partial("Partials/_VenueConcertTable", this);
        }
           
        public IActionResult OnPostDeleteVenue(int id)
        {
            Venue deleteVenue = FestivalManager.instance._calendar.GetVenue(id);
            FestivalManager.instance._calendar.DeleteVenue(deleteVenue);
            return RedirectToPage("./VenueOverview");
        }
    }
}
