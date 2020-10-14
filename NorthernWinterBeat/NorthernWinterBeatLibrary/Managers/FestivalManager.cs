using NorthernWinterBeat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public class FestivalManager
    {

        public static FestivalManager instance;

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
        }

        public List<Participant> GetParticipants()
        {
            return new List<Participant>();
        }


    }
}
