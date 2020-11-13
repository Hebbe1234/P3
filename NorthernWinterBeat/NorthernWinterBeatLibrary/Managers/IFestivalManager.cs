using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public interface IFestivalManager
    {
        public Calendar Calendar { get; set; }

        public void AddParticipant(Participant p);
        public List<Participant> GetParticipants();
        public Participant GetParticipant(int id);

    }
}
