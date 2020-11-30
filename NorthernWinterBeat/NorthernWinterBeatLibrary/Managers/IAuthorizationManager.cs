using Microsoft.AspNetCore.Authentication;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace NorthernWinterBeatLibrary.Managers
{
    public interface IAuthorizationManager
    {
        public bool VerifyTicket(string TicketInput);
        public ApplicationUser GetUser(string username);
        public (ClaimsIdentity, AuthenticationProperties) CreateClaim(ApplicationUser user);
        public string Encrypt(string password);
        public void CreateParticipantUser(string NameEntered, string EmailEntered, string Password1Entered, string ticketNumber);
        void CreateVenueUser(int id, string Username, string Password1Entered);
        public void SendEmail(string UserEmail, Participant p);
    }
}
