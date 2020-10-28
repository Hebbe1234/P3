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
        private readonly NorthernWinterBeatConcertContext _context;
        public AddVenueModel(NorthernWinterBeatConcertContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostCreateVenue()
        {
            string Name = Request.Form["VenueEntered"];
            int Capacity = int.Parse(Request.Form["CapacityEntered"]);
            string Address = Request.Form["AddressEntered"];

            Venue NewVenue = new Venue(Name, Address, Capacity);
            

            await FestivalManager.instance._calendar.AddVenue(NewVenue); 

            return RedirectToPage("./Venues"); 
        }
    }
}
