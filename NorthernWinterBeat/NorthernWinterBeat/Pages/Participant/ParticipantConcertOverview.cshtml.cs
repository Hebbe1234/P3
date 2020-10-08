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
        public DateTime Day { get; set; } = new DateTime(2002, 08, 10);
        public void OnGet()
        {

        }

        public IActionResult OnPostMonday()
        {
            Console.WriteLine("Hello");
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

        public List<Concert> ConcertDayChooser(DateTime ConcertDay)
        {
            Console.WriteLine("helloo" + ConcertDay);
        
                return Concerts.FindAll(c => c.Start.Date.ToShortDateString() == ConcertDay.ToShortDateString());
        }
    }
}
