﻿using Microsoft.EntityFrameworkCore;
using NorthernWinterBeat.Models;
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
    public class FestivalManager
    {
        public static FestivalManager instance;

        private List<Participant> participants;
        public Calendar _calendar;
        public FestivalManager()
        {
            if(instance == null)
            {
                participants = DatabaseManager.context.Participant.Include(p => p.Ticket).ToList();
                _calendar = new Calendar();
                instance = this;
            }
        }

        public async Task AddParticipant(Participant p)
        {
            participants.Add(p);
            DatabaseManager.context.Participant.Add(p);
            await DatabaseManager.context.SaveChangesAsync();
        }
        public Participant GetParticipant(int id)
        {
            return participants.Find(p => p.ID == id); 
        }

        public List<Participant> GetParticipants()
        {
            return participants;
        }
        public async Task EditParticipant(int id, Participant NewParticipant)
        {
            Participant participant = participants.Find(p => p.ID == id);
            participant.Name = NewParticipant.Name;
            participant.Username = NewParticipant.Username;
            try
            {
                await DatabaseManager.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
