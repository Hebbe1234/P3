using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class Participant
    {
        public ParticipantEntity ParticipantEntity { get; set; }
        private IFestivalManager FestivalManager { get; }
        public Ticket Ticket { get; }

        public Participant(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
        }
        public Participant(Ticket _ticket, IFestivalManager _festivalManager) :
            this(_festivalManager)
        {
            Ticket = _ticket;
        }


        public virtual bool CanMakeBookingAt(Concert concert)
        {
            List<Concert> bookedConcerts = FestivalManager.Calendar.GetConcerts().FindAll(c => c.Bookings.Find(b => b.Participant == this) != null);
            foreach (var c in bookedConcerts)
            {
                if (concert.Start < c.End && concert.End > c.Start)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Booking> GetParticipantBookings()
        {
            return (FestivalManager.Calendar
                 .GetConcerts()
                 .SelectMany(c => c.Bookings))
                 .ToList()
                 .FindAll(b => b.Participant.ParticipantEntity.ID == this.ParticipantEntity.ID);
        }
    }
}
