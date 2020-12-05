using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages.Admin.ParticipantAdmin
{
    public class EditParticipantModel : PageModel
    {
        public Participant Participant { get; private set; }
        private IFestivalManager FestivalManager { get; }
        private IDataAccess DataAccess { get; }

        public EditParticipantModel(IDataAccess dataAccess, IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
            DataAccess = dataAccess;
        }

        public void OnGet(int id)
        {
            Participant = FestivalManager.GetParticipant(id);
        }

        public IActionResult OnPostEditParticipant(int id)
        {
            string Name = Request.Form["Name"];
            string Email = Request.Form["EmailEntered"];

            Participant = FestivalManager.GetParticipant(id);

            Participant NewParticipant = new Participant(new Ticket(), Name, Email, DataAccess); 
            if (!ModelState.IsValid)
            {
                return Page();
            }
            FestivalManager.GetParticipant(id).Update(NewParticipant);

            return RedirectToPage("./ParticipantPage", new { id = id}); 
        }
        public IActionResult OnPostCancel(int id)
        {
            return RedirectToPage("./ParticipantPage", new { id = id });
        }
    }
}
