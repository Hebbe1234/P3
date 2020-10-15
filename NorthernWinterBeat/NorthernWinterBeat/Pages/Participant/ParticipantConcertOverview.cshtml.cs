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
            if(Day == null)
            {
                this.Day = new DateTime(2020, 10, 08);
            }

            Concerts = FestivalManager.instance._calendar.GetConcerts();
            
        }
        public IActionResult OnPostDate(string date)
        {
            return RedirectToPage("./ParticipantConcertOverview", new { Day = DateTime.Parse(date) });

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
    }
}
