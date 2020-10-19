using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace NorthernWinterBeat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }



        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            string TicketInput = Request.Form["TicketEntered"];
            string EmailInput = Request.Form["EmailEntered"];
            string PasswordInput = Request.Form["PasswordEntered"];

            //Her testes hvorvidt en billet er indtastet, og valideringen skal ske her. 
            if(TicketInput != "")
            {
                return RedirectToPage("./MakeUserLogin");
            }

            //Her ender vi når en billet ikke er indtastet, hvilket betyder der skal logges ind. 
            if (PasswordInput == "Admin")
            {
                return RedirectToPage("./Admin/IndexAdmin");
            }
            else if (PasswordInput == "Participant")
            {
                return RedirectToPage("./ParticipantRazor/ParticipantConcertOverview");
            }
            else if (PasswordInput == "Venue")
            {
                return RedirectToPage("./Venue/IndexVenue");
            }

            //Her endes der hvis der er indtastet noget forkert eller intet, derfor reloades der. 
            return RedirectToPage("./Index");

        }
    }
}
