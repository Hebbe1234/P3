using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
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
        private IDataAccess DataAccess { get; set; }
        public FestivalManager(IDataAccess dataAccess)
        {
            if(instance == null)
            {
                DataAccess = dataAccess;
                participants = DataAccess.Retrieve<Participant>();
                _calendar = new Calendar(dataAccess);
                instance = this;
            }
        }

        public void AddParticipant(Participant p)
        {
            participants.Add(p);
            DataAccess.Add(p); 
        }

        public List<Participant> GetParticipants()
        {
            return participants;
        }
    }
}
