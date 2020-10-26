using NorthernWinterBeat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{

    /// <summary>
    /// The responsibility of a FestivalManager is to be the primary access to the top hiearchy of the festival. (Calendar & Participants)
    /// </summary>
    public class FestivalManager
    {
        public static FestivalManager instance;

        private List<Participant> participants;
        public Calendar _calendar;
        public FestivalManager()
        {
            if(instance == null)
            {
                participants = DatabaseManager.context.Participant.ToList();
                _calendar = new Calendar();
                instance = this;
            }
        }

        public List<Participant> GetParticipants()
        {
            return participants;
        }
    }
}
