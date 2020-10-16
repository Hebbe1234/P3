using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;


namespace NorthernWinterBeat.Pages.ParticipantRazor
{
    public class ParticipantBookingsModel : PageModel
    {

        public List<Booking> bookings { get; private set; } = new List<Booking>();

        public void OnGet()
        {
            bookings.Add(
                new Booking(
                    new Models.Participant(new Ticket(1))
                    ,
                    new Concert()
                    {
                        Artist = "Martin",
                        Start = DateTime.Now,
                        Venue = new Models.Venue()
                        {
                            Address = "Studenterhuset"
                        }
                    }
                )
            );
            bookings.Add(
                new Booking(
                    new Models.Participant(new Ticket(1))
                    ,
                    new Concert()
                    {
                        Artist = "Jakob",
                        Start = DateTime.Now,
                        Venue = new Models.Venue()
                        {
                            Address = "Studenterhuset kælder"
                        }
                    }
                )
            ); bookings.Add(
                 new Booking(
                     new Models.Participant(new Ticket(1))
                     ,
                     new Concert()
                     {
                         Artist = "Joachim",
                         Start = DateTime.Now,
                         Venue = new Models.Venue()
                         {
                             Address = "Studenterhuset"
                         }
                     }
                 )
             );
        }
    }
}
