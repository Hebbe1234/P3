using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Managers;

namespace NorthernWinterBeat.Pages.Admin.ParticipantAdmin
{
    public class ParticipantPageModel : PageModel
    {
        public Participant Participant { get; private set; }

        public void OnGet(int id)
        {
            Participant = FestivalManager.instance?.GetParticipant(id); 
        }


    }
}
