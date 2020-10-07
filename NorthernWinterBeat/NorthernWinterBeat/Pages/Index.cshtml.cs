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


            if(TicketInput != "")
            {
                return RedirectToPage("./MakeUserLogin");
            }

            if (PasswordInput == "Admin")
            {
                return RedirectToPage("./Admin/IndexAdmin");
            }
            else if (PasswordInput == "Participant")
            {
                return RedirectToPage("./Participant/IndexParticipant");
            }
            else if (PasswordInput == "Venue")
            {
                return RedirectToPage("./Venue/IndexVenue");
            }


            return RedirectToPage("./Index");

        }
    }
}
