using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary;
using NorthernWinterBeat.Models;

namespace NorthernWinterBeat.Pages.Admin.Pages
{
    public class ConcertModel : PageModel
    {
        public List<Venue> venues { get; set; } = new List<Venue>(); 

        public ConcertModel()
        {
            venues = FestivalManager.instance._calendar.GetVenues(); 
        }
        public void OnGet()
        {
            venues = FestivalManager.instance._calendar.GetVenues(); 
        }

        public IActionResult OnPostCreateConcert()
        {
            string Artist = Request.Form["ArtistEntered"];
            string Description = Request.Form["DescriptionEntered"];
            string Image = Request.Form["ImageEntered"];   // Dette virker lidt fjollet umiddelbart. 
            string Venue = Request.Form["VenueEntered"];
            string Date = Request.Form["DateEntered"];
            string StartTime = Request.Form["StartTimeEntered"];
            string EndTime = Request.Form["EndTimeEntered"];

            int Year = int.Parse(Date.Substring(0, 4));
            int Month = int.Parse(Date.Substring(5, 2));
            int Day = int.Parse(Date.Substring(8, 2));
            int StartHour = int.Parse(StartTime.Substring(0, 2));
            int StartMinute = int.Parse(StartTime.Substring(3, 2));
            int EndHour = int.Parse(EndTime.Substring(0, 2));
            int EndMinute = int.Parse(EndTime.Substring(3, 2));

            DateTime Start = new DateTime(Year, Month, Day, StartHour, StartMinute, 0);
            DateTime End = new DateTime(Year, Month, Day, EndHour, EndMinute, 0);
            Concert NewConcert = new Concert(Start, End, Artist, Description);

            FestivalManager.instance._calendar.AddConcert(NewConcert, Venue);
            return RedirectToPage("./Calendar");
        }
    }
}

       