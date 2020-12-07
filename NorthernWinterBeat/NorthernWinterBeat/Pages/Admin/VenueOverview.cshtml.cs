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
    public class VenuesOverviewModel : PageModel
    {
        public List<Venue> Venues { get; set; } = new List<Venue>();

        [BindProperty (SupportsGet =true)]
        public string SortBy { get; set; }
        private IFestivalManager FestivalManager { get; }

        public VenuesOverviewModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
            Venues = FestivalManager.Calendar.GetVenues();
        }

        public void OnGet()
        {
            Venues = FestivalManager.Calendar.GetVenues(); 
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

        public PartialViewResult OnGetVenuesPartial(string sortBy)
        {
            this.SortBy = sortBy;
            return Partial("Partials/_VenuesTable", this);
        }
    }
}
