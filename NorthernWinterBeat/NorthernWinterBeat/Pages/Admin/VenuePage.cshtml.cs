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
        public Venue Venue { get; set; }
        public List<Concert> Concerts { get; set; }
        private IFestivalManager FestivalManager { get; }

        public VenuePageModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; } = 1; 

        [BindProperty (SupportsGet =true)]
        public string SortBy { get; set; }
        public void OnGet(int id)
        {
            Venue = FestivalManager.Calendar.GetVenue(id);
            Concerts = FestivalManager.Calendar?.GetConcertsAtVenue(Venue.ID); 
        }

        public List<Concert> SortConcerts(List<Concert> sortConcerts)
        {
            if(SortBy == "Artist")
            {
                var list = sortConcerts.OrderBy(c => c.Artist).ThenBy(c => c.Start).ToList<Concert>();
                return list;
            } else if(SortBy == "Capacity")
            {
                return sortConcerts.OrderBy(c => c.Venue?.Capacity).ThenBy(c => c.Start).ToList<Concert>();
            } else if(SortBy == "Time")
            {
                return sortConcerts.OrderBy(c => c.Start).ThenBy(c => c.Artist).ToList<Concert>();
            } else
            {
                return sortConcerts.OrderBy(c => c.Start).ThenBy(c => c.Artist).ToList<Concert>();
            }
        }

        public PartialViewResult OnGetVenueConcertsPartial(string sortBy, int id)
        {
            this.SortBy = sortBy;
            this.id = id;
            this.Venue = FestivalManager.Calendar.GetVenue(id);
            this.Concerts = FestivalManager.Calendar?.GetConcertsAtVenue(Venue.ID);
            return Partial("Partials/_VenueConcertTable", this);
        }
           
        public IActionResult OnPostDeleteVenue(int id)
        {
            Venue deleteVenue = FestivalManager.Calendar.GetVenue(id);
            FestivalManager.Calendar.DeleteVenue(deleteVenue);
            return RedirectToPage("./VenueOverview");
        }
    }
}
