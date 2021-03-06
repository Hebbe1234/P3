using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class AddVenueModel : PageModel
    {
        private IFestivalManager FestivalManager { get; }
        private IDataAccess DataAccess { get; set; }
        public AddVenueModel(IDataAccess dataAccess, IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
            DataAccess = dataAccess;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPostCreateVenue()
        {
            string Name = Request.Form["VenueEntered"];
            string CapacityString = Request.Form["CapacityEntered"];
            string Address = Request.Form["AddressEntered"];
            int Capacity = 0; 
            if(CapacityString != "")
            {
                Capacity = int.Parse(CapacityString); 
            }

            Venue NewVenue = new Venue(Name, Address, Capacity, DataAccess);
            
            FestivalManager.Calendar.AddVenue(NewVenue); 

            return RedirectToPage("./VenueOverview"); 
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("./VenueOverview"); 
        }
    }
}
