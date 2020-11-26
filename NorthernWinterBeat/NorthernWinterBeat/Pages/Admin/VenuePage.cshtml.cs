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
        private IDataAccess DataAccess { get; set; }
        private IFestivalManager FestivalManager { get; }

        public VenuePageModel(IDataAccess dataAccess, IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
            DataAccess = dataAccess;
        }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; } = 1; 

        [BindProperty (SupportsGet =true)]
        public string SortBy { get; set; }
        public void OnGet(int id)
        {
            venue = FestivalManager.Calendar.GetVenue(id);
            concerts = FestivalManager.Calendar?.GetConcertsAtVenue(venue.ID); 
        }

        public List<Concert> SortConcerts(List<Concert> sortConcerts)
        {
            if(SortBy == "Artist")
            {
                var list = sortConcerts.OrderBy(c => c.Artist).ToList<Concert>();
                return list;
            } else if(SortBy == "Capacity")
            {
                return sortConcerts.OrderBy(c => c.Venue?.Capacity).ToList<Concert>();
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
            this.venue = FestivalManager.Calendar.GetVenue(id);
            this.concerts = FestivalManager.Calendar?.GetConcertsAtVenue(venue.ID);
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
