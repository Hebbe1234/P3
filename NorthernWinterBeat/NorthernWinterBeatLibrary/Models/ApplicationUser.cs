using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class ApplicationUser
    {
        private ApplicationUser(NorthernWinterBeatConcertContext ctx)
        {
            DataAccess = new EFDataAccess(ctx);
        }

        public ApplicationUser(string username, string password, Roles role, IDataAccess dataAccess)
        {
            Username = username;
            Password = password;
            Role = role;
            DataAccess = dataAccess; 
        }
        public enum Roles
        {
            PARTICIPANT, ADMIN, VENUE
        }
        public string Username { get; set; } = "Test";
        public string Password { get; set; } = "Test";
        public Roles Role { get; set; } = Roles.ADMIN;
        [Key]
        public int ID { get; set; }
        public int? VenueID { get; set; }
        public string TicketID { get; set; }
        private IDataAccess DataAccess { get; set; }

        public void Update(ApplicationUser newApplicationUser)
        {
            this.Password = newApplicationUser.Password;
            DataAccess.Save(); 
        }
    }
}
