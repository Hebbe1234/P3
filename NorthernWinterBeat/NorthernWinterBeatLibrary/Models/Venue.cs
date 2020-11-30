using NorthernWinterBeatLibrary.DataAccess;
using NorthernWinterBeatLibrary.Managers;
using System.ComponentModel.DataAnnotations;

namespace NorthernWinterBeat.Models
{
    public class Venue
    {
        public Venue(NorthernWinterBeatConcertContext ctx)
        {
            DataAccess = new EFDataAccess(ctx);
        }
        public Venue(IDataAccess _dataAccess)
        {
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
