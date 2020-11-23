using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages.Admin
{
    public class CreateVenueLoginModel : PageModel
    {
        public Venue venue { get; set; }
        public IDataAccess DataAccess { get; set; }
        private IFestivalManager FestivalManager { get; }
        private IAuthorizationManager AuthorizationManager { get; }
        public CreateVenueLoginModel(IDataAccess dataAccess, IFestivalManager festivalManager, IAuthorizationManager authorizationManager)
        {
            FestivalManager = festivalManager;
            DataAccess = dataAccess;
            AuthorizationManager = authorizationManager; 
        }
        public void OnGet(int id)
        {
            venue = FestivalManager.Calendar.GetVenue(id);

        }
        public IActionResult OnPostCreateVenueLogin(int id)
        {
            string Username = Request.Form["VenueUsername"];
            string Password1Entered = Request.Form["Password1Entered"];
            string Password2Entered = Request.Form["Password2Entered"];

            //Her kan koden valideres
            if (Password1Entered != Password2Entered)
            {
                return RedirectToPage("./CreateVenueLogin", new { id = id });
            }

            var user = AuthorizationManager.GetUser(Username);
            if (user != null)
            {
                Console.WriteLine("A Venue with that username already exist");
                return RedirectToPage("./CreateVenueLogin", new { id = id });
            }
            else if (Username == "")
            {
                Console.WriteLine("No email entered");
                return RedirectToPage("./CreateVenueLogin", new { id = id });
            }

            AuthorizationManager.CreateVenueUser(id, Username, Password1Entered); 
            return RedirectToPage("./Calendar");
        }

        
    }
}
