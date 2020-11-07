using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin.ParticipantAdmin
{
    public class ParticipantPageModel : PageModel
    {
        public Participant Participant { get; private set; }
        public List<Concert> BookedConcerts { get; set; } = new List<Concert>();
        public List<Concert> NonBookedConcerts { get; set; } = new List<Concert>();
        public void OnGet(int id)
        {
            Participant = FestivalManager.instance?.GetParticipant(id);
            bool flag = true; 

            foreach (Concert item in FestivalManager.instance._calendar.GetAllConcerts())
            {
                flag = true; 
                foreach (Booking booking in item.Bookings)
                {
                    if(booking.Participant.ID == id)
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
            Concert concert = FestivalManager.instance._calendar.GetConcert(concertId);
            Booking booking = concert.Bookings.Find(b => b.Participant.ID == participantId);
            concert.RemoveBooking(booking);
            return RedirectToPage("./ParticipantPage", new { id = participantId });
        }
        public IActionResult OnPostAddBooking(int concertId, int participantId)
        {
            Concert concert = FestivalManager.instance._calendar.GetConcert(concertId);
            Participant participant = FestivalManager.instance.GetParticipant(participantId);
            concert.MakeBooking(participant);
            return RedirectToPage("./ParticipantPage", new { id = participantId });
        }
    }
}
