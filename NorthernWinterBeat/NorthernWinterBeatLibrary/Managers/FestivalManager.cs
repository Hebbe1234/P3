using NorthernWinterBeat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{

    /// <summary>
    /// The responsibility of a FestivalManager is to be the primary access to the top hiearchy of the festival. (Calendar & Participants)
    /// </summary>
    public class FestivalManager
    {
        public static FestivalManager instance;

        private List<Participant> participants = new List<Participant>() { new Participant(new Ticket(234)) };

        public FestivalManager()
        {
            if(instance == null)
            {
                CreateCalendar();
                instance = this;
            }
        }

        public Calendar _calendar;

        public void CreateCalendar()
        {
            _calendar = new Calendar();
            _calendar.AddConcert(new Concert(DateTime.Now, DateTime.Now, new Venue("Shit", "shit", 10), "shit", "shitty") { ID = 0 });
            _calendar.AddConcert(new Concert()
            {
                Artist = "Artist Name",
                ArtistDescription = "Joe & The Shitboys brings the perfect combination of humorous lyrics and old school punk gimmicks without getting tacky."
                + " The average song length is around 1:30 minutes, and that is all they need to make you feel like a shitty person."
                + "Which they manage to do with songs like “Life Is Great You Suck” and “If You Believe in Eating Meat, Start With Your Dog”."
                + "It’s painfully easy to ignore the roasting and enjoy the show, caused by the high energy the band delivers, and their charming and flamboyant frontman, Joe. "
                + "They have been able to land slots at Iceland Airwaves, Spot Festival and on the NME 100 Essential new artists for 2020 list in spite of their short existence as a band, and now they are hitting NWB21! "
                + "“Joe and The Shitboys were the best thing we saw at Iceland Airwaves … No - one is safe from their cheeky tongue and no - bullshit approach.” – NME"
                + "“The band are truly causing mayhem and we’re so here for it.” – The Line of Best Fit"
                + "“Chaotic” – Dork",
                Start = DateTime.Now,
                End = DateTime.Now.AddMinutes(100),
                Venue = new Venue("Shit", "shit", 59),
                ID = 10
            });


        }

        public List<Participant> GetParticipants()
        {
            return participants;

        }


    }
}
