using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.Models
{
    public class Manager : User
    {
        public Manager(string _role)
        {
            Role = _role; 
        }
        public Manager(string _role, string _username, string _password)
        {
            Username = _username;
            Password = _password; 
            Role = _role;
        }
        public string Role { get; private set; }
    }
}
