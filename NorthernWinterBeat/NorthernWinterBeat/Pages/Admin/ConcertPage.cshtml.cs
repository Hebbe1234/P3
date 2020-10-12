using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages.Admin
{
    public class ConcertPageModel : PageModel
    {
        public List<Concert> Concerts { get; set; } = new List<Concert>() {
            new Concert( new DateTime(2021, 01, 28, 20, 00, 00), new DateTime(2021, 01, 28, 20, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 33), "Martin Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 21, 00, 00), new DateTime(2021, 01, 28, 21, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 36), "ANders Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 19, 00, 00), new DateTime(2021, 01, 28, 19, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 38), "Martsefs Hansen", "Fuckig sejg"),
            new Concert( new DateTime(2021, 01, 28, 19, 45, 00), new DateTime(2021, 01, 28, 20, 45, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 30), "Marsefsetin Hansen", "Fuckig sejg") };
        [BindProperty (SupportsGet =true)]
        public int ConcertID { get; set; }
        public void OnGet()
        {
        }


    }
}
