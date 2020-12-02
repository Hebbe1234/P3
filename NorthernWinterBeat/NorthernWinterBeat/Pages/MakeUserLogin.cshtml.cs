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
        [BindProperty(SupportsGet = true)]
        public string Alert { get; set; }

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
            string EmailEntered = Request.Form["EmailEntered"].ToString().ToLower();
            string Password1Entered = Request.Form["Password1Entered"];
            string Password2Entered = Request.Form["Password2Entered"];

            //Her kan koden valideres
            if (NameEntered == "")
            {
                return RedirectToPage("./MakeUserLogin", new { Alert = "No Name", ticketNumber = ticketNumber });
            }
            else if (EmailEntered == "")
            {
                return RedirectToPage("./MakeUserLogin", new { Alert = "No Email", ticketNumber = ticketNumber });
            }
            else if (Password1Entered == "" || Password2Entered == "")
            {
                return RedirectToPage("./MakeUserLogin", new { Alert = "No Password", ticketNumber = ticketNumber });
            }


            if (Password1Entered != Password2Entered)
            {
                return RedirectToPage("./MakeUserLogin", new { Alert = "Different Passwords", ticketNumber = ticketNumber });
            }

            //Her kan det valideres hvorvidt usernamet er korrekt. 

            var user = AuthorizationManager.GetUser(EmailEntered);
            if (user != null)
            {
                Console.WriteLine("A user with that email already exist");
                return RedirectToPage("./MakeUserLogin", new { Alert = "Email Exist", ticketNumber = ticketNumber });
            }

            AuthorizationManager.CreateParticipantUser(NameEntered, EmailEntered, Password1Entered, ticketNumber);

            return RedirectToPage("./Index", new { Alert = "User Created", ticketNumber = ticketNumber });
        }

        
    }
}
