using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NorthernWinterBeat.Pages.Participant
{
    public class ParticipantShowBookingModel : PageModel
    {
        public string ticketNumber { get; private set; }
        public void OnGet()
        {
            ticketNumber = "ASDSD8AS9D98934";
        }
    }
}
