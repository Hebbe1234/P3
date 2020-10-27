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
        public int CurrentBookings(int id)
        {
            return concerts.Find(c => c.ID == id).Bookings?.Count() ?? 0; 
        }

        public PartialViewResult OnGetConcertPartial(string sortBy)
        {
            this.SortBy = sortBy;
            return Partial("Partials/_ConcertTable", this);
        }
    }
}
