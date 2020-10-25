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
        public void OnGet(DateTime? Day)
        {
            Concerts = FestivalManager.instance._calendar.GetConcerts();

            if (Day == null)
            {
                this.Day = Concerts.First().Start;
            }

            
        }
        //public IActionResult OnPostDate(string date)
        //{
        //    return RedirectToPage("./ParticipantConcertOverview", new { Day = DateTime.Parse(date) });

        //}

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
            this.Concerts = FestivalManager.instance._calendar.GetConcerts();
            return Partial("Partials/_ConcertOverviewTable", this);
        }
    }

}
