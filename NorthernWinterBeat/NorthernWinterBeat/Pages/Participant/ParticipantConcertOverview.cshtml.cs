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
        public static List<Concert> Concerts { get; set; } = new List<Concert>() {
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
        public DateTime Day { get; set; }
        public void OnGet()
        {

        }

        // Different Post-method-handlers, called in each button in the DOM
        // Handlers should be made independent of the particular weekday.
        public IActionResult OnPostMonday()
        {
            return RedirectToPage("./ParticipantConcertOverview", new { Day = new DateTime(2020, 10, 08) });
        }
        public IActionResult OnPostTuesday()
        {
            return RedirectToPage("./ParticipantConcertOverview", new { Day = new DateTime(2020, 10, 09) });
        }
        public IActionResult OnPostWednesday()
        {
            return RedirectToPage("./ParticipantConcertOverview", new { Day = new DateTime(2020, 10, 10) });
        }

        public IActionResult OnGetSelectArtist(int? id = 0)
        {
            return RedirectToPage("ParticipantArtist", "Participant", new { id = id });
        }

        // Function that returns a list of concerts on a given date
        public List<Concert> ConcertDayChooser(DateTime ConcertDay)
        {
            return Concerts.FindAll(c => c.Start.Date.ToShortDateString() == ConcertDay.ToShortDateString());
        }
    }
}
