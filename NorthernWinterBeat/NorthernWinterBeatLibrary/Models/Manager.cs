using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class Manager : User
    {
        public enum Roles
        {
            ADMINISTRATOR, VENUE
        }
        public Roles Role { get; private set; }
        public Manager(Roles _role)
        {
            Role = _role; 
        }
        public Manager(Roles _role, string _username, string _password)
        {
            Username = _username;
            Password = _password; 
            Role = _role;
        }
    }
}
