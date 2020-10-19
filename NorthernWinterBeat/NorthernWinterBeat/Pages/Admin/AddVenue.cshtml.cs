using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class AddVenueModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostCreateVenue()
        {
            string Name = Request.Form["VenueEntered"];
            int Capacity = int.Parse(Request.Form["CapacityEntered"]);
            string Address = Request.Form["AddressEntered"];

            Venue NewVenue = new Venue(Name, Address, Capacity);
            FestivalManager.instance._calendar.AddVenue(NewVenue); 

            return RedirectToPage("./Venues"); 
        }
    }
}
