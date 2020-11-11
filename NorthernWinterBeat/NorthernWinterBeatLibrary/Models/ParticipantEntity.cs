using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NorthernWinterBeat.Models
{	public class ParticipantEntity : User
	{
		public enum ParticipantState
        {
			ACTIVE, INACTIVE
        }

        public ParticipantState State { get; set; }
        public string Name { get; set; } = "";
		public Ticket Ticket { get; protected set; }

        public ParticipantEntity()
        {
            State = ParticipantState.ACTIVE;
        }

        
    }
}