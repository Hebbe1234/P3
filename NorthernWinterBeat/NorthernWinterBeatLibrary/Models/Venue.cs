using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthernWinterBeat.Models
{
    public class Venue
    {
  
        public Venue(string _name, string _address, int _capacity)
        {
            Name = _name;
            Address = _address;
            Capacity = _capacity; 
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }

    }
}
