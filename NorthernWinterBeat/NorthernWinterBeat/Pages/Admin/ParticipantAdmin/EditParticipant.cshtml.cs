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
    public class EditParticipantModel : PageModel
    {
        public Participant Participant { get; private set; }

        public void OnGet(int id)
        {
            Participant = FestivalManager.instance?.GetParticipant(id);
        }

        public async Task <IActionResult> OnPostEditParticipant(int id)
        {
            string Name = Request.Form["Name"];
            string Email = Request.Form["EmailEntered"];
            //string TicketNr = Request.Form["TicketNr"];

            Participant NewParticipant = new Participant(Participant.Ticket, Name, Email); 
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await FestivalManager.instance.EditParticipant(id, NewParticipant);

            return RedirectToPage("./ParticipantOverview"); 
        }
    }
}
