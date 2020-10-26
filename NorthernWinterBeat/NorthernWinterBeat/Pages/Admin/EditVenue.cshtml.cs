using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class EditVenueModel : PageModel
    {
        public Venue venue { get; set; }
        private readonly NorthernWinterBeatConcertContext _context;
        public EditVenueModel(NorthernWinterBeatConcertContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            venue = FestivalManager.instance._calendar.GetVenue(id);

        }
        public async Task<IActionResult> OnPostEditVenue(int id)
        {
            string Name = Request.Form["VenueEntered"];
            int Capacity = int.Parse(Request.Form["CapacityEntered"]);
            string Address = Request.Form["AddressEntered"];


            Venue NewVenueInfo = new Venue(Name, Address, Capacity);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await FestivalManager.instance._calendar.EditVenue(id, NewVenueInfo);

            return RedirectToPage("./Venues"); 
        }
        private bool VenueExists(int id)
        {
            return _context.Concert.Any(e => e.ID == id);
        }

    }
}
