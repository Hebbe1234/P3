using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin
{
    public class ConcertPageModel : PageModel
    {
        public Concert concert { get; private set; }
        private IFestivalManager FestivalManager { get; }

        public ConcertPageModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }

        public void OnGet(int id)
        {
            concert = FestivalManager.Calendar?.GetConcert(id);
        }

        public IActionResult OnPostDeleteConcert(int id)
        {
            Concert deleteConcert = FestivalManager.Calendar.GetConcert(id); 
            FestivalManager.Calendar.DeleteConcert(deleteConcert); 
            return RedirectToPage("./Calendar");
        }
        public IActionResult OnPostRemoveBooking(int bookingid)
        {
            List<Concert> concerts = new List<Concert>();
            concerts = FestivalManager.Calendar.GetConcerts();
            Booking booking = concerts.SelectMany(c => c.Bookings)?.Where(b => b.ID == bookingid)?.First();
            int id = booking.Concert.ID; 
            booking.Concert.RemoveBooking(booking);
            return RedirectToPage("./ConcertPage", new { id = id});
        }
    }
}
