using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages
{
    public class ParticipantConcertOverviewModel : PageModel
    {


        public List<Concert> Concerts = new List<Concert>();

        [BindProperty(SupportsGet = true)]
        public DateTime? Day { get; set; } = null;
        private IFestivalManager FestivalManager { get; }

        public ParticipantConcertOverviewModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }
        public void OnGet(DateTime? Day)
        {
            Concerts = FestivalManager.Calendar.GetConcerts();

            if (Day == null)
            {
                if(Concerts.Count == 0)
                {
                    int Year = 0001, Month = 01, DayDate = 01, StartHour = 0, StartMinute = 0;
                    DateTime Start = new DateTime(Year, Month, DayDate, StartHour, StartMinute, 0);

                    this.Day = Start;
                } else
                {
                    this.Day = Concerts.First().Start;
                }
            }
        }

        public IActionResult OnGetSelectArtist(int? id = 0)
        {
            return RedirectToPage("ParticipantArtist", "Participant", new { id = id });
        }

        // Function that returns a list of concerts on a given date
        public List<Concert> ConcertDayChooser(DateTime ConcertDay)
        {
            var t1 = Concerts.Select(c => c.Start.Date.ToShortDateString()).ToList();
            var t2 = ConcertDay.ToShortDateString();

            return Concerts.FindAll(c => c.Start.Date.ToShortDateString() == ConcertDay.ToShortDateString());
        }

        public PartialViewResult OnGetConcertsOverviewPartial(string concertDay)
        {
            this.Day = DateTime.Parse(concertDay);
            this.Concerts = FestivalManager.Calendar.GetConcerts();
            return Partial("Partials/_ConcertOverviewTable", this);
        }
    }

}
