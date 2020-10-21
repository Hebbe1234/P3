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
    public class EditVenueModel : PageModel
    {
        public Venue venue { get; set; }

        public void OnGet(int id)
        {
            venue = FestivalManager.instance._calendar.GetVenue(id);

        }
        public IActionResult OnPostEditVenue(int id)
        {
            string Name = Request.Form["VenueEntered"];
            int Capacity = int.Parse(Request.Form["CapacityEntered"]);
            string Address = Request.Form["AddressEntered"];
            Venue NewVenueInfo = new Venue(Name, Address, Capacity);
            FestivalManager.instance._calendar.EditVenue(id, NewVenueInfo);
            return RedirectToPage("./Venues"); 
        }
    }
}
