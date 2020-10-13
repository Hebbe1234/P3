using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages.Admin.Pages
{
    public class ConcertModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string Artist = Request.Form["ArtistEntered"];
            string Description = Request.Form["DescriptionEntered"];
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

            return RedirectToPage("./Concert");
        }

    }
}