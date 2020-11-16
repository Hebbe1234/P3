using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class EditVenueModel : PageModel
    {
        public Venue venue { get; set; }
        public IDataAccess DataAccess { get; set; }
        private IFestivalManager FestivalManager { get; }
        public EditVenueModel(IDataAccess dataAccess, IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
            DataAccess = dataAccess;
        }
        public void OnGet(int id)
        {
            venue = FestivalManager.Calendar.GetVenue(id);

        }
        public IActionResult OnPostEditVenue(int id)
        {
            string Name = Request.Form["VenueEntered"];
            string CapacityString = Request.Form["CapacityEntered"];
            string Address = Request.Form["AddressEntered"];
            int Capacity = 0;
            if (CapacityString != "")
            {
                Capacity = int.Parse(CapacityString);
            }

            Venue NewVenueInfo = new Venue(Name, Address, Capacity, DataAccess);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            FestivalManager.Calendar.GetVenue(id).Update(NewVenueInfo);

            return RedirectToPage("./VenueOverview"); 
        }
        public IActionResult OnPostCancel(int id)
        {
            return RedirectToPage("./VenuePage", new { id = id });
        }
    }
}
