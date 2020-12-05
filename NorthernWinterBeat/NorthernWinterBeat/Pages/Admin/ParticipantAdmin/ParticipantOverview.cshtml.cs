using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthernWinterBeatLibrary.Managers;
using NorthernWinterBeatLibrary.Models;

namespace NorthernWinterBeat.Pages.Admin.ParticipantAdmin
{
    public class ParticipantOverviewModel : PageModel
    {
        private IFestivalManager FestivalManager { get; }

        public List<Participant> Participants = new List<Participant>();
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "";

        public ParticipantOverviewModel(IFestivalManager festivalManager)
        {
            FestivalManager = festivalManager;
            Participants = FestivalManager.GetParticipants();
        }
        public void OnGet()
        {
            Participants = FestivalManager.GetParticipants();
        }

        public List<Participant> SortParticipants(List<Participant> SortParticipants)
        {
            switch(SortBy)
            {
                case "Name":
                    SortParticipants = SortParticipants.OrderBy(p => p.Name).ToList();
                    break;
                case "E-mail":
                    SortParticipants = SortParticipants.OrderBy(p => p.Username).ToList();
                    break;
                case "TicketNr":
                    SortParticipants = SortParticipants.OrderBy(p => p.Ticket.TicketNumber).ToList();
                    break; 
                default:
                    break;
            }
            return SortParticipants;
        }
        public PartialViewResult OnGetParticipantPartial(string sortBy)
        {
            this.SortBy = sortBy;
            return Partial("Partials/_ParticipantOverviewTable", this);
        }
    }
}