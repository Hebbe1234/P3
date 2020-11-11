using NorthernWinterBeatLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NorthernWinterBeat.Models
{
    public class Venue
    {

        public enum VenueState
        {
            ACTIVE, INACTIVE
        }

        private Venue()
        {
        }
        public Venue(IDataAccess _dataAccess)
        {
            State = VenueState.ACTIVE;
            DataAccess = _dataAccess; 
        }
        public Venue(string _name, string _address, int _capacity, IDataAccess _dataAccess) :
            this(_dataAccess)
        {
            Name = _name;
            Address = _address;
            Capacity = _capacity;
        }
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public VenueState State { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        private IDataAccess DataAccess { get; set; }

        public void Update(Venue NewVenueInfo)
        {
            Capacity = NewVenueInfo.Capacity;
            Address = NewVenueInfo.Address;
            Name = NewVenueInfo.Name;

            DataAccess.Save();
        }
    }
}
