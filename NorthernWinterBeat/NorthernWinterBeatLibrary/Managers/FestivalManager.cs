﻿using Microsoft.EntityFrameworkCore;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthernWinterBeatLibrary.Managers
{
    /// <summary>
    /// The responsibility of a FestivalManager is to be the primary access to the top hiearchy of the festival. (Calendar & Participants)
    /// </summary>
    public class FestivalManager :  IFestivalManager
    {
        private List<Participant> participants { get; set; }
        private IDataAccess DataAccess { get; set; }
        public Calendar Calendar { get; set; }
        public FestivalManager(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
            participants = DataAccess.Retrieve<Participant>();
            Calendar = new Calendar(dataAccess);
        }

        public void AddParticipant(Participant p)
        {
            participants.Add(p);
            DataAccess.Add(p); 
        }
        public Participant GetParticipant(int id)
        {
            return participants.Find(p => p.ID == id); 
        }

        public List<Participant> GetParticipants()
        {
            return participants;
        }
    }
}
