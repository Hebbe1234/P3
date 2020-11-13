using Moq;
using NorthernWinterBeat.Models;
using NorthernWinterBeatLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWB_TESTS
{
    public class UnitTestVenue
    {
        [Fact]
        public void Update_CheckVenueUpdatesWithNewInfo()
        {
            //Arrange
            var mock = new Mock<IDataAccess>();
            Venue venue = new Venue("Morten", "At The sea", 100, mock.Object);
            Venue UpdateVenueInfo = new Venue("Mogens", "By The lake", 26, mock.Object);

            //Act
            venue.Update(UpdateVenueInfo);

            //Assert
            Assert.Equal(venue.Name, UpdateVenueInfo.Name); 
            Assert.Equal(venue.Address, UpdateVenueInfo.Address); 
            Assert.Equal(venue.Capacity, UpdateVenueInfo.Capacity); 
        }
    }
}
