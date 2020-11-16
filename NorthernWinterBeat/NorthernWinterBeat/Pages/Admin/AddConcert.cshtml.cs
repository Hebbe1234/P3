using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;

namespace NorthernWinterBeat.Pages.Admin.Pages
{
    public class AddConcertModel : PageModel
    {
        public List<Venue> venues { get; set; } = new List<Venue>();
        public IDataAccess DataAccess { get; set; }
        private IFestivalManager FestivalManager { get; }

        public AddConcertModel(IDataAccess dataAccess, IFestivalManager festivalManager)
        {
            DataAccess = dataAccess;
            FestivalManager = festivalManager;
            venues = FestivalManager.Calendar.GetVenues(); 
        }
        public void OnGet()
        {
            venues = FestivalManager.Calendar.GetVenues(); 
        }

        public IActionResult OnPostAsyncCreateConcert()
        {
            string Artist = Request.Form["ArtistEntered"];
            string Description = Request.Form["DescriptionEntered"];
            string Image = Request.Form["ImageEntered"];   // Dette virker lidt fjollet umiddelbart. 
            string Venue = Request.Form["VenueEntered"];
            string Date = Request.Form["DateEntered"];
            string StartTime = Request.Form["StartTimeEntered"];
            string EndTime = Request.Form["EndTimeEntered"];
            int Year = 0001, Month = 01, StartDay = 01, EndDay = 01;
            int StartHour = 0, StartMinute = 0, EndHour = 0, EndMinute = 0; 
            if (Date != "")
            {
                Year = int.Parse(Date.Substring(0, 4));
                Month = int.Parse(Date.Substring(5, 2));
                StartDay = EndDay = int.Parse(Date.Substring(8, 2));
            }
            if(StartTime != "")
            {
                StartHour = int.Parse(StartTime.Substring(0, 2));
                StartMinute = int.Parse(StartTime.Substring(3, 2));
            }
            if(EndTime != "")
            {
                EndHour = int.Parse(EndTime.Substring(0, 2));
                EndMinute = int.Parse(EndTime.Substring(3, 2));
            }
            if(StartHour > EndHour ||((StartHour == EndHour) && (StartMinute > EndMinute)))
            {
                EndDay++; 
            }

            DateTime Start = new DateTime(Year, Month, StartDay, StartHour, StartMinute, 0);
            DateTime End = new DateTime(Year, Month, EndDay, EndHour, EndMinute, 0);
            Concert NewConcert = new Concert(Start, End, Artist, Description, DataAccess);

            FestivalManager.Calendar.AddConcert(NewConcert, Venue);
            return RedirectToPage("./Calendar");
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("./VenueOverview");
        }
    }
}

       