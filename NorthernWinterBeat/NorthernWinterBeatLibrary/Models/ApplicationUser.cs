using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class ApplicationUser
    {

        public ApplicationUser() { }
        public ApplicationUser(string username, string password, Roles role)
        {
            Username = username;
            Password = password;
            Role = role;
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
    }
}
