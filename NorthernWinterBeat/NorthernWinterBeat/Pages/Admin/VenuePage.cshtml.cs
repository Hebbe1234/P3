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
            //venue = FestivalManager.instance._calendar.GetVenue(id);
            //concerts = FestivalManager.instance._calendar.GetConcertsAtVenue(venue.Name);
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
            this.concerts = FestivalManager.instance?._calendar?.GetConcertsAtVenue(venue.Name);
            return Partial("Partials/_VenueConcertTable", this);
        }
           
        public async Task<IActionResult> OnPostDeleteVenue(int id)
        {
            Venue deleteVenue = FestivalManager.instance._calendar.GetVenue(id);
            await FestivalManager.instance._calendar.DeleteVenue(deleteVenue);
            return RedirectToPage("./venues");
        }




        public string FindStartTime(int id)
        {
            string hour = ("00" + concerts.Find(c => c.ID == id).Start.Hour);
            string minute = ("00" + concerts.Find(c => c.ID == id).Start.Minute);

            minute = minute.Substring(minute.Length - 2);
            hour = hour.Substring(hour.Length - 2);

            return hour + ":" + minute;
        }
        public string FindEndTime(int id)
        {
            string hour = ("00" + concerts.Find(c => c.ID == id).End.Hour);
            string minute = ("00" + concerts.Find(c => c.ID == id).End.Minute);

            minute = minute.Substring(minute.Length - 2);
            hour = hour.Substring(hour.Length - 2);

            return hour + ":" + minute;
        }
        public string FindDate(int id)
        {
            Concert concert = concerts.Find(c => c.ID == id);
            string days = "00" + concert.Start.Date.Day;
            string month = "00" + concert.Start.Date.Month;
            return days.Substring(days.Length - 2) + "-" + month.Substring(month.Length - 2);
        }
    }
}
