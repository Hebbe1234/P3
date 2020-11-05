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
    public class ParticipantOverviewModel : PageModel
    {
        public List<Participant> Participants = new List<Participant>();



        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "";

        public ParticipantOverviewModel()
        {
            Participants = FestivalManager.instance.GetParticipants();
        }
        public void OnGet()
        {
            Participants = FestivalManager.instance.GetParticipants();
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

            //return Partial("Partials/_ConcertTable", this);
        }
    }
}