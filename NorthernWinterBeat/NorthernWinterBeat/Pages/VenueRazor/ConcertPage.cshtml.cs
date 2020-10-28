using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.VenueRazor
{
    public class ConcertPageModel : PageModel
    {
        public Concert concert { get; private set; }

        public void OnGet(int id = 0)
        {
            concert = FestivalManager.instance?._calendar?.GetConcert(id);
        }
    }
}
