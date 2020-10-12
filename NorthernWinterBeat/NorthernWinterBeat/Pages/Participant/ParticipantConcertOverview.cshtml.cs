using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages
{
    public class ParticipantConcertOverviewModel : PageModel
    {

        //Dummy Concerts, which is to get retrieved from the DB
        public List<Concert> Concerts { get; set; } = new List<Concert>() {
            new Concert(new DateTime(2020, 10, 08, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Lil Pump", "Han er sej" ),
            new Concert(new DateTime(2020, 10, 08, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Lil Pump", "Han er sej" ),
            new Concert(new DateTime(2020, 10, 08, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Lil Pump", "Han er sej" ),

            new Concert(new DateTime(2020, 10, 09, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Bad Bunny", "Han er sej" ),
            new Concert(new DateTime(2020, 10, 09, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Bad Bunny", "Han er sej" ),
            new Concert(new DateTime(2020, 10, 09, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Bad Bunny", "Han er sej" ),

            new Concert(new DateTime(2020, 10, 10, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Drake", "Han er sej" ),
            new Concert(new DateTime(2020, 10, 10, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Drake", "Han er sej" ),
            new Concert(new DateTime(2020, 10, 10, 20, 00, 00), new DateTime(2020, 10, 08, 21, 00, 00), new Models.Venue("Studenterhuset", "Budolfi Plads", 30 ), "Drake", "Han er sej" )

        };


        [BindProperty(SupportsGet = true)]
        public DateTime? Day { get; set; } = null;
        public void OnGet(DateTime? Day)
        {
        
            if(Day == null)
            {
                this.Day = new DateTime(2020, 10, 08);
            }
            
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
