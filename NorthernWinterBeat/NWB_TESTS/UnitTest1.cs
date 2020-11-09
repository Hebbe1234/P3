using NorthernWinterBeatLibrary.DataAccess;
using System;
using Xunit;
using Moq; 

namespace NWB_TESTS
{
    public class UnitTestConcert
    {

        [Fact]
        public void Concert_BookingsStartEmpty()
        {
            //Arrange
            Concert concert = new Concert();
            int expected = 0;

            //Act 
            int result = concert.Bookings.Count;

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RemoveBooking_RemovesKnownBooking()
        {
            //Arrange
            var dataAccess = new Mock<IDataAccess>();
            //dataAccess.Setup(p => p.Save()); 
            Concert concert = new Concert(dataAccess.Object);
            Booking booking = new Booking();
            int expected = 0;
            concert.Bookings.Add(booking);

            //Act 
            concert.RemoveBooking(booking); 
            int result = concert.Bookings.Count;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
