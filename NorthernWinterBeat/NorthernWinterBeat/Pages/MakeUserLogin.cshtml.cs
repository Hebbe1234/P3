using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages
{
    public class MakeUserLoginModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string ticketNumber { get; set; }

        public void OnGet()
        {

        }
        public IActionResult OnPostAsync()
        {
            string UsernameEntered = Request.Form["UsernameEntered"];
            string Password1Entered = Request.Form["Password1Entered"];
            string Password2Entered = Request.Form["Password2Entered"];

            //Her kan koden valideres

            if (Password1Entered != Password2Entered)
            {
                return RedirectToPage("./MakeUserLogin");
            }

            //Her kan det valideres hvorvidt usernamet er korrekt. 
            if (UsernameEntered == "")
            {
                return RedirectToPage("./MakeUserLogin");
            }

            var newUser = new ApplicationUser(UsernameEntered, AuthorizationManager.instance.Encrypt(Password1Entered), ApplicationUser.Roles.PARTICIPANT)
            {
                TicketID = ticketNumber
            };

            DatabaseManager.context.ApplicationUser.Add(newUser);
            DatabaseManager.context.SaveChanges();

            var newParticipant = new Participant(new Ticket(ticketNumber));
            FestivalManager.instance.AddParticipant(newParticipant);

            return RedirectToPage("./Index");

        }

    }
}
