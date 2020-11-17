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
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages
{
    public class MakeUserLoginModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string ticketNumber { get; set; }
        private IAuthorizationManager AuthorizationManager { get; }
        private IFestivalManager FestivalManager { get; }

        private IDataAccess DataAccess { get; }

        public MakeUserLoginModel(IFestivalManager festivalManager, IAuthorizationManager authorizationManager, IDataAccess dataAccess)
        {
            AuthorizationManager = authorizationManager;
            FestivalManager = festivalManager;
            DataAccess = dataAccess; 
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            string NameEntered = Request.Form["NameEntered"];
            string EmailEntered = Request.Form["EmailEntered"];
            string Password1Entered = Request.Form["Password1Entered"];
            string Password2Entered = Request.Form["Password2Entered"];

            //Her kan koden valideres

            if (Password1Entered != Password2Entered)
            {
                return RedirectToPage("./MakeUserLogin");
            }

            //Her kan det valideres hvorvidt usernamet er korrekt. 

            var user = AuthorizationManager.GetUser(EmailEntered);
            if(user != null )
            {
                Console.WriteLine("A user with that email already exist");
                return RedirectToPage("./MakeUserLogin");
            } else if(EmailEntered == "")
            {
                Console.WriteLine("No email entered");
                return RedirectToPage("./MakeUserLogin");
            }

            var newUser = new ApplicationUser(EmailEntered, AuthorizationManager.Encrypt(Password1Entered), ApplicationUser.Roles.PARTICIPANT)
            {
                TicketID = ticketNumber
            };

            DataAccess.Add(newUser);
            Participant newParticipant = new Participant(new Ticket(ticketNumber), NameEntered, EmailEntered, DataAccess);
            FestivalManager.AddParticipant(newParticipant);

            return RedirectToPage("./Index");
        }
    }
}
