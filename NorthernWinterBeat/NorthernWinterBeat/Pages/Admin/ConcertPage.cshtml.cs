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
        public Concert concert { get; private set; } = new Concert(new DateTime(2021, 01, 28, 20, 00, 00), new DateTime(2021, 01, 28, 20, 30, 00), new Models.Venue("Studenterhuset", "Borgergase 2", 33), "Martin Hansen", "Fuckig sejg");
        public void OnGet()
        {
        }


    }
}
