using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages.Admin.ParticipantAdmin
{
    public class ParticipantPageModel : PageModel
    {
        public Participant Participant { get; private set; }
        public List<Concert> BookedConcerts { get; set; } = new List<Concert>();
        public List<Concert> NonBookedConcerts { get; set; } = new List<Concert>();
        private IFestivalManager FestivalManager { get; set; }

        public ParticipantPageModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }

        public void OnGet(int id)
        {
            Participant = FestivalManager.GetParticipant(id);
            bool flag = true; 

            // Split all concerts into two categories. The ones where the participant has a booking
            // and the ones, where the participant does not have a booking.
            foreach (Concert item in FestivalManager.Calendar.GetConcerts())
            {
                flag = true; 
                foreach (Booking booking in item.Bookings)
                {
                    if(booking.Participant?.ID == id)
                    {
                        BookedConcerts.Add(item);
                        flag = false;
                        break;
                    }
                }
                if(flag == true)
                {
                    NonBookedConcerts.Add(item); 
                }
            }
        }

        public IActionResult OnPostRemoveBooking(int concertId, int participantId)
        {
            Concert concert = FestivalManager.Calendar.GetConcert(concertId);
            Booking booking = concert.Bookings.Find(b => b.Participant?.ID == participantId);
            concert.RemoveBooking(booking);
            return RedirectToPage("./ParticipantPage", new { id = participantId });
        }
        public IActionResult OnPostAddBooking(int concertId, int participantId)
        {
            Concert concert = FestivalManager.Calendar.GetConcert(concertId);
            Participant participant = FestivalManager.GetParticipant(participantId);
            concert.MakeBooking(participant, FestivalManager);
            return RedirectToPage("./ParticipantPage", new { id = participantId });
        }
    }
}
