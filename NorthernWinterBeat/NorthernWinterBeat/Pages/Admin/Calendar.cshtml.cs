using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;


namespace NorthernWinterBeat.Pages.Admin
{
    public class CalendarModel : PageModel
    {
        public List<Concert> concerts { get; set; } = new List<Concert>();


        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        //[BindProperty (SupportsGet = true)] 
        //public int ConcertClicked { get; set; }
        public CalendarModel()
        {
            concerts = FestivalManager.instance._calendar.GetConcerts();
        }
        public void OnGet()
        {
            concerts = FestivalManager.instance._calendar.GetConcerts();
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
                SortConcert = SortConcert.OrderBy(o => o.Venue.Capacity).ThenBy(o => o.Start).ToList<Concert>();
            }

            return SortConcert;
        }

        public PartialViewResult OnGetConcertPartial(string sortBy)
        {
            this.SortBy = sortBy;
            return Partial("_ConcertTable", this);
        }
    }
}
